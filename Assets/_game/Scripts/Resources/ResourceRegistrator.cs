using System.Collections.Generic;
using UnityEngine;

public class ResourceRegistrator : MonoBehaviour
{
    private readonly HashSet<IResourceble> _processedResources = new HashSet<IResourceble>();

    public bool IsProcessed(IResourceble resource)
    {
        return _processedResources.Contains(resource);
    }

    public void AddProcessed(IResourceble resource)
    {
        _processedResources.Add(resource);
    }

    public void RemoveProcessed(IResourceble resource)
    {
        _processedResources.Remove(resource);
    }
}