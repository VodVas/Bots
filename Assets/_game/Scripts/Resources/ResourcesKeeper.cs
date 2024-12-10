using System;
using UnityEngine;

public class ResourcesKeeper : MonoBehaviour
{
    public event Action ResourceChange;
    public event Action CanCreateUnit;

    public int WoodCount { get; private set; } = 0;
    public int StoneCount { get; private set; } = 0;

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

        CheckCountForUnit();
    }

    private void CheckCountForUnit()
    {
        if (WoodCount >= 3 && StoneCount >= 3)
        {
            CanCreateUnit?.Invoke();
        }
    }

    public void Subtract(int woodAmount, int stoneAmount)
    {
        WoodCount -= woodAmount;
        StoneCount -= stoneAmount;

        ResourceChange?.Invoke();
    }

    public void Reset()
    {
        WoodCount = 0;
        StoneCount = 0;

        ResourceChange?.Invoke();
    }
}