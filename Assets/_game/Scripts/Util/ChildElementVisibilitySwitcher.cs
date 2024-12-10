using UnityEngine;

public class ChildElementVisibilitySwitcher : MonoBehaviour
{
    private Transform _parentTransform;
    private int _lastActiveIndex = -1;

    private void Awake()
    {
        _parentTransform = transform;
    }

    public void ActivateFirstNonActiveObject()
    {
        for (int i = 0; i < _parentTransform.childCount; i++)
        {
            Transform child = _parentTransform.GetChild(i);

            if (child.gameObject.activeSelf == false)
            {
                child.gameObject.SetActive(true);

                _lastActiveIndex = i;

                return;
            }
        }
    }

    public void DeactivateLastObject(int count)
    {
        int deactivatedCount = 0;

        for (int i = _lastActiveIndex; i >= 0 && deactivatedCount < count; i--)
        {
            Transform child = _parentTransform.GetChild(i);

            if (child.gameObject.activeSelf)
            {
                child.gameObject.SetActive(false);
                deactivatedCount++;

                if (deactivatedCount > 0)
                {
                    _lastActiveIndex--;
                }
            }
        }
    }

    public void ResetActivations()
    {
        for (int i = 0; i < _parentTransform.childCount; i++)
        {
            Transform child = _parentTransform.GetChild(i);

            if (child.gameObject.activeSelf == true)
            {
                child.gameObject.SetActive(false);
            }
        }

        _lastActiveIndex = -1;
    }
}