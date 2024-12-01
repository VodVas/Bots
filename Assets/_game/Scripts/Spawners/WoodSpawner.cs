using Zenject;

public class WoodSpawner : BaseResourcesSpawner<Wood>
{
    [Inject]
    protected override void Construct(Wood woodPrefab, IPositionProvider positionProvider)
    {
        _spawner = new Spawner<Wood>(woodPrefab, positionProvider);
    }
}