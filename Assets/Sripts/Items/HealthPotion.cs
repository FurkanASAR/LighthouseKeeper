using UnityEngine;

public class HealthPotion : MonoBehaviour, IInteractable
{
    [SerializeField] private ItemDefinitionsSO ItemDefinitionsSO;

    public Vector3 InteractionPoint => transform.position;
    private Inventory playerInventory;

    private void Start()
    {
        playerInventory = GameManager.Instance.GetPlayer().GetInventory();
    }

    public void Collect()
    {
        Debug.Log(playerInventory);
        Debug.Log(ItemDefinitionsSO);
        playerInventory.AddItem(ItemDefinitionsSO);
    }

    public void Interact()
    {
        Collect();
    }
}
