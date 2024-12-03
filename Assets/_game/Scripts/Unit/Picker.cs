using UnityEngine;

public class Picker : MonoBehaviour
{
    [SerializeField] private Transform _holdPoint;

    private UnitNavigator _unitController;

    public PickingObject CurrentObject { get; private set; }

    public void SetUnitController(UnitNavigator unitController)
    {
        _unitController = unitController;
    }

    public void DropCurrentObject()
    {
        if (CurrentObject != null)
        {
            CurrentObject.Drop();
            CurrentObject = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PickingObject pickingObject) && pickingObject.IsPickedUp == false)
        {
            if (_unitController.GetTargetResource() != null &&
                pickingObject.TryGetComponent(out IResourceble resourceble) &&
                resourceble == _unitController.GetTargetResource())
            {
                CurrentObject = pickingObject;
                CurrentObject.PickUp(_holdPoint, _holdPoint.transform.position);
            }
        }
    }
}