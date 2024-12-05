using UnityEngine;
using Zenject;

public class ResourcesSpawnerInstaller : MonoInstaller
{
    [SerializeField] private Collider _planeCollider;
    [SerializeField] private float _spawnHeight = 0f;
    [SerializeField] private float _offsetX;
    [SerializeField] private float _offsetZ;
    [SerializeField] private int _maxAttempts;
    [SerializeField] private float _searchRadius = 1f;

    public override void InstallBindings()
    {
        Container.Bind<IPositionProvider>()
            .To<RandomPositionProvider>()
            .AsTransient()
            .WithArguments(_planeCollider, _spawnHeight, _offsetX, _offsetZ, _maxAttempts, _searchRadius);
    }
}