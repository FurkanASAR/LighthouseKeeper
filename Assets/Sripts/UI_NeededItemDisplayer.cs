using System.Collections.Generic;
using UnityEngine;

public class UI_NeededItemDisplayer : MonoBehaviour
{
    [SerializeField] private GameObject neededItemsCnavas; 
    [SerializeField] private Transform neededItemDispalyer;
    [SerializeField] private Transform displayerParent;

    private List<Transform> displayedItemsList = new List<Transform>();

    private void Awake()
    {
        neededItemsCnavas.SetActive(false);
    }
    public void ShowNeededItems(List<NeededItem> neededItems)
    {
        neededItemsCnavas.SetActive(true);
        DisplayNeededItemsToFix(neededItems);
    }

    public void HideNeededItems()
    {
        neededItemsCnavas.SetActive(false);
        DestroyNeededItems();
    }
    public void DisplayNeededItemsToFix(List<NeededItem> neededItems)
    {
        foreach (NeededItem neededItem in neededItems)
        {
            Debug.Log($"Needed Item: {neededItem.itemDefinition.itemName}, Required Amount: {neededItem.requiredAmount}");

            Transform itemDisplay = Instantiate(neededItemDispalyer, displayerParent);
            itemDisplay.GetComponent<ItemDisplayerScript>().SetItemDisplay(neededItem.itemDefinition.itemSprite, neededItem.requiredAmount);
            displayedItemsList.Add(itemDisplay);
        }
    }

    private void DestroyNeededItems()
    {
        foreach (Transform item in displayedItemsList)
        {
            Destroy(item.gameObject);
        }
        displayedItemsList.Clear();
    }
}
