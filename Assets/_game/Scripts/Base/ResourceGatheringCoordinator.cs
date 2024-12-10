using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(ParticlePlayer))]
[RequireComponent(typeof(UnitRepository))]
public class ResourceGatheringCoordinator : MonoBehaviour
{
    [SerializeField] private float _scanRadius = 5f;
    [SerializeField] private float _scanInterval = 2f;

    private UnitRepository _unitRepository;
    private ParticlePlayer _particlePlayer;

    [Inject] private ResourceRegistrator _resourceManager;

    private void Awake()
    {
        _particlePlayer = GetComponent<ParticlePlayer>();
        _unitRepository = GetComponent<UnitRepository>();

        StartCoroutine(ResourceAllocating());
    }

    private IEnumerator ResourceAllocating()
    {
        var wait = new WaitForSeconds(_scanInterval);

        while (true)
        {
            _particlePlayer.Play();

            ProcessResourceAssignment();

            yield return wait;
        }
    }

    private (IResourceble, Collider) Scan()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, _scanRadius);

        foreach (var collider in hitColliders)
        {
            if (collider.TryGetComponent(out IResourceble resource))
            {
                if (_resourceManager.IsProcessed(resource))
                {
                    continue;
                }

                Remember(resource);

                return (resource, collider);
            }
        }

        return (null, null);
    }

    private void ProcessResourceAssignment()
    {
        var (resource, collider) = Scan();

        if (resource != null && collider != null)
        {
            AssignUnitToResource(resource, collider);
        }
    }

    private void Remember(IResourceble resource)
    {
        _resourceManager.AddProcessed(resource);

        if (resource is IDeathEvent deathEventResource)
        {
            deathEventResource.Dead += OnResourceDead;
        }
    }

    private void AssignUnitToResource(IResourceble resource, Collider collider)
    {
        IUnitController availableUnit = _unitRepository.GetAvailableUnit();

        if (availableUnit != null)
        {
            availableUnit.SetDestinationToResource(collider.transform.position, resource);
        }
    }

    private void OnResourceDead(IDeathEvent resource)
    {
        if (resource is IResourceble resourceble)
        {
            _resourceManager.RemoveProcessed(resourceble);

            resource.Dead -= OnResourceDead;
        }
    }
}