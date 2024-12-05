using UnityEngine;
using Zenject;

public class PrefabInstaller : MonoInstaller
{
    [SerializeField] private Wood _woodPrefab;
    [SerializeField] private Stone _stonePrefab;

    public override void InstallBindings()
    {
        Container.Bind<Wood>()
            .FromInstance(_woodPrefab)
            .AsSingle();

        Container.Bind<Stone>()
            .FromInstance(_stonePrefab)
            .AsSingle();
    }
}