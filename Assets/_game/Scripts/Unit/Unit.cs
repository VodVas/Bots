using UnityEngine;

public class Unit : MonoBehaviour
{
    private UnitNavigator _controller;
    private Picker _picker;

    private void Awake()
    {
        _picker = GetComponent<Picker>();
    }

    public void Init(UnitNavigator controller)
    {
        _controller = controller;
        _picker.SetUnitController(controller);
    }

    public IResourceble GetCurrentResource()
    {
        PickingObject currentPickingObject = _picker.CurrentObject;

        if (currentPickingObject != null)
        {
            return currentPickingObject.GetComponent<IResourceble>();
        }

        return null;
    }

    public void DeliverResource()
    {
        PickingObject currentPickingObject = _picker.CurrentObject;

        if (currentPickingObject != null)
        {
            _picker.DropCurrentObject();

            if (currentPickingObject.TryGetComponent(out IDeathEvent deathEvent))
            {
                deathEvent.Die();
            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Base mainBase))
        {
            IResourceble resource = GetCurrentResource();

            if (resource != null)
            {
                mainBase.CollectResource(resource);

                DeliverResource();
            }
        }
    }
}