using Zenject;

public class StoneSpawner : BaseResourcesSpawner<Stone>
{
    [Inject]
    protected override void Construct(Stone stonePrefab, IPositionProvider positionProvider)
    {
        Spawner = new Spawner<Stone>(stonePrefab, positionProvider);
    }
}