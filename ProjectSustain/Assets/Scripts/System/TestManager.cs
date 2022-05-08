using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tables
{
    GameObject table;
    bool availability;
    
    public Tables(GameObject Table)
    {
        table = Table;
        availability = true;
    }
}

public class TestManager : SingletonBase<TestManager>
{
    public List<Tables> ListOfTables = new List<Tables>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LoadTables()
    {
        GameObject TableManager = GameObject.FindGameObjectWithTag("TableManager");
        for (int i = 0; i < TableManager.transform.childCount; i++)
        {
            ListOfTables.Add(new Tables(TableManager.transform.GetChild(i).gameObject));
        }
    }

    public void GetAvailPosition(GameObject targetObject)
    {
        // Check through the list of tables
        // when you find one

        //targetObject.GetComponent<AIController>();


    }
}
