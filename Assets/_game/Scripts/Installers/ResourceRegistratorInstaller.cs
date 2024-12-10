using UnityEngine;
using Zenject;

public class ResourceRegistratorInstaller : MonoInstaller
{
    [SerializeField] private ResourceRegistrator _resourceRegistrator;

    public override void InstallBindings()
    {
        Container.Bind<ResourceRegistrator>()
                 .FromInstance(_resourceRegistrator)
                 .AsSingle()
                 .NonLazy();
    }
}