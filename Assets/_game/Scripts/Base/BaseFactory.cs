using UnityEngine;
using Zenject;

public class BaseFactory : IBaseFactory
{
    private const string RotationX = "RotationX";
    private const string RotationY = "RotationY";
    private const string RotationZ = "RotationZ";

    [Inject] private DiContainer _container;
    [Inject] private Base _basePrefab;

    [Inject(Id = RotationX)] private float _rotationX;
    [Inject(Id = RotationY)] private float _rotationY;
    [Inject(Id = RotationZ)] private float _rotationZ;

    private Quaternion _baseRotation;

    public Base Create(Vector3 position, Quaternion rotation)
    {
        _baseRotation = Quaternion.Euler(_rotationX, _rotationY, _rotationZ);

        return _container.InstantiatePrefabForComponent<Base>(_basePrefab, position, _baseRotation, null);
    }
}