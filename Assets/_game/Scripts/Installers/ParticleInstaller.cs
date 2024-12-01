using Zenject;

public class ParticleInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<ParticlePlayer>()
            .FromComponentInHierarchy()
            .AsSingle();
    }
}