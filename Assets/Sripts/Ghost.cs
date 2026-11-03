using System.Net.Http.Headers;
using Unity.VisualScripting;
using UnityEditor.AdaptivePerformance.Editor;
using UnityEngine;
using UnityEngine.AI;

public class Ghost : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Vector3 destination = new();


    private Transform playerTransform;

    private enum State
    {
        Idle,
        Chasing,
        Patrolling
    }
    private State state;


    private void Awake()
    {
        if (agent == null)
        {
            agent = GetComponent<NavMeshAgent>();
        }

        state = State.Patrolling;
        destination = new Vector3(Random.Range(-10f, 10f), 0f, Random.Range(-10f, 10f));
        agent.SetDestination(destination);
    }

    private void Update()
    {
        ChangeState();
        HandleStates();
    }

    private void ChangeState()
    {
        if (playerTransform != null)
        {
            state = State.Chasing;
        }
        else
        {
            state = State.Patrolling;
        }
    }
    private void HandleStates()
    {
        switch (state)
        {
            case State.Idle: Debug.Log("Ghost State: Idle"); break;

            case State.Chasing: HandleChasing(); break;

            case State.Patrolling: HandlePatroling(); break;
        }
    }

    private void HandleChasing()
    {
        agent.SetDestination(playerTransform.position);
    }

    private void HandlePatroling()
    {
        if (!agent.hasPath)
        {
            SetNewDestination();
        }
        
        if (HasReachedDestination())
        {
            SetNewDestination();
        }
    }

    private void SetNewDestination()
    {
        destination = new Vector3(Random.Range(-10f, 10f), 0f, Random.Range(-10f, 10f));
        agent.SetDestination(destination);
    }
    private bool HasReachedDestination()
    {
        if (agent.pathPending)
        {
            return false;
        }

        if (agent.hasPath && agent.remainingDistance <= agent.stoppingDistance)
        {
            return true;
        }

        return false;
    }

    public void SetPlayerTransform(Transform playerTransform)
    {
        this.playerTransform = playerTransform;
    }

    public void ResetAgentPath()
    {
        agent.ResetPath();
    }

}
