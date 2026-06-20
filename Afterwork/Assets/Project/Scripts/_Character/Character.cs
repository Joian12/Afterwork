using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Character : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _navMeshAgent;
    [SerializeField] private Transform _target;
    
    private void Awake()
    {
        StartCoroutine(GotoTarget());
        
    }

    private IEnumerator GotoTarget()
    {
        yield return new WaitForSeconds(2f);
        
        this._navMeshAgent.destination = this._target.position;
        this._navMeshAgent.speed = 2.5f;
    }
}
