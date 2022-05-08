using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FryingPan
{
    public GameObject fryingPan;
    public bool isFree = true;
    [System.NonSerialized] public float cookTimer = 0.0f;
    public GameObject targetIngredient;

    public FryingPan(GameObject target)
    {
        fryingPan = target;
        isFree = true;
        cookTimer = 0.0f;
        targetIngredient = null;
    }
}

public class CookingStove : InteractBase
{
   
    //public struct FryingPan
    //{
    //    public GameObject fryingPan;
    //    public bool isFree;
    //    public float cookTimer;
    //    public GameObject targetIngredient;
    //}

    public List<FryingPan> fryingPans = new List<FryingPan>();

    //GameObject targetIngredient;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        //base.Update();
        for (int i = 0; i < fryingPans.Count; ++i)
        {
            // existing ingredient on it
            if (fryingPans[i].targetIngredient != null)
            {
                fryingPans[i].cookTimer += Time.deltaTime;
                // if the cook time is reached
                if (fryingPans[i].cookTimer >= fryingPans[i].targetIngredient.GetComponent<IngredientObject>().cookTime)
                {
                    if (fryingPans[i].targetIngredient.GetComponent<IngredientObject>().isCooked == false)
                    {
                        fryingPans[i].targetIngredient.GetComponent<IngredientObject>().isCooked = true;
                        fryingPans[i].targetIngredient.GetComponent<SpriteRenderer>().sprite = fryingPans[i].targetIngredient.GetComponent<IngredientObject>().cookedSprite;
                    }
                }
            }
        }
    }

    public override void Interact(GameObject playerObject)
    {
        if (playerReference == null)
        {
            playerReference = playerObject;
        }

        //base.Interact(playerObject);
        for (int i = 0; i < fryingPans.Count; ++i)
        {
            // If that pan is free
            if (fryingPans[i].isFree)
            {
                if (fryingPans[i].targetIngredient == null)
                {
                    GameObject tempIngredient = playerObject.GetComponent<PlayerController>().ingredientObject;
                    // Check if it is cooked
                    if (tempIngredient.GetComponent<IngredientObject>().isCooked == false && tempIngredient.GetComponent<IngredientObject>().isPrepared == true)
                    {
                        // fryingPans[i].targetIngredient = Instantiate(playerObject.GetComponent<PlayerController>().ingredientObject, fryingPans[i].fryingPan.transform);
                        fryingPans[i].targetIngredient = playerObject.GetComponent<PlayerController>().ingredientObject;
                        fryingPans[i].targetIngredient.transform.SetParent(fryingPans[i].fryingPan.transform);

                        fryingPans[i].targetIngredient.transform.localPosition = new Vector3(0, 0.05f, 0);
                        fryingPans[i].targetIngredient.transform.localRotation = Quaternion.Euler(new Vector3(90.0f, 0.0f, 0.0f));
                        fryingPans[i].targetIngredient.transform.localScale = fryingPans[i].targetIngredient.GetComponent<IngredientObject>().cookSpriteScale;
                        // tempIngredient.transform.parent = null;
                        playerObject.GetComponent<PlayerController>().ingredientObject = null;
                        //Destroy(tempIngredient);

                        fryingPans[i].isFree = false;
                        break;
                    }
                }
            }
            // Check for already cooking frying pans
            else if (fryingPans[i].isFree == false)
            {
                if (playerReference.GetComponent<PlayerController>().ingredientObject == null)
                {
                    // Cehck if they are done
                    if (fryingPans[i].targetIngredient.GetComponent<IngredientObject>().isCooked == true)
                    {
                    
                        // attach back to player's hand
                        fryingPans[i].targetIngredient.transform.SetParent(playerReference.GetComponent<PlayerController>().playerHand.transform);
                        fryingPans[i].targetIngredient.transform.localPosition = new Vector3(0, 0, 0);
                        fryingPans[i].targetIngredient.transform.localRotation = Quaternion.Euler(new Vector3(90.0f, 0.0f, 0.0f));
                        playerReference.GetComponent<PlayerController>().ingredientObject = fryingPans[i].targetIngredient;

                        fryingPans[i].cookTimer = 0.0f;
                        fryingPans[i].isFree = true;
                        fryingPans[i].targetIngredient = null;
                    }
                  
                    break;
                }
            }
        }

    }

    public override void InteractEnd()
    {
       // base.InteractEnd();
    }
}
