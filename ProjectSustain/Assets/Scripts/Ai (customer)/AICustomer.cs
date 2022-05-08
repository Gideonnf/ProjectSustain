using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AICustomer : MonoBehaviour
{
    public GameObject tableTransform;
    public NavMeshAgent agent;
    public Rigidbody rbNPC;

    int count;
    int availableSeats;

    private void Start()
    {
        TableManager.Instance.LoadTables();
        
        //availableSeats = TestManager.Instance.ListOfTables.Count;
        availableSeats = tableTransform.GetComponent<Transform>().childCount;
        rbNPC = GetComponent<Rigidbody>();
        //Debug.Log(availableSeats);
        count = 1;
    }

    void Update()
    {
        //MoveAgent();
    }

    void MoveAgent()
    {
        //Debug.Log("Test");
        
        if (availableSeats > 0)
        {
            if (count == 1)
            {
                count--;
                TableManager.Instance.GetAvailPosition(tableTransform);

                for (int i = 0; i < availableSeats; i++)
                {
                    //Debug.Log(TestManager.Instance.ListOfTables[i].availability);
                    if (TableManager.Instance.ListOfTables[i].availability)
                    {
                        TableManager.Instance.ListOfTables[i].availability = false;

                        Debug.Log("Agent " + i + 1 + " is going table " + TableManager.Instance.ListOfTables[i].tableName);
                        Debug.Log(TableManager.Instance.ListOfTables[i].tableName + " is set to " + TableManager.Instance.ListOfTables[i].availability);
                        //agent.SetDestination(tableTransform.transform.GetChild(i).position);
                        count = 1;

                        return;
                        //agent.SetDestination(gameObject.transform.GetChild(i).position);
                        //Debug.Log("Table is taken: " + TestManager.Instance.ListOfTables[i].tableName);
                        //Debug.Log(i);
                        //Debug.Log(TestManager.Instance.ListOfTables[i].availability);
                        //Debug.Log(gameObject.transform.GetChild(i).name);
                    }
                    else 
                    {
                        Debug.Log("Full house");
                        // Wait outside restaurant
                    }
                }
            }
        }
    }
}
