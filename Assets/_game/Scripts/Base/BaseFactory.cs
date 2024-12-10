using UnityEngine;
using Zenject;

public class BaseFactory : IBaseFactory
{
    [Inject] private DiContainer _container;
    [Inject] private Base _basePrefab;

    private Quaternion _baseRotation;
    private float _rotationX = 270f;

    public Base Create(Vector3 position, Quaternion rotation)
    {
        _baseRotation = Quaternion.Euler(_rotationX, 0, 0);

        return _container.InstantiatePrefabForComponent<Base>(_basePrefab, position, _baseRotation, null);
    }
}