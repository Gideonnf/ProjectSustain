using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodDispenser : InteractBase
{
    public GameObject foodObject;

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
        //base.Interact(playerObject);
        //Debug.Log("test");
        GameObject newIngredient;
        playerReference = playerObject;
        if (playerReference != null)
        {
            GameObject playerHands = playerReference.GetComponent<PlayerController>().playerHand;
            newIngredient = Instantiate(foodObject, playerHands.transform);
            newIngredient.transform.localPosition = new Vector3(0, 0, 0);
            newIngredient.transform.localRotation = Quaternion.Euler(new Vector3(90.0f, 0.0f, 0.0f));

            playerReference.GetComponent<PlayerController>().ingredientObject = newIngredient;
        }
    }

    /// <summary>
    /// Override the base cause it'll have a unique end to interaction
    /// compared to other interactables
    /// </summary>
    public override void InteractEnd()
    {
        //base.InteractEnd();
        if (interactDone)
        {
            GameObject newIngredient;
            if (playerReference != null)
            {
                GameObject playerHands = playerReference.GetComponent<PlayerController>().playerHand;
                newIngredient = Instantiate(foodObject, playerHands.transform);
                newIngredient.transform.position = new Vector3(0, 0, 0);
                newIngredient.transform.localRotation = Quaternion.Euler(new Vector3(90.0f, 0.0f, 0.0f));

                playerReference.GetComponent<PlayerController>().ingredientObject = newIngredient;
            }

            ResetRadial();
            interactTimer = 0.0f;
        }

        playerReference = null;
        interacting = false;
    }
}
