using UnityEngine;

public class Picker : MonoBehaviour
{
    [SerializeField] private Transform _holdPoint;

    private UnitMover _unitMover;

    public PickingObject CurrentObject { get; private set; }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PickingObject pickingObject) && pickingObject.IsPickedUp == false)
        {
            if (pickingObject.TryGetComponent(out IResourceble resourceble) && resourceble == _unitMover.TargetResource)
            {
                CurrentObject = pickingObject;
                CurrentObject.PickUp(_holdPoint, _holdPoint.transform.position);
            }
        }
    }

    public void SetUnitController(UnitMover unitMover)
    {
        _unitMover = unitMover;
    }

    public void DropCurrentObject()
    {
        if (CurrentObject != null)
        {
            CurrentObject.Drop();
            CurrentObject = null;
        }
    }
}