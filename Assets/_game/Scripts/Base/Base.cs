using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField] private List<ResourceStorage> _resourceStorages;
    [SerializeField] private ResourcesKeeper _resourcesKeeper;

    public void ActivateStorageResource(IResourceble resource)
    {
        foreach (var storage in _resourceStorages)
        {
            if (storage.Type == resource.Type)
            {
                ViewWarehouseResources(storage.Activator, storage.Type, storage.Amount);

                break;
            }
        }
    }

    private void ViewWarehouseResources(ObjectActivator objectActivator, ResourceType type, int amount)
    {
        objectActivator.ActivateNextObject();
        _resourcesKeeper.Add(type, amount);
    }
}