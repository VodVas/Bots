using UnityEngine;

public class PickingObject : MonoBehaviour
{
    public bool IsPickedUp { get; private set; } = false;

    public void PickUp(Transform parent, Vector3 holdPosition)
    {
        if (IsPickedUp == false)
        {
            transform.SetParent(parent);
            transform.position = holdPosition;
            IsPickedUp = true;
        }
    }

    public void Drop()
    {
        IsPickedUp = false;
        transform.SetParent(null);
    }
}