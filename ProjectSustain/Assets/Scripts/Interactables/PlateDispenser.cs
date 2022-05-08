using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateDispenser : InteractBase
{
    public GameObject plateObject;

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
        GameObject newPlate;
        playerReference = playerObject;
        if (playerReference != null)
        {
            GameObject playerHands = playerReference.GetComponent<PlayerController>().playerHand;
            newPlate = Instantiate(plateObject, playerHands.transform);
            newPlate.transform.localPosition = new Vector3(0, 0, 0);
            //newIngredient.transform.localRotation = Quaternion.Euler(new Vector3(90.0f, 0.0f, 0.0f));
            playerReference.GetComponent<PlayerController>().plateObject = newPlate;

            // if player is holding onto food already
            if (playerReference.GetComponent<PlayerController>().ingredientObject != null)
            {
                //playerReference.GetComponent<PlayerController>().AddToPlate();
                //GameObject currentIngredient = playerReference.GetComponent<PlayerController>().ingredientObject;

                //if (currentIngredient.GetComponent<IngredientObject>().isCooked && currentIngredient.GetComponent<IngredientObject>().isPrepared)
                //{
                //    currentIngredient.transform.SetParent(newPlate.transform);
                //    currentIngredient.transform.localPosition = new Vector3(0, 0.1f, 0);
                //    currentIngredient.transform.localRotation = Quaternion.Euler(new Vector3(90.0f, 0.0f, 0.0f));
                //}
            }
        }
        Debug.Log("test");
    }

    /// <summary>
    /// Override the base cause it'll have a unique end to interaction
    /// compared to other interactables
    /// </summary>
    public override void InteractEnd()
    {
        base.InteractEnd();
    }
}
