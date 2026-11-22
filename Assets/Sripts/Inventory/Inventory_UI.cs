using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.UI;


public class Inventory_UI : MonoBehaviour
{
    public event EventHandler OnInventoryRefreshed;

    [SerializeField] private Transform slotContainerLayout;   
    [SerializeField] private Player player;


    private int dropAmount = 1;

    private List<RectTransform> inventorySlotRecTransformList = new();
    private Inventory playerInventory;    

    private void Start()
    {
        playerInventory = player.GetInventory();
        playerInventory.OnInventoryChanged += PlayerInventory_OnInventoryChanged;
        
        InitializeUISlots();
    }

    private void OnDisable()
    {
        playerInventory.OnInventoryChanged -= PlayerInventory_OnInventoryChanged;
    }
    private void PlayerInventory_OnInventoryChanged(object sender, Inventory.OnInventoryChangedEventArgs e)
    {
        HandleSlotChanges(e.Slot);
    }

    private void HandleSlotChanges(InventorySlot slot)
    {
        if(slot.itemSO == null)
        {
            ClearUISLot(slot);
            return;
        }
        foreach(RectTransform rectTransform in inventorySlotRecTransformList)
        {
            InventorySlot_UI slotUI = rectTransform.GetComponent<InventorySlot_UI>();
            if (slotUI.GetInventorySlot() == slot)
            {
                slotUI.RefreshUI();                
                return;
            }
        }

        AddNewSlot(slot);
    }

    private void ClearUISLot(InventorySlot slot)
    {
        Debug.Log("Clearing UI slot");

        foreach (RectTransform rectTransform in inventorySlotRecTransformList)
        {
            InventorySlot_UI slotUI = rectTransform.GetComponent<InventorySlot_UI>();
            if (slotUI.GetInventorySlot() == slot)
            {
                slotUI.DestroySlot();
            }
        }
    }

    private void AddNewSlot(InventorySlot slot)
    {
        foreach (RectTransform rectTransform in inventorySlotRecTransformList)
        {
            if (!rectTransform.gameObject.activeSelf)
            {
                rectTransform.gameObject.SetActive(true);
                InventorySlot_UI slotUI = rectTransform.GetComponent<InventorySlot_UI>();

                slotUI.SetInventorySlot(slot);
                slotUI.RefreshUI();                

                return;
            }
        }
    }
    private void InitializeUISlots()
    {
        foreach (RectTransform rectTransform in slotContainerLayout)
        {
            inventorySlotRecTransformList.Add(rectTransform);
            rectTransform.GetComponent<Image>().sprite = null;
            InventorySlot_UI slotUI = rectTransform.GetComponent<InventorySlot_UI>();
            slotUI.OnInventorySlotClicked += (sender, e) => playerInventory.RemoveItem(sender, e, dropAmount);
            rectTransform.gameObject.SetActive(false);            
        }
    }

}
