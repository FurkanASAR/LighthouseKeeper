using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Inventory
{
    public event EventHandler<OnInventoryChangedEventArgs> OnInventoryChanged;
    public class OnInventoryChangedEventArgs : EventArgs
    {
        public OnInventoryChangedEventArgs(InventorySlot slot)
        {
            this.Slot = slot;
        }
        public InventorySlot Slot { get; private set; }
    }

    private int maximumInventorySlotCount = 5;
    private List<InventorySlot> inventorySlotList;

    public Inventory()
    {
        inventorySlotList = new List<InventorySlot>();
    }

    public bool TryAddItem(ItemDefinitionsSO item)
    {
        if (item == null)
        {
            Debug.LogWarning("Attempted to add a null item to the inventory.");
            return false;
        }

        if (item.isStackable)
        {
            if (TryStackingItem(item))
            {
                return true;
            }
        }

        return TryAddNewItem(item);
    }

    private bool TryAddNewItem(ItemDefinitionsSO item)
    {
        if (inventorySlotList.Count >= maximumInventorySlotCount)
        {
            Debug.Log("Maximum inventory capacity reached!");
            return false;
        }
        InventorySlot newSlot = new(item);

        inventorySlotList.Add(newSlot);

        OnInventoryChanged?.Invoke(this, new OnInventoryChangedEventArgs(newSlot));
        return true;
    }

    private bool TryStackingItem(ItemDefinitionsSO item)
    {
        for (int i = 0; i < inventorySlotList.Count; i++)
        {
            InventorySlot currentSlot = inventorySlotList[i];
            if (currentSlot.itemSO.itemID != item.itemID)
            {
                continue;
            }

            if (currentSlot.itemAmount >= item.maxStackSize)
            {
                continue;
            }
            else if (currentSlot.itemAmount < item.maxStackSize)
            {
                currentSlot.itemAmount++;
                OnInventoryChanged?.Invoke(this, new OnInventoryChangedEventArgs(currentSlot));
                return true;
            }
        }
        return false;
    }

    public int GetInventoryCount()
    {
        return inventorySlotList.Count;
    }

    public void RemoveItem(object sender, OnInventoryChangedEventArgs e, int removeAmount)
    {
        InventorySlot slot = e.Slot;

        if (slot == null)
        {
            Debug.LogWarning("Attempted to drop an item from a null inventory slot.");
            return;
        }

        slot.itemAmount -= removeAmount;
        int remainingAmount = slot.itemAmount;

        if (remainingAmount < 0)
        {
            slot.itemAmount = 0;
            remainingAmount *= -1;
        }

        if (slot.itemAmount == 0)
        {
            slot.DestroySlot();
            inventorySlotList.Remove(slot);

        }
        OnInventoryChanged?.Invoke(this, e);
        return;
    }


    public void RemoveNeededItems(List<NeededItem> neededItems)
    {
        foreach (var neededItem in neededItems)
        {
            ItemDefinitionsSO itemDefinition = neededItem.itemDefinition;
            int requiredAmount = neededItem.requiredAmount;
            int remainingAmountToRemove = requiredAmount;

            foreach (var slot in inventorySlotList.ToList())
            {
                if (slot.itemSO == itemDefinition)
                {
                    RemoveItem(this, new OnInventoryChangedEventArgs(slot), remainingAmountToRemove);
                    if(remainingAmountToRemove <= 0)
                    {
                        break;
                    }
                }
            }
        }
        inventorySlotList.RemoveAll(slot => slot.itemAmount <= 0);
    }

    public void RemoveItem(object sender, OnInventoryChangedEventArgs e, int removeAmount, out int remainingAmountToRetrun)
    {
        InventorySlot slot = e.Slot;

        if (slot == null)
        {
            Debug.LogWarning("Attempted to drop an item from a null inventory slot.");
            remainingAmountToRetrun = 0;
            return;
        }

        slot.itemAmount -= removeAmount;
        int remainingAmount = slot.itemAmount;

        if (remainingAmount < 0)
        {
            slot.itemAmount = 0;
        }

        slot.DestroySlot();
        inventorySlotList.Remove(slot);

        OnInventoryChanged?.Invoke(this, e);

        remainingAmountToRetrun = remainingAmount;
        return;
    }

    public bool HasAllItems(List<NeededItem> neededItems)
    {
        foreach (var neededItem in neededItems)
        {
            if (!HasItem(neededItem))
            {
                return false;
            }
        }
        return true;
    }

    private bool HasItem(NeededItem neededItem)
    {
        ItemDefinitionsSO neededItemDefinitionSo = neededItem.itemDefinition;
        int requiredAmount = neededItem.requiredAmount;

        int currentAmount = 0;

        foreach (var slot in inventorySlotList)
        {
            if (slot.itemSO == neededItemDefinitionSo)
            {
                currentAmount += slot.itemAmount;
            }
        }

        return currentAmount >= requiredAmount;
    }
}
public class InventorySlot
{
    public InventorySlot(ItemDefinitionsSO itemSO)
    {
        this.itemSO = itemSO;
        itemAmount = 1;
    }
    public ItemDefinitionsSO itemSO { get; private set; }
    public int itemAmount;

    public void DestroySlot()
    {
        itemSO = null;
        itemAmount = 0;
    }
}