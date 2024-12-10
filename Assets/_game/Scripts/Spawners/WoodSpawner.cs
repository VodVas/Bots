using Zenject;

public class WoodSpawner : BaseResourcesSpawner<Wood>
{
    [Inject]
    protected override void Construct(Wood woodPrefab, IPositionProvider positionProvider, DiContainer container)
    {
        Spawner = new Spawner<Wood>(container, woodPrefab, positionProvider);
    }
}