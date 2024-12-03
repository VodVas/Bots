using UnityEngine;
using Zenject;

public class UnitControlInstaller : MonoInstaller
{
    [SerializeField] private float _unitSpeed = 10f;

    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<UnitRepository>()
            .AsSingle()
            .NonLazy();

        Container.Bind<float>()
            .FromInstance(_unitSpeed)
            .AsSingle();
    }
}