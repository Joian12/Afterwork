using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Character : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _navMeshAgent;
    [SerializeField] private Transform _target;
    
    private IActorMovement _actorMovement;
    
    private void Awake()
    {
        // should be in create
        this._actorMovement = new ActorMovement(this._navMeshAgent);
        
        StartCoroutine(GotoTarget());
    }

    private IEnumerator GotoTarget()
    {
        yield return new WaitForSeconds(1f);
        
        this._actorMovement.MoveToTarget(this._target.position);
    }
}