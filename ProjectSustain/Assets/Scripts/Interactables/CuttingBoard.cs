using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CuttingBoard : InteractBase
{
   // public GameObject foodObject;
    public GameObject cuttingBoard;

    GameObject targetIngredient = null;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

    public override void Interact(GameObject playerObject)
    {
        // base.Interact(playerObject);
        //Debug.Log("test");
        if (targetIngredient == null)
        {
            GameObject tempIngredient = playerObject.GetComponent<PlayerController>().ingredientObject;
            if (tempIngredient.GetComponent<IngredientObject>().isPrepared == false)
            {
                //targetIngredient = Instantiate(playerObject.GetComponent<PlayerController>().ingredientObject, cuttingBoard.transform);
                targetIngredient = playerObject.GetComponent<PlayerController>().ingredientObject;
                targetIngredient.transform.SetParent(cuttingBoard.transform);
                targetIngredient.transform.localPosition = new Vector3(0, 0.05f, 0);
                targetIngredient.transform.localRotation = Quaternion.Euler(new Vector3(90.0f, 0.0f, 0.0f));
                // tempIngredient.transform.parent = null;
                playerObject.GetComponent<PlayerController>().ingredientObject = null;
                //Destroy(tempIngredient);
                //Destroy(playerObject.GetComponent<PlayerController>().ingredientObject);
                // targetIngredient.transform.parent = cuttingBoard.transform;
            }
        }
        else
        {
            if (targetIngredient.GetComponent<IngredientObject>().isPrepared)
            {
                if (playerReference.GetComponent<PlayerController>().ingredientObject == null)
                {
                    // Pick up the ingredient aft preparing it
                    //GameObject newIngredient = 
                    // Transfer ingredient back to player's hand
                    targetIngredient.transform.SetParent(playerReference.GetComponent<PlayerController>().playerHand.transform);
                    targetIngredient.transform.localPosition = new Vector3(0, 0, 0);
                    targetIngredient.transform.localRotation = Quaternion.Euler(new Vector3(90.0f, 0.0f, 0.0f));
                    playerReference.GetComponent<PlayerController>().ingredientObject = targetIngredient;

                    // Set back to null
                    playerReference = null;
                    targetIngredient = null;
                    interactDone = false;
                }         
            }
            else
            {
                Debug.Log("Interacting");
                interacting = true;
                playerReference = playerObject;
            }
        }
    }

    /// <summary>
    /// Override the base cause it'll have a unique end to interaction
    /// compared to other interactables
    /// </summary>
    public override void InteractEnd()
    {
        //base.InteractEnd();
        if (interactDone && targetIngredient != null)
        {
            //GameObject newIngredient;
            //if (playerReference != null)
            //{
            //    GameObject playerHands = playerReference.GetComponent<PlayerController>().playerHand;
            //    newIngredient = Instantiate(foodObject, playerHands.transform);
            //    foodObject.transform.position = new Vector3(0, 0, 0);
            //    foodObject.transform.Rotate(90.0f, 0.0f, 0.0f, Space.World);
            //}
            if (targetIngredient.GetComponent<IngredientObject>().isPrepared == false)
            {
                targetIngredient.GetComponent<SpriteRenderer>().sprite = targetIngredient.GetComponent<IngredientObject>().preparedSprite;
                targetIngredient.GetComponent<IngredientObject>().isPrepared = true;

                ResetRadial();
                interactTimer = 0.0f;
            }
        }

       // playerReference = null;
        interacting = false;
    }
}
