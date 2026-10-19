using UnityEngine;

public interface IInteractable
{
    public Vector3 InteractionPoint { get; }


    public void Interact();
}
