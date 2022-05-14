using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class IngredientItem
{
    public string name;
    public Sprite finishedSprite;

    public IngredientItem(string name, Sprite Sprite)
    {
        this.name = name;
        finishedSprite = Sprite;
    }
}

[System.Serializable]
public class FoodItem
{
    public string name;
    public GameObject foodPrefab;
    public Sprite foodSprite;
    // Different food will have different waiting time based on complexity
    public float waitingTime = 0.0f;
    public List<IngredientItem> ingredientList = new List<IngredientItem>();

    public FoodItem(GameObject foodPrefab, List<IngredientItem> ingredientList)
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
        //FoodItem new = Instantiate(ListOfFood[0]);
        return ListOfFood[0];
    }

    public FoodItem CheckCompleteDish(List<IngredientItem> ingredientList)
    {
        // Loop through the food
        foreach (FoodItem food in ListOfFood)
        {
            // First check the count of ingredients
            // So that we dont make unneccesary loops

            // If the ingredient list for that food item is equal to the ingredient list
            if (food.ingredientList.Count == ingredientList.Count)
            {
                //Loop through the ingredient list given 
                foreach (IngredientItem ingredient in ingredientList)
                {
                    // if the ingredient in the list doesnt exist in the food's ingredients
                    // then it isnt that dish
                    if (!food.ingredientList.Contains(ingredient))
                    {
                        // break out
                        break;
                    }
                }

                // If it made it here, means that the dish ingredients are the same
                return food;
            }
 
        }

        return null;
    }
}
