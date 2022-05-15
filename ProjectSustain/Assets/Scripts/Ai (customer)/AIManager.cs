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
    public List<AI> ListOfAgents = new List<AI>();
    public Queue<Transform> queueAgents = new Queue<Transform>();
    public Transform waitingPoint;
    public Transform exitPoint;
    public Vector3 newPosition;

    public override void Awake()
    {
        base.Awake();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            //CheckIfDone();
        }

        if (Input.GetKeyDown(KeyCode.Backslash))
        {
            TestAIFinish();
        }
    }

    public void TestAIFinish()
    {
        for (int i = 0; i < ListOfAgents.Count; ++i)
        {
            if (ListOfAgents[i].ai.GetComponent<AICustomer>().isOrdering)
            {
                ListOfAgents[i].ai.GetComponent<AICustomer>().SimulateFinish();
                break;
            }
        }
    }

    //public void CheckIfDone()
    //{
    //   foreach(AI ai in ListOfAgents)
    //    {
    //        if (ai.ai.GetComponent<AICustomer>().isFinished == false)
    //        {
    //            // go to exitpoint
    //            ai.ai.GetComponent<AICustomer>().MoveAgent(exitPoint.position);
                
    //            GetAvailAgent();
    //            queueAgents.Dequeue();
    //        }
    //    }
    //}


    
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
            if (TableManager.Instance.ListOfTables[i].availability)
            {
                for (int j = 0; j < ListOfAgents.Count; j++)
                {
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
                //queueAgents.Enqueue(ListOfAgents[i].ai.transform);
                if (i == TableManager.Instance.ListOfTables.Count)
                {
                    ListOfAgents[i].ai.GetComponent<AICustomer>().MoveAgent(waitingPoint.position);
                }
                else
                {
                    newPosition = ListOfAgents[i - 1].ai.GetComponent<AICustomer>().agent.destination - new Vector3(4f, 0f, 0f); 
                    ListOfAgents[i].ai.GetComponent<AICustomer>().MoveAgent(newPosition);
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

