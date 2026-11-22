using System;
using UnityEngine;
using System.Collections.Generic;

public class Generator : MonoBehaviour, IInteractable
{
    private event EventHandler OnGeneratorInteracted;
    private string objectName = "Generator";
    public Player player;

    private bool isBroken;

    [SerializeField] private SphereCollider sphereCollider;
    [SerializeField] private InteractionCanvasScript interactionCanvasScript;


    private void Awake()
    {
        sphereCollider = GetComponent<SphereCollider>();

        OnGeneratorInteracted += Generator_OnGeneratorInteracted;

        isBroken = false;
        OnGeneratorInteracted?.Invoke(this, EventArgs.Empty);
    }

    private void Generator_OnGeneratorInteracted(object sender, EventArgs e)
    {
        isBroken = !isBroken;

        if (isBroken)
        {
            sphereCollider.enabled = true;
        }
        else
        {
            sphereCollider.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if (other.TryGetComponent(out IInteractor interactor))
        {
            Debug.Log(interactor.InteractorType);
            if (interactor.InteractorType == InteractorType.Player)
            {
                var player = interactor as Player;
                player.GetComponentInChildren<UI_NeededItemDisplayer>().ShowNeededItems(neededItemsList);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out IInteractor interactor))
        {
            if (interactor.InteractorType == InteractorType.Player)
            {
                var player = interactor as Player;
                player.GetComponentInChildren<UI_NeededItemDisplayer>().HideNeededItems();
            }
        }
    }

    public void Interact(GameObject interactor)
    {
        if (interactor.TryGetComponent(out IInteractor iInteractor))
        {
            if (iInteractor.InteractorType == InteractorType.Player)
            {
                var player = iInteractor as Player;
                if (player.HandleFixingObject(neededItemsList))
                {
                    OnGeneratorInteracted?.Invoke(this, EventArgs.Empty);
                }
            }
            else if (iInteractor.InteractorType == InteractorType.Enemy)
            {
                Debug.Log("Enemy interacted with the generator.");
            }
        }
    }

    [SerializeField] private List<NeededItem> neededItemsList = new();

    public Vector3 InteractionPoint => transform.position;

    public string InteractPrompt => objectName;
}


[Serializable]
public class NeededItem
{
    public ItemDefinitionsSO itemDefinition;
    public int requiredAmount;
    public NeededItem(ItemDefinitionsSO itemDefinition, int requiredAmount)
    {
        this.itemDefinition = itemDefinition;
        this.requiredAmount = requiredAmount;
    }
}
