using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashInteractable : InteractBase
{
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
        playerReference = playerObject;
        if (playerReference != null)
        {
            GameObject playerHands = playerReference.GetComponent<PlayerController>().playerHand;
            // Has an ingredient in hand
            if (playerReference.GetComponent<PlayerController>().ingredientObject)
            {
                GameObject ingredient = playerReference.GetComponent<PlayerController>().ingredientObject;
                playerReference.GetComponent<PlayerController>().ingredientObject = null;

                PlayerManager.Instance.RemoveScore(ingredient.GetComponent<IngredientObject>().ingredientItem.wastageValue);
                Destroy(ingredient);
            }
        }
    }

    public override void InteractEnd()
    {
        base.InteractEnd();
    }
}
