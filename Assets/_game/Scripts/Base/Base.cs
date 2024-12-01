using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField] private ObjectActivator _woodStorageActivator;
    [SerializeField] private ObjectActivator _stoneStorageActivator;
    [SerializeField] private ResourcesKeeper _resourcesKeeper;
    [SerializeField] private int _woodAmount = 1;
    [SerializeField] private int _stoneAmount = 1;

    public void CollectResource(IResourceble resource)
    {
        switch (resource.ResourceType)
        {
            case ResourceType.Wood:
                AddResources(_woodStorageActivator, ResourceType.Wood, _woodAmount);

                break;

            case ResourceType.Stone:
                AddResources(_stoneStorageActivator, ResourceType.Stone, _stoneAmount);

                break;
        }
    }

    private void AddResources(ObjectActivator objectActivator, ResourceType type, int amount)
    {
        objectActivator.ActivateNextObject();

        _resourcesKeeper.Add(type, amount);
    }
}