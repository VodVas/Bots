using System.Collections.Generic;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(UnitSpawner))]
[RequireComponent(typeof(ResourcesKeeper))]
[RequireComponent(typeof(UnitRepository))]
public class Base : MonoBehaviour
{
    [SerializeField] private List<ResourceStorage> _resourceStorages;
    [SerializeField] protected int _unitPrice = 3;
    [SerializeField] private int _newBasePrice = 5;

    private BaseState _currentState = BaseState.BuildingUnits;
    private ResourcesKeeper _resourcesKeeper;
    private UnitSpawner _unitSpawner;
    private Flag _currentFlag;
    private int _spawnPositionNumber = 0;
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
        if (_currentState == BaseState.WaitingForResources)
        {
            if (_resourcesKeeper.WoodCount >= _newBasePrice && _resourcesKeeper.StoneCount >= _newBasePrice)
            {
                _currentState = BaseState.SendingUnitToFlag;

                SendUnitToFlag();
            }
        }
    }

    private void SendUnitToFlag()
    {
        IUnitController availableUnit = _unitRepository.GetAvailableUnit();

        if (availableUnit != null && _unitRepository.UnitCount > 1)
        {
            SpendWarehouseResources(_newBasePrice);

            availableUnit.SetDestinationToBuildBase(_currentFlag.transform.position, NewBaseBuilt);
        }
        else
        {
            _currentState = BaseState.WaitingForResources;
        }
    }

    private void SpendWarehouseResources(int amount)
    {
        foreach (var storage in _resourceStorages)
        {
            if (storage.Type == ResourceType.Wood && _resourcesKeeper.WoodCount >= amount)
            {
                storage.Activator.DeactivateLastObject(amount);
                continue;
            }

            if (storage.Type == ResourceType.Stone && _resourcesKeeper.StoneCount >= amount)
            {
                storage.Activator.DeactivateLastObject(amount);
                continue;
            }
        }

        _resourcesKeeper.Subtract(amount, amount);
    }

    public void ResetResourcesAndStorages()
    {
        _resourcesKeeper.Reset();

        foreach (var storage in _resourceStorages)
        {
            storage.Activator.ResetActivations();
        }
    }

    private void NewBaseBuilt(IUnitController unit)
    {
        Base newBase = _baseFactory.Create(unit.Position, Quaternion.identity);

        _unitRepository.TransferUnitToBase(unit, newBase);

        if (unit is UnitMover unitMover)
        {
            unitMover.SetBase(newBase);
        }

        newBase.ResetResourcesAndStorages();

        Destroy(_currentFlag.gameObject);

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

        SpendWarehouseResources(_unitPrice);

        _unitSpawner.Create(_spawnPositionNumber);
    }
}