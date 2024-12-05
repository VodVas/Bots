using UnityEngine;

public class ObjectActivator : MonoBehaviour
{
    private Transform _parentTransform;
    private int _currentIndex = 0;

    private void Awake()
    {
        _parentTransform = transform;
    }

    public void ActivateNextObject()
    {
        int childCount = _parentTransform.childCount;

        if (_currentIndex < childCount)
        {
            Transform child = _parentTransform.GetChild(_currentIndex);

            if (child.gameObject.activeSelf == false)
            {
                child.gameObject.SetActive(true);

                _currentIndex++;
            }
        }
    }
}