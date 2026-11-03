using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class InventorySlot_UI : MonoBehaviour, IPointerClickHandler
{


    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI itemAmountText;
    private InventorySlot inventorySlot;
    private Inventory_UI inventory_UI;

 

    public void SetInventorySlot(InventorySlot inventorySlot)
    {
        this.inventorySlot = inventorySlot;
        RefreshUI();
    }

    public void SetUIInventory(Inventory_UI inventory_UI)
    {
        this.inventory_UI = inventory_UI;        
    }
    private void RefreshUI()
    {
        if (inventorySlot == null)
            return;

        image.sprite = inventorySlot.itemDefinitionsSO.itemSprite;
        Debug.Log("inventory slot itemAmount: " + inventorySlot.itemAmount);
        itemAmountText.text = inventorySlot.itemAmount.ToString();        

    }

    public void DestroySelf()
    {        
        Destroy(gameObject);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            Debug.Log(inventorySlot.itemDefinitionsSO.itemName);    


        }

        else if (eventData.button == PointerEventData.InputButton.Right)
        {            
            DropItem();
        }
    }


    private void DropItem()
    {
        inventorySlot.DecreaseAmount();     
    }

    private void UseItem()
    {

    }
}
