using UnityEngine;

public class ItemWorld : MonoBehaviour, IInteractable
{
    public Vector3 InteractionPoint => transform.position;
    public string InteractPrompt => itemSO.itemName;
    [SerializeField] private ItemDefinitionsSO itemSO;

    public void Interact(GameObject interactor)
    {
        if(interactor.TryGetComponent(out Player player))
        {
            Inventory playerInventory = player.GetInventory();
            playerInventory.TryAddItem(itemSO);
        }              
    }

    private void Awake()
    {
        MeshFilter meshFilter = GetComponentInChildren<MeshFilter>();
        MeshRenderer meshRenderer = GetComponentInChildren<MeshRenderer>();
        if (meshFilter == null)
        {
            Debug.LogError("MeshFilter component is missing.");
            return;
        }
        if (meshRenderer == null)
        {
            Debug.LogError("MeshRenderer component is missing.");
            return;
        }
        meshFilter.mesh = itemSO.mesh;
        meshRenderer.material = itemSO.material;

    }
}
