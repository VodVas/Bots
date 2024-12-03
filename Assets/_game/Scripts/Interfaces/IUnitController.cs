using UnityEngine;

public interface IUnitController
{
    public bool IsAvailable { get; }
    public void SetDestinationToResource(Vector3 position, IResourceble resource);
}