using System;
using UnityEngine;

public class ResourcesKeeper : MonoBehaviour
{
    public event Action ResourceChange;

    public int WoodCount { get; private set; }
    public int StoneCount { get; private set; }

    public void Add(ResourceType type, int amount)
    {
        switch (type)
        {
            case ResourceType.Wood:
                WoodCount += amount;

                break;

            case ResourceType.Stone:
                StoneCount += amount;

                break;
        }

        ResourceChange?.Invoke();
    }
}