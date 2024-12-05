using UnityEngine;
using Zenject;

public class UnitSpawner : MonoBehaviour
{
    [SerializeField] private Transform[] _unitPositions;
    [SerializeField] private Transform _referencePoint;
    [SerializeField] private Unit _unit;
    [SerializeField] private Vector3 _unitRotation;
    [SerializeField] private int _unitsCount = 3;

    private UnitRepository _unitRepository;
    private float _unitSpeed;

    [Inject]
    public void Construct(UnitRepository unitRepository, float unitSpeed)
    {
        _unitRepository = unitRepository;
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
        GameObject unitObject = Instantiate(_unit.gameObject, _unitPositions[index].position, Quaternion.Euler(_unitRotation));

        if (unitObject.TryGetComponent(out Unit unit))
        {
            UnitMover unitMover = new UnitMover(unit, _unitSpeed, _referencePoint.transform.position);

            unit.Init(unitMover);

            _unitRepository.RegisterUnit(unitMover);
        }
    }
}