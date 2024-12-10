using System.Collections.Generic;
using UnityEngine;

public class UnitRepository : MonoBehaviour
{
    private List<IUnitController> _units = new List<IUnitController>();

    public int UnitCount => _units.Count;

    private void Update()
    {
        UpdateMovers();
    }

    public void RegisterUnit(IUnitController unit)
    {
        if (_units.Contains(unit) == false)
        {
            _units.Add(unit);
        }
    }

    public void UnregisterUnit(IUnitController unit)
    {
        if (_units.Contains(unit))
        {
            _units.Remove(unit);
        }
    }

    public void TransferUnitToBase(IUnitController unit, Base newBase)
    {
        UnregisterUnit(unit);

        newBase.RegisterUnit(unit);
    }

    public IUnitController GetAvailableUnit()
    {
        foreach (var unit in _units)
        {
            if (unit.IsAvailable)
            {
                return unit;
            }
        }

        return null;
    }

    private void UpdateMovers()
    {
        for (int i = _units.Count - 1; i >= 0; i--)
        {
            if (_units[i] is UnitMover unitMover)
            {
                unitMover.Update();
            }
        }
    }
}