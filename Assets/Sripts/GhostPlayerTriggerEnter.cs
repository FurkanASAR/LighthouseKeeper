using System.Collections;
using UnityEngine;



public class GhostPlayerTriggerEnter : MonoBehaviour
{
    [SerializeField] private Ghost ghost;

    private void OnTriggerEnter(Collider other)
    {        
        if (other.TryGetComponent<Player>(out Player player))
        {
            ghost.SetPlayerTransform(player.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Player>(out Player player))
        {
            ghost.SetPlayerTransform(null);
            ghost.ResetAgentPath();
        }
    }
}
