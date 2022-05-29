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
    public List<AI> ListOfAgentsWaiting = new List<AI>();
    public Queue<Transform> queueAgents = new Queue<Transform>();
    public Transform waitingPoint;
    public Transform exitPoint;
    public Vector3 newPosition;

    public float waitingTime = 0.0f;
    public bool roundStart = false;
    public int customerInQueue = 0;
    float waitingTimer = 0.0f;

    public override void Awake()
    {
        base.Awake();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            //CheckIfDone();
            TestAIFinish(1);
        }

        if (Input.GetKeyDown(KeyCode.Backslash))
        {
            TestAIFinish(0);
        }

        if (PlayerManager.Instance.InMenu)
            return;

        if (roundStart == false)
        {
            waitingTimer += Time.deltaTime;
            if (waitingTimer >= waitingTime)
            {
                roundStart = true;
                waitingTimer = 0.0f;
                GetAvailAgent();
            }
        }
    }

    public void TestAIFinish(int id)
    {
        ListOfAgents[id].ai.GetComponent<AICustomer>().SimulateFinish();
        //for (int i = 0; i < ListOfAgents.Count; ++i)
        //{
        //    if (ListOfAgents[i].ai.GetComponent<AICustomer>().isOrdering)
        //    {
        //        ListOfAgents[i].ai.GetComponent<AICustomer>().SimulateFinish();
        //        break;
        //    }
        //}
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
            AiManager.transform.GetChild(i).gameObject.GetComponent<AICustomer>().aiID = i;
        }

        //GetAvailAgent();
    }

    public void GetAvailAgent()
    {
        for (int i = 0; i < TableManager.Instance.ListOfTables.Count; i++)
        {
            if (TableManager.Instance.ListOfTables[i].availability)
            {
                // Check current queue first 
                if (ListOfAgentsWaiting.Count > 0)
                {
                    int id;
                    for (id = 0; id < ListOfAgentsWaiting.Count; id++)
                    {
                        if (ListOfAgentsWaiting[id].availability)
                        {
                            TableManager.Instance.ListOfTables[i].availability = false;
                            ListOfAgentsWaiting[id].availability = false;
                            //Debug.Log(ListOfAgents[j].aiName + " seats at " + TableManager.Instance.ListOfTables[i].tableName);
                            ListOfAgentsWaiting[id].ai.GetComponent<AICustomer>().MoveAgent(TableManager.Instance.ListOfTables[i].tablePosition.position);
                            break;
                        }
                    }

                    // Remove from the list
                    ListOfAgentsWaiting.RemoveAt(id);
                }
                // else if no one in waitingList
                // then check all agents
                else
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
        }

        // NOTE
        // this will probably cause an issue
        // cause i == listoftables.count will never trigger anymore
        

        // Theres more agents then there are tables
        if (TableManager.Instance.ListOfTables.Count < ListOfAgents.Count)
        {
            bool newWaitingAgent = false;
            for (int i = 0; i < ListOfAgents.Count; i++)
            {
                // If they are still available and not at a table
                if (ListOfAgents[i].availability)
                {
                    if (!ListOfAgentsWaiting.Contains(ListOfAgents[i]))
                    {
                        ListOfAgentsWaiting.Add(ListOfAgents[i]);
                        newWaitingAgent = true;
                    }
                }
            }

            if (newWaitingAgent)
            {
                if (ListOfAgentsWaiting.Count > 0)
                {
                    for (int i = 0; i < ListOfAgentsWaiting.Count; i++)
                    {
                        // Move them to the queue
                        float zCord = waitingPoint.position.z;
                        zCord -= (4 * i);
                        Vector3 targetPosition = waitingPoint.position;
                        targetPosition.z = zCord;
                        ListOfAgentsWaiting[i].ai.GetComponent<AICustomer>().MoveAgent(targetPosition);
                    }
                }
            }
            //for (int i = TableManager.Instance.ListOfTables.Count; i < ListOfAgents.Count; i++)
            //{
            //    if (ListOfAgents[i].availability)
            //    {
            //        //queueAgents.Enqueue(ListOfAgents[i].ai.transform);
            //        if (i == TableManager.Instance.ListOfTables.Count)
            //        {
            //            ListOfAgents[i].ai.GetComponent<AICustomer>().MoveAgent(waitingPoint.position);
            //        }
            //        else
            //        {
            //            newPosition = ListOfAgents[i - 1].ai.GetComponent<AICustomer>().agent.destination - new Vector3(4f, 0f, 0f);
            //            ListOfAgents[i].ai.GetComponent<AICustomer>().MoveAgent(newPosition);
            //        }
            //    }
            //}
            //foreach (Transform i in queueAgents)
            //{
            //    Debug.Log(i.name);
            //    Debug.Log(i.position.x);
            //}
        }
    }

    public void RequeueAI(int aiID)
    {
        ListOfAgents[aiID].availability = true;

        GetAvailAgent();
    }

    public void ResetAI()
    {
        for (int i = 0; i < ListOfAgents.Count; ++i)
        {
            //ListOfAgents[i].availability = false;
            ListOfAgents[i].ai.GetComponent<AICustomer>().MoveAgent(exitPoint.position);
        }

        TableManager.Instance.ResetTables();
    }

}

