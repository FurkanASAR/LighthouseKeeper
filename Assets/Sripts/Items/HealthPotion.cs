using UnityEngine;

public class HealthPotion : MonoBehaviour, IInteractable
{
    [SerializeField] private ItemDefinitionsSO ItemDefinitionsSO;

    public Vector3 InteractionPoint => transform.position;

    public string InteractPrompt => ItemDefinitionsSO.itemName;

    private Inventory playerInventory;

    private void Start()
    {
        playerInventory = GameManager.Instance.GetPlayer().GetInventory();
    }

    public void Collect()
    {
       
        playerInventory.TryAddItem(ItemDefinitionsSO);
    }

    public void Interact()
    {
        Collect();
    }
}
