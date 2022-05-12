using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FoodItem
{
    public GameObject foodPrefab;
    // Different food will have different waiting time based on complexity
    public float waitingTime = 0.0f;
    public List<GameObject> ingredientList = new List<GameObject>();

    public FoodItem(GameObject foodPrefab, List<GameObject> ingredientList)
    {
        this.foodPrefab = foodPrefab;
        this.ingredientList = ingredientList;
        waitingTime = 0.0f;
    }
}

public class FoodManager : SingletonBase<FoodManager>
{
    // could be combined into AI manager later on

    public List<FoodItem> ListOfFood = new List<FoodItem>();

    public override void Awake()
    {
        base.Awake();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public FoodItem GetFoodOrder()
    {
        
        // For testing purposes
        // there is only 1 dish that can be returned
        return ListOfFood[0];
    }
}
