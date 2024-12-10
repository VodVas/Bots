using UnityEngine;

public class RandomPositionProvider : IPositionProvider
{
    private readonly Collider _spawnAreaCollider;
    private readonly float _spawnHeight;
    private readonly float _offsetX;
    private readonly float _offsetZ;
    private readonly float _searchRadius;
    private readonly int _maxAttempts;
    private readonly int _searchRadiusMultiplier = 1000;

    public RandomPositionProvider(Collider planeCollider, float spawnHeight, float offsetX, float offsetZ, int maxAttempts, float searchRadius)
    {
        _spawnAreaCollider = planeCollider;
        _spawnHeight = spawnHeight;
        _offsetX = offsetX;
        _offsetZ = offsetZ;
        _maxAttempts = maxAttempts;
        _searchRadius = searchRadius;
    }

    public Vector3 GetPosition()
    {
        Vector3 randomPoint = GetRandomPointOnPlane();
        Vector3 safePosition = GetSafePosition(randomPoint);

        return safePosition + Vector3.up * _spawnHeight;
    }

    private Vector3 GetRandomPointOnPlane()
    {
        float randomX = Random.Range(_spawnAreaCollider.bounds.min.x, _spawnAreaCollider.bounds.max.x);
        float randomZ = Random.Range(_spawnAreaCollider.bounds.min.z, _spawnAreaCollider.bounds.max.z);

        return new Vector3(randomX, 0f, randomZ);
    }

    private Vector3 GetSafePosition(Vector3 targetPosition)
    {
        for (int attempts = 0; attempts < _maxAttempts; attempts++)
        {
            Collider[] hitColliders = Physics.OverlapSphere(targetPosition, _searchRadius / _searchRadiusMultiplier);

            if (hitColliders.Length == 0)
            {
                return targetPosition;
            }

            float randomOffsetX = Random.Range(-_offsetX, _offsetX);
            float randomOffsetZ = Random.Range(-_offsetZ, _offsetZ);

            targetPosition.x += randomOffsetX;
            targetPosition.z += randomOffsetZ;
        }

        return Vector3.zero;
    }
}