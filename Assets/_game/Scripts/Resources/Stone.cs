using System;
using UnityEngine;

public class Stone : MonoBehaviour, IDeathEvent, IResourceble
{
    public event Action<IDeathEvent> Dead;

    public ResourceType ResourceType => ResourceType.Stone;

    public void Die()
    {
        Dead?.Invoke(this);
    }
}