using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationPlayer : MonoBehaviour
{
    private const string IsMoving = "isMoving";

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

            if (sqrDistanceMoved > _distanceThreshold)
            {
                _animator.SetBool(IsMoving, true);
            }
            else
            {
                _animator.SetBool(IsMoving, false);
            }

            _previousPosition = transform.position;
        }
    }
}