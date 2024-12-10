using System.Collections;
using UnityEngine;
using Zenject;

public abstract class BaseResourcesSpawner<T> : MonoBehaviour where T : MonoBehaviour, IDeathEvent
{
    [SerializeField] protected float Delay;

    protected Spawner<T> Spawner;

    protected abstract void Construct(T prefab, IPositionProvider positionProvider, DiContainer container);

    private void Start()
    {
        StartCoroutine(DelayingCreate());
    }

    private void OnDisable()
    {
        Spawner.ClearPool();
    }

    private IEnumerator DelayingCreate()
    {
        var wait = new WaitForSeconds(Delay);

        while (true)
        {
            Spawner.SpawnObject();

            yield return wait;
        }
    }
}