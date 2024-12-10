using UnityEngine;
using Zenject;

public class FlagInstaller : MonoInstaller
{
    private const string FlagPrefab = "FlagPrefab";

    [SerializeField] private GameObject _flagPrefab;

    public override void InstallBindings()
    {
        Container.Bind<GameObject>()
            .WithId(FlagPrefab)
            .FromInstance(_flagPrefab)
            .AsSingle();
    }
}