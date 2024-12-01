using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationPlayer : MonoBehaviour
{
    private const string isMoving = "isMoving";

    [SerializeField] private float _delay = 0.2f;

    private Transform _referencePoint;
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        StartCoroutine(CheckingDistance());
    }

    public void SetReferencePoint(Transform referencePoint)
    {
        _referencePoint = referencePoint;
    }

    private IEnumerator CheckingDistance()
    {
        var wait = new WaitForSeconds(_delay);

        while (true)
        {
            float distanceMoved1 = Vector3.Distance(transform.position, _referencePoint.transform.position);

            yield return wait;

            float distanceMoved2 = Vector3.Distance(transform.position, _referencePoint.transform.position);

            if (distanceMoved1 == distanceMoved2)
            {
                _animator.SetBool(isMoving, false);
            }
            else
            {
                _animator.SetBool(isMoving, true);
            }
        }
    }
}