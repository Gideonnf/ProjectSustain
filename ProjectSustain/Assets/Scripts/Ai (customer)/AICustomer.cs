using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AICustomer : MonoBehaviour
{
    public NavMeshAgent agent;

    void Update()
    {
        //MoveAgent();
    }

    public void MoveAgent(Transform transform)
    {
        if (AIManager.Instance.moveAgent)
        {
            Debug.Log(transform.position);
            agent.SetDestination(transform.position);
        }
    }
}
