using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemDisplayerScript : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI itemAmount;


    public void SetItemDisplay(Sprite itemSprite, int amount)
    {
        image.sprite = itemSprite;
        itemAmount.text = amount.ToString();
    }

}
