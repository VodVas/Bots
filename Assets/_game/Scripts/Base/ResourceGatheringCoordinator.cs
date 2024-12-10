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

    [Inject] private ResourceRegistrator _resourceRegistrator;

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

    private List<(IResourceble, Collider)> Scan()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, _scanRadius);

        List<(IResourceble, Collider)> sortedResources = new List<(IResourceble, Collider)>();

        foreach (var collider in hitColliders)
        {
            if (collider.TryGetComponent(out IResourceble resource))
            {
                if (_resourceRegistrator.IsProcessed(resource))
                {
                    continue;
                }

                Remember(resource);

                sortedResources.Add((resource, collider));
            }
        }

        sortedResources.Sort((a, b) =>
            Vector3.Distance(transform.position, a.Item2.transform.position)
            .CompareTo(Vector3.Distance(transform.position, b.Item2.transform.position)));

        return sortedResources;
    }

    private void ProcessResourceAssignment()
    {
        var resources = Scan();

        if (resources.Count == 0) return;

        var availableUnits = _unitRepository.GetAvailableUnits();
        int unitIndex = 0;

        foreach (var (resource, collider) in resources)
        {
            if (unitIndex >= availableUnits.Count)
            {
                break;
            }

            AssignUnitToResource(resource, collider, availableUnits[unitIndex]);

            unitIndex++;
        }
    }

    private void Remember(IResourceble resource)
    {
        _resourceRegistrator.AddProcessed(resource);

        if (resource is IDeathEvent deathEventResource)
        {
            deathEventResource.Dead += OnResourceDead;
        }
    }

    private void AssignUnitToResource(IResourceble resource, Collider collider, IUnitController unit)
    {
        if (unit != null)
        {
            unit.SetDestinationToResource(collider.transform.position, resource);
        }
    }

    private void OnResourceDead(IDeathEvent resource)
    {
        if (resource is IResourceble resourceble)
        {
            _resourceRegistrator.RemoveProcessed(resourceble);

            resource.Dead -= OnResourceDead;
        }
    }
}