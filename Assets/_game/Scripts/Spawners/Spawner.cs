using UnityEngine;
using UnityEngine.Pool;

public class Spawner<T> where T : MonoBehaviour, IDeathEvent
{
    private T _objectPrefab;
    private IPositionProvider _positionProvider;
    private ObjectPool<T> _objectPool;

    public Spawner(T objectPrefab, IPositionProvider positionProvider)
    {
        _objectPrefab = objectPrefab;
        _positionProvider = positionProvider;

        _objectPool = new ObjectPool<T>(CreateObject, OnGetFromPool, OnReleaseToPool, OnDestroyPoolObject, true, 10, 15);
    }

    public void ClearPool()
    {
        _objectPool.Clear();
    }

    public void SpawnObject()
    {
        _objectPool.Get();
    }

    private T CreateObject()
    {
        Vector3 spawnPosition = _positionProvider.GetPosition();

        return Object.Instantiate(_objectPrefab, spawnPosition, Quaternion.identity);
    }

    private void OnGetFromPool(T obj)
    {
        Vector3 spawnPosition = _positionProvider.GetPosition();

        obj.transform.position = spawnPosition;
        obj.gameObject.SetActive(true);
        obj.Dead += HandleObjectDeath;
    }

    private void OnReleaseToPool(T obj)
    {
        obj.Dead -= HandleObjectDeath;
        obj.gameObject.SetActive(false);
    }

    private void HandleObjectDeath(IDeathEvent deadObject)
    {
        _objectPool.Release((T)deadObject);
    }

    private void OnDestroyPoolObject(T obj)
    {
        if (obj != null)
        {
            Object.Destroy(obj.gameObject);
        }
    }
}