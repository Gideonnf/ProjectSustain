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
        GameObject AIManager = GameObject.FindGameObjectWithTag("NPC");
        for (int i = 0; i < AIManager.transform.childCount; i++)
        {
            ListOfAgents.Add(new AI(AIManager.transform.GetChild(i).gameObject));
            //Debug.Log(AIManager.transform.GetChild(i).gameObject);
        }
        GetAvailAgent(AIManager);
    }

    public void GetAvailAgent(GameObject targetAgent)
    {
        for (int i = 0; i < TableManager.Instance.ListOfTables.Count; i++)
        {
            if (TableManager.Instance.ListOfTables[i].availability)
            {
                for (int j = 0; i < ListOfAgents.Count; j++)
                {
                    if (ListOfAgents[i].availability)
                    {
                        TableManager.Instance.ListOfTables[i].availability = false;
                        ListOfAgents[i].availability = false;
                        Debug.Log(ListOfAgents[j].aiName + " seats at " + TableManager.Instance.ListOfTables[i].tableName);
                        //AICustomer.MoveAgent();
                    }
                    else
                    {
                        Debug.Log("All agent is taken");
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

