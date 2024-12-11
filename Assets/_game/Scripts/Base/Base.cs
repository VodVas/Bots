using System.Collections.Generic;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(UnitSpawner))]
[RequireComponent(typeof(ResourcesKeeper))]
[RequireComponent(typeof(UnitRepository))]
public class Base : MonoBehaviour
{
    [SerializeField] private List<ResourceStorage> _resourceStorages;
    [SerializeField] private int _unitPriceWood = 3;
    [SerializeField] private int _unitPriceStone = 3;
    [SerializeField] private int _newBasePriceWood = 5;
    [SerializeField] private int _newBasePriceStone = 5;

    private BaseState _currentState = BaseState.BuildingUnits;
    private ResourcesKeeper _resourcesKeeper;
    private UnitSpawner _unitSpawner;
    private Flag _currentFlag;
    private UnitRepository _unitRepository;
    [Inject] private IBaseFactory _baseFactory;

    private enum BaseState
    {
        BuildingUnits,
        WaitingForResources,
        SendingUnitToFlag
    }

    private void Awake()
    {
        _unitSpawner = GetComponent<UnitSpawner>();
        _unitRepository = GetComponent<UnitRepository>();
        _resourcesKeeper = GetComponent<ResourcesKeeper>();
    }

    private void OnEnable()
    {
        _resourcesKeeper.CanCreateUnit += CreateUnit;
        _resourcesKeeper.ResourceChange += OnResourceChange;
    }

    private void OnDisable()
    {
        _resourcesKeeper.CanCreateUnit -= CreateUnit;
        _resourcesKeeper.ResourceChange -= OnResourceChange;
    }

    public void ActivateStorageResource(IResourceble resource)
    {
        foreach (var storage in _resourceStorages)
        {
            if (storage.Type == resource.Type)
            {
                ViewWarehouseResources(storage.Activator, storage.Type, storage.Amount);

                break;
            }
        }
    }

    public void PlaceFlag(Flag flag) 
    {
        _currentFlag = flag;
        _currentState = BaseState.WaitingForResources;
    }

    public void RegisterUnit(IUnitController unit)
    {
        _unitRepository.RegisterUnit(unit);
    }

    private void OnResourceChange()
    {
        if (_currentState == BaseState.WaitingForResources && _unitRepository.UnitCount > 1)
        {


            if (_resourcesKeeper.WoodCount >= _newBasePriceWood && _resourcesKeeper.StoneCount >= _newBasePriceStone)
            {
                _currentState = BaseState.SendingUnitToFlag;

                SendUnitToFlag();
            }
        }
        else
        {
            _currentState = BaseState.BuildingUnits;
        }
    }

    private void SendUnitToFlag()
    {
        var availableUnits = _unitRepository.GetAvailableUnits();
        int closestUnit = 0;

        if (availableUnits.Count > 0 && _unitRepository.UnitCount > 1)
        {
            SpendWarehouseResources(_newBasePriceWood, _newBasePriceStone);

            IUnitController unitToSend = availableUnits[closestUnit];

            unitToSend.SetDestinationToBuildBase(_currentFlag.transform.position, BuiltNewBase);
        }
        else
        {
            _currentState = BaseState.WaitingForResources;
        }
    }

    private void SpendWarehouseResources(int amountWood, int amountStone)
    {
        foreach (var storage in _resourceStorages)
        {
            if (storage.Type == ResourceType.Wood && _resourcesKeeper.WoodCount >= amountWood)
            {
                storage.Activator.DeactivateLastObject(amountWood);
                continue;
            }

            if (storage.Type == ResourceType.Stone && _resourcesKeeper.StoneCount >= amountStone)
            {
                storage.Activator.DeactivateLastObject(amountStone);
                continue;
            }
        }

        _resourcesKeeper.Subtract(amountWood, amountStone);
    }

    private void ResetResourcesAndStorages()
    {
        _resourcesKeeper.ResetCount();

        foreach (var storage in _resourceStorages)
        {
            storage.Activator.ResetActivations();
        }
    }

    private void BuiltNewBase(IUnitController unit)
    {
        Base newBase = _baseFactory.Create(unit.Position, Quaternion.identity);

        _unitRepository.TransferUnitToBase(unit, newBase);

        if (unit is UnitMover unitMover)
        {
            unitMover.SetBase(newBase);
        }

        newBase.ResetResourcesAndStorages();

        if (_currentFlag != null)
        {
            Destroy(_currentFlag.gameObject);
        }

        _currentFlag = null;
        _currentState = BaseState.BuildingUnits;
    }

    private void ViewWarehouseResources(ChildElementVisibilitySwitcher objectActivator, ResourceType type, int amount)
    {
        objectActivator.ActivateFirstNonActiveObject();

        _resourcesKeeper.Add(type, amount);
    }

    private void CreateUnit()
    {
        if (_currentState == BaseState.WaitingForResources)
        {
            return;
        }

        SpendWarehouseResources(_unitPriceWood, _unitPriceStone);

        _unitSpawner.Create();
    }
}