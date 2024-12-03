using System.Collections;
using UnityEngine;

public abstract class BaseResourcesSpawner<T> : MonoBehaviour where T : MonoBehaviour, IDeathEvent
{
    [SerializeField] protected float Delay;

    protected Spawner<T> Spawner;

    protected abstract void Construct(T prefab, IPositionProvider positionProvider);

    private void Start()
    {
        StartCoroutine(DelayAndSpawn());
    }

    private void OnDisable()
    {
        Spawner.ClearPool();
    }

    private IEnumerator DelayAndSpawn()
    {
        var wait = new WaitForSeconds(Delay);

        while (true)
        {
            Spawner.SpawnObject();

            yield return wait;
        }
    }
}