using System.Collections.Generic;
using Zenject;

public class UnitRepository : ITickable
{
    private readonly List<IUnitController> _units = new List<IUnitController>();

    public void RegisterUnit(IUnitController unit)
    {
        if (_units.Contains(unit) == false)
        {
            _units.Add(unit);
        }
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

    public void Tick()
    {
        foreach (var unit in _units)
        {
            if (unit is UnitMover controller)
            {
                controller.Update();
            }
        }
    }
}