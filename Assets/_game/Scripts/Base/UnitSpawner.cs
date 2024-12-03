using UnityEngine;
using Zenject;

public class UnitSpawner : MonoBehaviour
{
    [SerializeField] private Transform[] _unitPositions;
    [SerializeField] private Transform _referencePoint;
    [SerializeField] private GameObject _unitPrefab;
    [SerializeField] private Vector3 _unitRotation;
    [SerializeField] private int _unitsCount = 50;

    private UnitRepository _unitManager;
    private float _unitSpeed;

    [Inject]
    public void Construct(UnitRepository unitManager, float unitSpeed)
    {
        _unitManager = unitManager;
        _unitSpeed = unitSpeed;
    }

    private void Start()
    {
        for (int i = 0; i < _unitsCount && i < _unitPositions.Length; i++)
        {
            SpawnUnit(i);
        }
    }

    private void SpawnUnit(int index)
    {
        GameObject unitObject = Instantiate(_unitPrefab, _unitPositions[index].position, Quaternion.Euler(_unitRotation));

        if (unitObject.TryGetComponent(out Unit unit))
        {
            UnitNavigator controller = new UnitNavigator(unit, _unitSpeed, _referencePoint.transform.position);

            unit.Init(controller);

            _unitManager.RegisterUnit(controller);
        }
    }
}