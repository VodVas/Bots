using System;
using UnityEngine;

public interface IUnitController
{
    public bool IsAvailable { get; }
    public Vector3 Position { get; }
    public void SetDestinationToResource(Vector3 position, IResourceble resource);
    public void SetDestinationToBuildBase(Vector3 position, Action<IUnitController> onBaseBuilt);
}