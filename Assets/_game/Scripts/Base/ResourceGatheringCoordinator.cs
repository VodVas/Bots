using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ResourceGatheringCoordinator : MonoBehaviour
{
    [SerializeField] private float _scanRadius = 5f;
    [SerializeField] private float _scanInterval = 2f;

    private UnitRepository _unitRepository;
    private ParticlePlayer _particlePlayer;
    private HashSet<IResourceble> _processedResources = new HashSet<IResourceble>();

    [Inject]
    public void Construct(UnitRepository unitRepository, ParticlePlayer particlePlayer)
    {
        _unitRepository = unitRepository;
        _particlePlayer = particlePlayer;
    }

    private void Awake()
    {
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
                if (_processedResources.Contains(resource))
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
        _processedResources.Add(resource);

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
        _processedResources.Remove(resource as IResourceble);

        resource.Dead -= OnResourceDead;
    }
}