using UnityEngine;

[CreateAssetMenu]
public class ItemDefinitionsSO : ScriptableObject
{
    public enum ItemType
    {
        Wood,
        Stone,
        HealthPotion,
        Iron
    }

    public ItemType itemType;

    public string itemName;
    public int itemID;
    public bool isStackable;
    public int maxStackSize;
}
