using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AICustomer : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform tables;
    public List<Transform> points;
    public Rigidbody rigidbody;

    int count;
    int availableSeats;

    private void Start()
    {
        availableSeats = tables.childCount;
        rigidbody = GetComponent<Rigidbody>();
        count = 0;
    }

    void Update()
    {
        if (availableSeats > 0)
        {
            for (int i = 0; i < 2; i++)
            {
                agent.SetDestination(tables.GetChild(i).position);
                if (agent.velocity == Vector3.zero)
                {
                    count++;
                    if (count > 3)
                        agent.isStopped = true;
                }
                else
                {
                    agent.isStopped = false;
                    count = 0;
                } 
            }
        }
    }
}
