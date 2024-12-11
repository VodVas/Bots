using UnityEngine;
using Zenject;

public class BaseFactoryInstaller : MonoInstaller
{
    private const string RotationX = "RotationX";
    private const string RotationY = "RotationY";
    private const string RotationZ = "RotationZ";

    [SerializeField] private Base _basePrefab;
    [SerializeField] private float _rotationX = 270f;
    [SerializeField] private float _rotationY = 270f;
    [SerializeField] private float _rotationZ = 270f;

    public override void InstallBindings()
    {
        Container.Bind<Base>()
            .FromInstance(_basePrefab)
            .AsTransient();

        Container.Bind<float>().WithId(RotationX).FromInstance(_rotationX).AsTransient();
        Container.Bind<float>().WithId(RotationY).FromInstance(_rotationY).AsTransient();
        Container.Bind<float>().WithId(RotationZ).FromInstance(_rotationZ).AsTransient();

        Container.Bind<IBaseFactory>()
            .To<BaseFactory>()
            .AsSingle();
    }
}