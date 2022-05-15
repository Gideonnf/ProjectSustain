using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI
{
    public GameObject ai;
    public string aiName;
    public int aiID;
    public bool availability;

    public AI(GameObject Ai)
    {
        ai = Ai;
        aiName = Ai.GetComponent<Transform>().name;
        aiID = Ai.GetComponent<Transform>().GetSiblingIndex();
        availability = true;
    }
}

public class AIManager : SingletonBase<AIManager>
{
    public AICustomer aICustomer;
    public List<AI> ListOfAgents = new List<AI>();
    public Queue<Transform> queueAgents = new Queue<Transform>();
    public Transform waitingPoint;
    public Transform exitPoint;
    public Vector3 newPosition;
    public Vector3 waitingPosition;
    public Vector3 exitPosition;

    public override void Awake()
    {
        base.Awake();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            CheckIfDone();
        }
    }

    public void CheckIfDone()
    {
       foreach(AI ai in ListOfAgents)
        {
            if (ai.ai.GetComponent<AICustomer>().isFinished == false)
            {
                // go to exitpoint
            }
        }
    }

    public void LoadAgents()
    {
        GameObject AiManager = GameObject.FindGameObjectWithTag("NPC");
        for (int i = 0; i < AiManager.transform.childCount; i++)
        {
            ListOfAgents.Add(new AI(AiManager.transform.GetChild(i).gameObject));
        }

        GetAvailAgent();
    }

    public void GetAvailAgent()
    {
        for (int i = 0; i < TableManager.Instance.ListOfTables.Count; i++)
        {
            //Debug.Log(TableManager.Instance.ListOfTables.Count);
            if (TableManager.Instance.ListOfTables[i].availability)
            {
                for (int j = 0; j < ListOfAgents.Count; j++)
                {
                    //Debug.Log(ListOfAgents.Count);
                    if (ListOfAgents[j].availability)
                    {
                        TableManager.Instance.ListOfTables[i].availability = false;
                        ListOfAgents[j].availability = false;
                        //Debug.Log(ListOfAgents[j].aiName + " seats at " + TableManager.Instance.ListOfTables[i].tableName);
                        ListOfAgents[j].ai.GetComponent<AICustomer>().MoveAgent(TableManager.Instance.ListOfTables[i].tablePosition.position);
                        break;
                    }
                }
            }
        }

        // Theres more agents then there are tables
        if (TableManager.Instance.ListOfTables.Count < ListOfAgents.Count)
        {
            for (int i = TableManager.Instance.ListOfTables.Count; i < ListOfAgents.Count; i++)
            {
                queueAgents.Enqueue(ListOfAgents[i].ai.transform);
                //Debug.Log(j);
                //Debug.Log(i);
                if (i == TableManager.Instance.ListOfTables.Count)
                {
                    //Debug.Log(i);
                    ListOfAgents[i].ai.GetComponent<AICustomer>().MoveAgent(waitingPoint.position);
                    //waitingPosition = ListOfAgents[i].ai.GetComponent<AICustomer>().agent.destination;
                    //Debug.Log(ListOfAgents[i].ai.GetComponent<AICustomer>().agent.destination);
                }
                else
                {
                    //newPosition = waitingPoint.position - (new Vector3(2f, 0f, 0f) * (i - 2));
                    newPosition = ListOfAgents[i - 1].ai.GetComponent<AICustomer>().agent.destination - new Vector3(4f, 0f, 0f); 
                    ListOfAgents[i].ai.GetComponent<AICustomer>().MoveAgent(newPosition);
                    //Debug.Log(newPosition);
                    //Debug.Log(ListOfAgents[i].aiName);
                    //Debug.Log(ListOfAgents[i].ai.GetComponent<AICustomer>().agent.destination);
                    //Debug.Log(newPosition);
                }
            }

            //foreach (Transform i in queueAgents)
            //{
            //    Debug.Log(i.name);
            //    Debug.Log(i.position.x);
            //}
        }
    }

    
}

