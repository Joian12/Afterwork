using UnityEngine;
using UnityEngine.AI;

public class ActorMovement : IActorMovement
{
    private readonly NavMeshAgent _navMeshAgent;
    
    public ActorMovement(NavMeshAgent navMeshAgent)
    {
        this._navMeshAgent = navMeshAgent;
    }
    
    public void MoveToTarget(Vector3 targetPosition)
    {
        this._navMeshAgent.SetDestination(targetPosition);
    }
}