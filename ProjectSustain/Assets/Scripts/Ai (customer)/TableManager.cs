using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Tables
{
    public GameObject table;
    public Transform tablePosition;
    public string tableName;
    public int tableID;
    public bool availability;

    public Tables(GameObject Table)
    {
        table = Table;
        tableName = Table.GetComponent<Transform>().name;
        tableID = Table.GetComponent<Transform>().GetSiblingIndex();
        tablePosition = Table.transform;
        availability = true;
    }
}

public class TableManager : SingletonBase<TableManager>
{
    public List<Tables> ListOfTables = new List<Tables>();

    public override void Awake()
    {
        base.Awake();
    }

    public void Start()
    {
        GameObject TableManager = GameObject.FindGameObjectWithTag("TableManager");

        //for (int i = 0; i < TableManager.transform.childCount; i++)
        //{
        //    ListOfTables.Add(new Tables(TableManager.transform.GetChild(i).gameObject));
        //}

        GetAvailPosition(TableManager);
        LoadTables();
    }

    public void LoadTables()
    {
        AIManager.Instance.LoadAgents();
    }

    public void Update()
    {
        //if(Input.GetKeyDown(KeyCode.Space))
        //{
        //    LoadTables();
        //}
    }

    public void ResetTables()
    {
        for(int i = 0; i < ListOfTables.Count; i++)
        {
            ListOfTables[i].availability = true;
        }
    }

    public void GetAvailPosition(GameObject targetTable)
    {
        for (int i = 0; i < ListOfTables.Count; i++)
        {
            if (ListOfTables[i].availability)
            {
                ListOfTables[i].availability = true;
            }
            else if (ListOfTables[i].availability == false)
            {
                Debug.Log("Full");
            }
        }
    }

    public void UpdateTable(GameObject ai)
    {
        for(int i = 0; i < ListOfTables.Count; ++i)
        {
            // If the AI is leaving that table
            if (ListOfTables[i].table == ai)
            {
                ListOfTables[i].availability = true;
                break;
            }
        }

        AIManager.Instance.GetAvailAgent();
    }
}