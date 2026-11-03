using UnityEngine;
using System.Collections.Generic;
using System;
using Unity.VisualScripting;



public class Inventory_UI : MonoBehaviour
{
    public event EventHandler OnInventoryRefreshed;


    [SerializeField] private Transform inventorySlot_UI;
    [SerializeField] private Transform canvasTransform;
    [SerializeField] private Player player;

    
    private Inventory playerInventory;
    private int paddingX = 40;
    private int spaceX = 30;

    private int paddingY = 20;
    private int spaceY = 20;
    public List<Transform> inventorySlotList_UI = new();
    private List<InventorySlot> inventorySlotList = new();    


    private void Start()
    {
        playerInventory = player.GetInventory();
        playerInventory.OnInventoryChanged += PlayerInventory_OnInventoryChanged;

        RefreshInventory();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            DestroyCurrentSlots();
        }
    }

    private void PlayerInventory_OnInventoryChanged(object sender, EventArgs e)
    {
        RefreshInventory();
    }
    private void RefreshInventory()
    {
        DestroyCurrentSlots();
        CreateNewSLot();        

        OnInventoryRefreshed?.Invoke(this, EventArgs.Empty);
    }
    private void DestroyCurrentSlots()
    {
        for (int i = 0; i < inventorySlotList_UI.Count; i++)
        {

            inventorySlotList_UI[i].GetComponent<InventorySlot_UI>().DestroySelf();

        }
        inventorySlotList_UI.Clear();
    }
    private void CreateNewSLot()
    {
        inventorySlotList = playerInventory.GetInventorySlotList();

        int x = 0;
        int y = 0;
        int itemCellSize = 120;

        int inventorySlotCount = playerInventory.GetInventoryCount();        

        for (int i = 0; i < inventorySlotCount; i++)
        {            
            Transform newSlotUITransform = Instantiate(inventorySlot_UI, canvasTransform);
            newSlotUITransform.GetComponent<InventorySlot_UI>().SetUIInventory(this);
            newSlotUITransform.GetComponent<InventorySlot_UI>().SetInventorySlot(inventorySlotList[i]);
            RectTransform newSlotRectTransform = newSlotUITransform.GetComponent<RectTransform>();


            newSlotRectTransform.anchoredPosition = new Vector2(paddingX + (x * itemCellSize), paddingY + (-y * itemCellSize));
            x++;
            if (x > 3)
            {
                x = 0;
                y++;
            }            
            inventorySlotList_UI.Add(newSlotUITransform);

        }


    }
}
