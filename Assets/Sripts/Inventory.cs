using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static InventorySlot;

public class Inventory
{
    public event EventHandler OnInventoryChanged;

    private int maximumInventorySlotCount = 10;
    private int currentInventorySlotCount = 0;
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
            if (TryStacingItem(item))
            {
                OnInventoryChanged?.Invoke(this, EventArgs.Empty);
                return true;
            }
        }

        InventorySlot slot = new(item);


        return TryAddNewSlotToList(slot);
    }

    private bool TryAddNewSlotToList(InventorySlot newSlot)
    {
        if (inventorySlotList.Count >= maximumInventorySlotCount)
        {
            Debug.Log("Maximum inventory capacity reached!");
            return false;
        }

        newSlot.itemAmount++;
        newSlot.OnSlotItemZero += Slot_OnSlotItemZero;
        inventorySlotList.Add(newSlot);

        OnInventoryChanged?.Invoke(this, EventArgs.Empty);
        return true;
    }

    private void Slot_OnSlotItemZero(object sender, OnSLotItemZeroEventArgs e)
    {
        if (!inventorySlotList.Remove(e.thisSlot))
        {
            return;
        }
        OnInventoryChanged.Invoke(this, EventArgs.Empty);
    }

    private bool TryStacingItem(ItemDefinitionsSO item)
    {
        for (int i = 0; i < inventorySlotList.Count; i++)
        {
            InventorySlot currentSlot = inventorySlotList[i];
            if (currentSlot.itemID != item.itemID)
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
                return true;
            }
        }
        return false;
    }

    public int GetInventoryCount()
    {
        return inventorySlotList.Count;
    }


    public List<InventorySlot> GetInventorySlotList()
    {
        return inventorySlotList;
    }

}

public class InventorySlot
{

    public event EventHandler<OnSLotItemZeroEventArgs> OnSlotItemZero;
    public class OnSLotItemZeroEventArgs : EventArgs
    {
        public InventorySlot thisSlot;
    }
    public ItemDefinitionsSO itemDefinitionsSO;
    public int itemID;
    public InventorySlot(ItemDefinitionsSO itemDefinitionsSO)
    {
        this.itemDefinitionsSO = itemDefinitionsSO;
        this.itemID = itemDefinitionsSO.itemID;

    }

    public int itemAmount = 0;

    public void FireItemZeroEVent()
    {
        OnSlotItemZero.Invoke(this, new OnSLotItemZeroEventArgs
        {
            thisSlot = this
        });

        Debug.Log("Event fired!");

        Debug.Log(this);
    }

    public void DecreaseAmount()
    {
        if (itemAmount <= 0)
        {
            return;
        }

        itemAmount--;

        if (itemAmount == 0)
        {
            OnSlotItemZero.Invoke(this, new OnSLotItemZeroEventArgs
            {
                thisSlot = this
            });

        }




    }
}








