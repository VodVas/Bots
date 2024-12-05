using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationPlayer : MonoBehaviour
{
    private readonly int _isMoving = Animator.StringToHash("isMoving");

    [SerializeField] private float _delay = 0.2f;
    [SerializeField] private float _distanceThreshold = 0.0001f;

    private Animator _animator;
    private Vector3 _previousPosition;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _previousPosition = transform.position;

        StartCoroutine(CheckingMovement());
    }

    private IEnumerator CheckingMovement()
    {
        var wait = new WaitForSeconds(_delay);

        while (true)
        {
            yield return wait;

            float sqrDistanceMoved = (transform.position - _previousPosition).sqrMagnitude;

            _animator.SetBool(_isMoving, sqrDistanceMoved > _distanceThreshold);

            _previousPosition = transform.position;
        }
    }
}