using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateObject : MonoBehaviour
{
    public List<IngredientItem> ListOfIngredients = new List<IngredientItem>();
    public GameObject dishPrefab;
    public Sprite dishSprite;
    public FoodItem currentFood;
    public bool complete;
    int ingredientCounter;

    float timer;
    // Start is called before the first frame update
    void Start()
    {
        complete = false;
        currentFood = null;
        ingredientCounter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //// just for testing sake
        //if (ListOfIngredients.Count >= 5)
        //{
        //    // is done
        //    if (complete == false)
        //    {
        //        complete = true;
        //        GameObject newBurger = Instantiate(burgerPrefab, transform);
        //        newBurger.transform.localPosition = new Vector3(0, 0.15f, 0);
        //        newBurger.transform.localRotation = Quaternion.Euler(new Vector3(90.0f, 0.0f, 0.0f));
        //        for (int i = 0; i < ListOfIngredients.Count; ++i)
        //        {
        //            ListOfIngredients[i].SetActive(false);
        //        }

        //        GetComponentInParent<ServingStation>().textObject.SetActive(true);
        //    }
        //}

        if (complete == true)
        {
            timer += Time.deltaTime;
            if (timer > 1.0f)
            {
                GetComponentInParent<ServingStation>().textObject.SetActive(false);

            }
        }
    }

    public void AddToPlate(GameObject newIngredient)
    {
        if (newIngredient != null && complete == false)
        {
            // Make sure its prepared and cooked
            if (newIngredient.GetComponent<IngredientObject>().isCooked && newIngredient.GetComponent<IngredientObject>().isPrepared)
            {
                transform.GetChild(ListOfIngredients.Count).GetComponent<SpriteRenderer>().sprite = newIngredient.GetComponent<SpriteRenderer>().sprite;
                ingredientCounter++;
                // Destroy the ingredient
                //Destroy(newIngredient);
                IngredientItem ingredient = newIngredient.GetComponent<IngredientObject>().ingredientItem;
                //newIngredient.transform.SetParent(transform);
                //newIngredient.transform.localPosition = new Vector3(0, 0.1f, 0);
                //newIngredient.transform.localRotation = Quaternion.Euler(new Vector3(90.0f, 0.0f, 0.0f));
                ListOfIngredients.Add(ingredient);

                currentFood = FoodManager.Instance.CheckCompleteDish(ListOfIngredients);
                if (currentFood == null)
                {
                    // No dish was found
                    complete = false;
                }
                else
                {
                    Debug.Log("Number of Ingredients" + ingredientCounter);
                    for(int i = 0; i < ingredientCounter; ++i)
                    {
                        transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = null;
                    }


                    transform.GetChild(ingredientCounter).GetComponent<SpriteRenderer>().sprite = currentFood.foodSprite;
                    transform.GetChild(ingredientCounter).gameObject.SetActive(true);
                    GetComponentInParent<ServingStation>().textObject.SetActive(true);
                    complete = true;

                }
                Destroy(newIngredient);
            }
        }
    }

    void DeleteList()
    {
        
    }
}
