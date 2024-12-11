using UnityEngine;
using Zenject;

public class FlagInstaller : MonoInstaller
{
    private const string FlagPrefab = "FlagPrefab";

    [SerializeField] private Flag _flagPrefab;

    public override void InstallBindings()
    {
        Container.Bind<Flag>()
            .WithId(FlagPrefab)
            .FromInstance(_flagPrefab)
            .AsSingle();
    }
}