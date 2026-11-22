using UnityEngine;

public class HealthPotion : MonoBehaviour, IInteractable
{
    [SerializeField] private ItemDefinitionsSO ItemDefinitionsSO;

    public Vector3 InteractionPoint => transform.position;

    public string InteractPrompt => ItemDefinitionsSO.itemName;  

    public void Interact(GameObject interactor)
    {
        if(interactor.TryGetComponent(out Player player))
        {
            Inventory playerInventory = player.GetInventory();
            playerInventory.TryAddItem(ItemDefinitionsSO);
        }
    }
}
