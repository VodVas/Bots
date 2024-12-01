using System;
using UnityEngine;

public class Wood : MonoBehaviour, IDeathEvent, IResourceble
{
    public event Action<IDeathEvent> Dead;

    public ResourceType ResourceType => ResourceType.Wood;

    public void Die()
    {
        Dead?.Invoke(this);
    }
}