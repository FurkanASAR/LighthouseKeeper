using TMPro;
using UnityEngine;

public class PlayerInteract_UI : MonoBehaviour
{
    [SerializeField] private GameObject interactionBubble;
    [SerializeField] private PlayerInteract playerInteract;
    [SerializeField] private TextMeshProUGUI interactObjectPrompt;

    private void Awake()
    {
        if(playerInteract == null)
        {
            playerInteract = GetComponentInParent<PlayerInteract>();
        }
    }
    private void Update()
    {
        if (playerInteract.InteractableTarget == null)
        {
            interactObjectPrompt.text = null;
            Hide();
        }
        else
        {
            interactObjectPrompt.text = playerInteract.InteractableTarget.InteractPrompt;
            Show();
        }

    }

    private void Hide()
    {
        interactionBubble.SetActive(false);
    }

    private void Show()
    {
        interactionBubble.SetActive(true);
    }
}
