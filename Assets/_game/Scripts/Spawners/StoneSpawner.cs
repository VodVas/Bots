using Zenject;

public class StoneSpawner : BaseResourcesSpawner<Stone>
{
    [Inject]
    protected override void Construct(Stone stonePrefab, IPositionProvider positionProvider)
    {
        _spawner = new Spawner<Stone>(stonePrefab, positionProvider);
    }
}