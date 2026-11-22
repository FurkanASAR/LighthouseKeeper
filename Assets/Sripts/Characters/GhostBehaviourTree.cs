using UnityEngine;

public class GhostBehaviourTree : MonoBehaviour
{
    [SerializeField] private Ghost ghost;

    [SerializeField] private EnemyVision ghostVision;
    private DecisionTreeNode root;
    private ActionNode chaseActionNode;
    private ActionNode patrolActionNode;


    private void Awake()
    {
        chaseActionNode = new ActionNode(Ghost.State.Chasing);
        patrolActionNode = new ActionNode(Ghost.State.Patrolling);
        
    }

    public Ghost.State GetBehaviour()
    {
        root = new DecisionNode(() => CanSeePlayer(), chaseActionNode, patrolActionNode).MakeDecision();


        if (root is ActionNode actionNode)
        {
            return actionNode.ActionType;
        }
        else
        {
            Debug.LogError("Root node is not an ActionNode.");
            return Ghost.State.Idle; // Default state in case of error
        }
    }


    private bool CanSeePlayer()
    {
        return ghostVision.CanSeePlayer;
    }
}
