using UnityEngine;

public class UnitMover : IUnitController
{
    private Unit _unit;
    private Vector3 _resourcePosition;
    private Vector3 _basePosition;
    private UnitState _currentState;
    private IResourceble _targetResource;
    private float _unitSpeed;
    private float _littleValue = 0.1f;

    public bool IsAvailable { get; private set; } = true;
    public IResourceble TargetResource
    {
        get { return _targetResource; }
        private set { _targetResource = value; }
    }

    private enum UnitState
    {
        Waiting,
        MovingToResource,
        ReturningToBase
    }

    public UnitMover(Unit unit, float unitSpeed, Vector3 basePosition)
    {
        _unit = unit;
        _unitSpeed = unitSpeed;
        _basePosition = basePosition;
        _currentState = UnitState.Waiting;
    }

    public void Update()
    {
        ExecuteCurrentState();
    }

    public void SetDestinationToResource(Vector3 resourcePosition, IResourceble resource)
    {
        IsAvailable = false;
        _resourcePosition = resourcePosition;
        _targetResource = resource;
        _currentState = UnitState.MovingToResource;
    }

    private void ExecuteCurrentState()
    {
        switch (_currentState)
        {
            case UnitState.Waiting:
                IsAvailable = true;

                break;

            case UnitState.MovingToResource:
                TransitionToTarget(_resourcePosition, UnitState.ReturningToBase);

                break;

            case UnitState.ReturningToBase:
                TransitionToTarget(_basePosition, UnitState.Waiting);

                break;
        }
    }

    private void TransitionToTarget(Vector3 targetPosition, UnitState nextState)
    {
        _unit.transform.position = Vector3.MoveTowards(_unit.transform.position, targetPosition, Time.deltaTime * _unitSpeed);

        Vector3 direction = targetPosition - _unit.transform.position;

        if (direction != Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(direction);

            _unit.transform.rotation = Quaternion.Slerp(_unit.transform.rotation, rotation, Time.deltaTime * _unitSpeed);
        }

        if (Vector3.Distance(_unit.transform.position, targetPosition) < _littleValue)
        {
            _currentState = nextState;
        }
    }
}