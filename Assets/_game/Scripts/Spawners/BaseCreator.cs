using UnityEngine;
using Zenject;

public class BaseCreator : MonoBehaviour
{
    [Inject] private IBaseFactory _factory;

    public void SpawnNew(Vector3 position, Quaternion quaternion)
    {
        _factory.Create(position, quaternion);
    }
}