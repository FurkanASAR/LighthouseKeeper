using UnityEngine;

public interface IInteractable
{
    public Vector3 InteractionPoint { get; }
    public string InteractPrompt { get; }


    public void Interact(GameObject interactor);
}
