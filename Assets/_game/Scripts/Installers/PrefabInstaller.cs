using UnityEngine;
using Zenject;

public class PrefabInstaller : MonoInstaller
{
    [SerializeField] private Wood _woodPrefab;
    [SerializeField] private Stone _stonePrefab;
    [SerializeField] private GameObject _unitPrefab;
    [SerializeField] private Base _basePrefab;

    public override void InstallBindings()
    {
        Container.Bind<Wood>()
            .FromInstance(_woodPrefab)
            .AsSingle();

        Container.Bind<Stone>()
            .FromInstance(_stonePrefab)
            .AsSingle();

        Container.Bind<GameObject>()
            .FromInstance(_unitPrefab)
            .AsTransient();

        Container.Bind<Base>()
            .FromInstance(_basePrefab)
            .AsSingle();

        Container.Bind<IBaseFactory>()
            .To<BaseFactory>()
            .AsSingle();
    }
}