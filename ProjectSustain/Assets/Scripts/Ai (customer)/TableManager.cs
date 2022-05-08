using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tables
{
    public GameObject table;
    public string tableName;
    public int tableID;
    public bool availability;

    public Tables(GameObject Table)
    {
        table = Table;
        tableName = Table.GetComponent<Transform>().name;
        tableID = Table.GetComponent<Transform>().GetSiblingIndex();
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
        
        LoadTables();
        //Debug.Log("1");
    }

    public void Update()
    {
        
    }

    public void LoadTables()
    {
        GameObject TableManager = GameObject.FindGameObjectWithTag("TableManager");
        for (int i = 0; i < TableManager.transform.childCount; i++)
        {
            ListOfTables.Add(new Tables(TableManager.transform.GetChild(i).gameObject));
            //Debug.Log(TableManager.transform.GetChild(i).gameObject);
        }

        GetAvailPosition(TableManager);
        AIManager.Instance.LoadAgents();
    }

    public void GetAvailPosition(GameObject targetTable)
    {
        for (int i = 0; i < ListOfTables.Count; i++)
        {
            //Debug.Log(ListOfTables.Count);
            //Debug.Log(ListOfTables[i].tableID);
            //Debug.Log(targetTable.transform.GetChild(i).GetSiblingIndex());
            //if (targetTable.transform.GetChild(i).GetSiblingIndex() == ListOfTables[i].tableID)
            //{
                //Debug.Log(ListOfTables[i].tableName + " is " + ListOfTables[i].availability);
            if (ListOfTables[i].availability)
            {
                //Debug.Log(ListOfTables[i].availability);
                ListOfTables[i].availability = true;
                //Debug.Log(ListOfTables[i].tableName);
            }
            else if (ListOfTables[i].availability == false)
            {
                Debug.Log("Full");
            }
            //}
        }
    }
}