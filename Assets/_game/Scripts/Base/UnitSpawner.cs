using UnityEngine;
using Zenject;

[RequireComponent(typeof(UnitRepository))]
public class UnitSpawner : MonoBehaviour
{
    [SerializeField] private Transform[] _unitPositions;
    [SerializeField] private Transform _base;
    [SerializeField] private Vector3 _unitRotation;
    [SerializeField] private int _unitsCount = 3;

    private UnitRepository _unitRepository;

    [Inject] private Unit _unitPrefab;
    [Inject] private DiContainer _container;
    [Inject] private float _unitSpeed;

    private void Awake()
    {
        _unitRepository = GetComponent<UnitRepository>();
    }

    private void OnEnable()
    {
        for (int i = 0; i < _unitsCount && i < _unitPositions.Length; i++)
        {
            Create(i);
        }
    }

    public void Create(int index)
    {
        Unit unit = _container.InstantiatePrefabForComponent<Unit>(
            _unitPrefab,
            _unitPositions[index].position,
            Quaternion.Euler(_unitRotation),
            null);

        UnitMover unitMover = new UnitMover(unit, _unitSpeed, _base.transform.position);

        unit.Init(unitMover);

        _unitRepository.RegisterUnit(unitMover);
    }
}