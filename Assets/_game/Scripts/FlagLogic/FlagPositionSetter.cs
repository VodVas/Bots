using System.Collections;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Base))]
public class FlagPositionSetter : MonoBehaviour
{
    private const string FlagPrefab = "FlagPrefab";

    [SerializeField] private float _yOffset = 0.01f;

    [Inject(Id = FlagPrefab)] private GameObject _flagPrefab;
    [Inject] private DiContainer _container;

    private Flag _currentFlag;
    private Base _base;
    private bool _isPlacing = false;

    private void Awake()
    {
        _base = GetComponent<Base>();
    }

    private void OnMouseDown()
    {
        if (!_isPlacing)
        {
            _isPlacing = true;

            StartCoroutine(ProcessPlacement());
        }
    }

    private IEnumerator ProcessPlacement()
    {
        while (true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    if (hit.collider.TryGetComponent(out Plane _))
                    {
                        Vector3 flagPosition = new Vector3(hit.point.x, hit.point.y + _yOffset, hit.point.z); 

                        Set(flagPosition);

                        break;
                    }
                }
            }

            yield return null;
        }

        _isPlacing = false;
    }

    private void Set(Vector3 position)
    {
        if (_currentFlag == null)
        {
            GameObject flagObject = _container.InstantiatePrefab(_flagPrefab, position, Quaternion.identity, null);

            _currentFlag = flagObject.GetComponent<Flag>();
        }
        else
        {
            _currentFlag.SetPosition(position);
        }

        _base.PlaceFlag(_currentFlag);
    }
}