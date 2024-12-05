using TMPro;
using UnityEngine;

public class ResourceWarehouseView : MonoBehaviour
{
    [SerializeField] private TextMeshPro _woodText;
    [SerializeField] private TextMeshPro _stoneText;
    [SerializeField] private ResourcesKeeper _resourcesKeeper;

    private void OnEnable()
    {
        UpdateResourceText();

        _resourcesKeeper.ResourceChange += UpdateResourceText;
    }

    private void OnDisable()
    {
        _resourcesKeeper.ResourceChange -= UpdateResourceText;
    }

    private void UpdateResourceText()
    {
        _woodText.text = $"{_resourcesKeeper.WoodCount}";
        _stoneText.text = $" {_resourcesKeeper.StoneCount}";
    }
}