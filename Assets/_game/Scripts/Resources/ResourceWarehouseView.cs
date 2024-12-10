using TMPro;
using UnityEngine;

[RequireComponent(typeof(ResourcesKeeper))]
public class ResourceWarehouseView : MonoBehaviour
{
    [SerializeField] private TextMeshPro _woodText;
    [SerializeField] private TextMeshPro _stoneText;

    private ResourcesKeeper _resourcesKeeper;

    private void Awake()
    {
        _resourcesKeeper = GetComponent<ResourcesKeeper>();
    }

    private void OnEnable()
    {
        UpdateText();

        _resourcesKeeper.ResourceChange += UpdateText;
    }

    private void OnDisable()
    {
        _resourcesKeeper.ResourceChange -= UpdateText;
    }

    private void UpdateText()
    {
        _woodText.text = $"{_resourcesKeeper.WoodCount}";
        _stoneText.text = $" {_resourcesKeeper.StoneCount}";
    }
}