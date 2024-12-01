using System;
using UnityEngine;

public class ResourcesKeeper : MonoBehaviour
{
    public event Action ResourceChange;

    public int WoodCount { get; private set; }
    public int StoneCount { get; private set; }

    public void Add(ResourceType resourceType, int amount)
    {
        switch (resourceType)
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

    //Для следующей части задания
    //public bool SpendResource(ResourceType resourceType, int amount)
    //{
    //    switch (resourceType)
    //    {
    //        case ResourceType.Wood:
    //            if (WoodCount >= amount)
    //            {
    //                WoodCount -= amount;
    //                ResourceChange?.Invoke();

    //                return true;
    //            }

    //            break;

    //        case ResourceType.Stone:
    //            if (StoneCount >= amount)
    //            {
    //                StoneCount -= amount;
    //                ResourceChange?.Invoke();

    //                return true;
    //            }

    //            break;
    //    }

    //    return false;
    //}
}