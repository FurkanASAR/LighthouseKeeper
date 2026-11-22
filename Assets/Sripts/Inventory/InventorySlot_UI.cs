using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static Inventory;


public class InventorySlot_UI : MonoBehaviour, IPointerClickHandler
{

    public event EventHandler<OnInventoryChangedEventArgs> OnInventorySlotClicked;
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI itemAmountText;
    private InventorySlot inventorySlot;    

    public void SetInventorySlot(InventorySlot slot)
    {
        inventorySlot = slot;
    }

    public void RefreshUI()
    {
        if (inventorySlot == null)
        {
            return;
        }

        image.sprite = inventorySlot.itemSO.itemSprite;        
        itemAmountText.text = inventorySlot.itemAmount.ToString();        

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            Debug.Log(inventorySlot.itemSO.itemName);    


        }

        else if (eventData.button == PointerEventData.InputButton.Right)
        {            
            DropItem();
        }
    }

    public InventorySlot GetInventorySlot()
    {
        return inventorySlot;
    }

    public void DestroySlot()
    {
        inventorySlot = null;
        gameObject.SetActive(false);
    }

    private void DropItem()
    {
        OnInventorySlotClicked?.Invoke(this, new OnInventoryChangedEventArgs(inventorySlot));
    }

    private void UseItem()
    {

    }
}
