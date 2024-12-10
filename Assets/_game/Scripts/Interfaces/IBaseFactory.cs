using UnityEngine;

public interface IBaseFactory
{
    Base Create(Vector3 position, Quaternion rotation);
}