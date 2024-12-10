using Zenject;

public class StoneSpawner : BaseResourcesSpawner<Stone>
{
    [Inject]
    protected override void Construct(Stone stonePrefab, IPositionProvider positionProvider, DiContainer container)
    {
        Spawner = new Spawner<Stone>(container, stonePrefab, positionProvider);
    }
}