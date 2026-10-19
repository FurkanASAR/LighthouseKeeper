using UnityEngine;
using System.Collections.Generic;

public class Inventory
{
    private List<ItemDefinitionsSO> itemList;

    public Inventory()
    {
        itemList = new List<ItemDefinitionsSO>();

        Debug.Log("Inventory created.");

        ItemDefinitionsSO woodItem = ScriptableObject.CreateInstance<ItemDefinitionsSO>();
        woodItem.isStackable = true;
        woodItem.itemName = "Wood";
        woodItem.itemID = 1;
        woodItem.maxStackSize = 5;
        woodItem.itemType = ItemDefinitionsSO.ItemType.Wood;

        AddItem(woodItem);
        AddItem(woodItem);
        AddItem(woodItem);
        AddItem(woodItem);
        AddItem(woodItem);
        AddItem(woodItem);
    }

    public void AddItem(ItemDefinitionsSO item)
    {
        if (item == null)
        {
            Debug.LogWarning("Attempted to add a null item to the inventory.");
            return;
        }
        if (item.isStackable)
        {
            var existingItem = itemList.Find(i => i.itemID == item.itemID);
            if (existingItem != null)
            {
                // Logic for stacking items can be added here
                Debug.Log($"Stacking item: {item.itemName}");
                return;
            }
        }
        itemList.Add(item);
        Debug.Log($"Added item: {item.itemName}");
    }









}
