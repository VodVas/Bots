using System;
using UnityEngine;
using DG.Tweening;

public class UnitMover : IUnitController
{
    private Vector3 _resourcePosition;
    private Vector3 _basePosition;
    private Vector3 _destination;
    private UnitState _currentState;
    private IResourceble _targetResource;
    private Unit _unit;
    private Base _currentBase;
    private Tweener _moveTweener;
    private Tweener _rotateTweener;
    private float _unitSpeed;
    private float _rotationSpeed = 0.15f;

    private Action<IUnitController> _onBaseBuilt;

    public Vector3 Position => _unit.transform.position;
    public bool IsAvailable { get; private set; } = true;
    public IResourceble TargetResource
    {
        get { return _targetResource; }
        private set
        { _targetResource = value; }
    }

    private enum UnitState
    {
        Waiting,
        MovingToResource,
        ReturningToBase,
        MovingToBuildBase,
        BuildingBase
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

    public void SetBase(Base newBase)
    {
        _currentBase = newBase;
        _basePosition = newBase.transform.position;
    }

    public void SetDestinationToResource(Vector3 resourcePosition, IResourceble resource)
    {
        IsAvailable = false;
        _resourcePosition = resourcePosition;
        _targetResource = resource;
        _currentState = UnitState.MovingToResource;
    }

    public void SetDestinationToBuildBase(Vector3 position, Action<IUnitController> onBaseBuilt)
    {
        _destination = position;
        _onBaseBuilt = onBaseBuilt;
        _currentState = UnitState.MovingToBuildBase;
        IsAvailable = false;
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

            case UnitState.MovingToBuildBase:
                TransitionToTarget(_destination, UnitState.BuildingBase);

                break;

            case UnitState.BuildingBase:
                BuildBase();

                break;
        }
    }

    private void BuildBase()
    {
        _onBaseBuilt?.Invoke(this);
        _onBaseBuilt = null;

        _currentState = UnitState.Waiting;
    }

    private void TransitionToTarget(Vector3 targetPosition, UnitState nextState)
    {
        if (_moveTweener == null || _moveTweener.IsActive() == false)
        {
            float distance = Vector3.Distance(_unit.transform.position, targetPosition);
            float duration = distance / _unitSpeed;

            _moveTweener = _unit.transform.DOMove(targetPosition, duration)
                .SetEase(Ease.Linear)
                .OnComplete(() =>
                {
                    _currentState = nextState;
                    _moveTweener = null;
                });
        }

        if (_rotateTweener == null || _rotateTweener.IsActive()== false)
        {
            Vector3 direction = targetPosition - _unit.transform.position;

            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);

                _rotateTweener = _unit.transform.DORotateQuaternion(targetRotation, _rotationSpeed)
                    .SetEase(Ease.Linear)
                    .OnComplete(() =>
                    {
                        _rotateTweener = null;
                    });
            }
        }
    }
}
