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
        base.Interact(playerObject);
       //Debug.Log("test");
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
                foodObject.transform.position = new Vector3(0, 0, 0);
                foodObject.transform.Rotate(90.0f, 0.0f, 0.0f, Space.World);
            }
        }

        playerReference = null;
        interacting = false;
    }
}
