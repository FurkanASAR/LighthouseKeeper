using UnityEngine;

public class ItemWorld : MonoBehaviour, IInteractable
{
    public Vector3 InteractionPoint => transform.position;
    public string InteractPrompt => itemSO.itemName;
    [SerializeField] private ItemDefinitionsSO itemSO;

    private Inventory playerInventory;



    private void Start()
    {
        playerInventory = GameManager.Instance.GetPlayer().GetInventory();
    }

    public void Interact()
    {
        if (playerInventory.TryAddItem(itemSO))
        {
            //Destroy(gameObject);
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
