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

    public override void Awake()
    {
        base.Awake();
    }

    public void Start()
    {
        //LoadAgents();
        //Debug.Log("2");
    }

    public void LoadAgents()
    {
        GameObject AiManager = GameObject.FindGameObjectWithTag("NPC");
        for (int i = 0; i < AiManager.transform.childCount; i++)
        {
            ListOfAgents.Add(new AI(AiManager.transform.GetChild(i).gameObject));
            //Debug.Log(AiManager.transform.GetChild(i).gameObject);
        }

        GetAvailAgent(AiManager);
    }

    public void GetAvailAgent(GameObject targetAgent)
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
                        Debug.Log(ListOfAgents[j].aiName + " seats at " + TableManager.Instance.ListOfTables[i].tableName);
                        //AICustomer.MoveAgent();
                        break;
                    }
                    else if (ListOfAgents[j].availability == false && ListOfAgents.Count != j)
                    {
                        Debug.Log(ListOfAgents[j].aiName + " is seated");
                    }
                    else if (ListOfAgents[j].availability == false && ListOfAgents.Count == j)
                    {
                        Debug.Log("All agent is seated");
                    }
                }
            }
            else
            {
                Debug.Log("All table is taken");
            }
        }
    }
}

