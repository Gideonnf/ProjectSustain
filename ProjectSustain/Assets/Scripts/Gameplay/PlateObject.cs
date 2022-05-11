using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateObject : MonoBehaviour
{
    public List<GameObject> ListOfIngredients = new List<GameObject>();
    public GameObject burgerPrefab;
    bool complete = false;

    float timer;
    // Start is called before the first frame update
    void Start()
    {

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

        //if (complete == true)
        //{
        //    timer += Time.deltaTime;
        //    if (timer > 1.0f)
        //    {
        //        GetComponentInParent<ServingStation>().textObject.SetActive(false);

        //    }
        //}
    }

    public void AddToPlate(GameObject newIngredient)
    {
        if (newIngredient != null)
        {
            // Make sure its prepared and cooked
            if (newIngredient.GetComponent<IngredientObject>().isCooked && newIngredient.GetComponent<IngredientObject>().isPrepared)
            {
                newIngredient.transform.SetParent(transform);
                newIngredient.transform.localPosition = new Vector3(0, 0.1f, 0);
                newIngredient.transform.localRotation = Quaternion.Euler(new Vector3(90.0f, 0.0f, 0.0f));
                ListOfIngredients.Add(newIngredient);

            }
        }
    }

    void DeleteList()
    {
        
    }
}
