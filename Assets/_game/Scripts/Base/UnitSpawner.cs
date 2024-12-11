using UnityEngine;
using Zenject;

[RequireComponent(typeof(UnitRepository))]
public class UnitSpawner : MonoBehaviour
{
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
        for (int i = 0; i < _unitsCount; i++)
        {
            Create();
        }
    }

    public void Create()
    {
        Unit unit = _container.InstantiatePrefabForComponent<Unit>(
            _unitPrefab,
            transform.position,
            Quaternion.Euler(_unitRotation),
            null);

        UnitMover unitMover = new UnitMover(unit, _unitSpeed, transform.position);

        unit.Init(unitMover);

        _unitRepository.RegisterUnit(unitMover);
    }
}