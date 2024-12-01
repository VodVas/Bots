using System;

public interface IDeathEvent
{
    public event Action<IDeathEvent> Dead;

    public void Die();
}