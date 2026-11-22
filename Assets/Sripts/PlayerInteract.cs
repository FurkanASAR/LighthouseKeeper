using UnityEngine;

public class PlayerInteract : MonoBehaviour
{


    [SerializeField] private LayerMask interactableLayerMask;   
    private float sphereRadius = 3f;
    private Collider[] detectedColliders = new Collider[10];

    public IInteractable InteractableTarget { get; private set; }
   
    private void Update()
    {
        FindBestInteractableTarget();

        if (Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
    }

    private void Interact()
    {
        if(InteractableTarget != null)
        {
            InteractableTarget.Interact(transform.parent.gameObject);
            
        }
        else
        {
            Debug.Log("No interactable target found.");
        }
    }


    private void FindBestInteractableTarget()
    {        
        int detectedCount = Physics.OverlapSphereNonAlloc(transform.position, sphereRadius, detectedColliders, interactableLayerMask, QueryTriggerInteraction.Collide);

        IInteractable bestTarget = null;
        float bestDistance = float.MaxValue;        

        for (int i = 0; i < detectedCount; i++)
        {            
            Collider currentCollider = detectedColliders[i];
            if (currentCollider == null)
            {
                continue;
            }

            IInteractable currentInteractable = currentCollider.GetComponent<IInteractable>();

            if (currentInteractable == null)
            {           
                continue;
            }

            float distanceSqr = (currentInteractable.InteractionPoint - transform.position).sqrMagnitude;

            if(distanceSqr < bestDistance)
            {
                bestDistance = distanceSqr;
                bestTarget = currentInteractable;
            }
        }

        SetInteractableTarget(bestTarget);

    }



    private void SetInteractableTarget(IInteractable target)
    {        
        InteractableTarget = target;
    }



    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, sphereRadius);
    }






}
