using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ServingStation : InteractBase
{
    GameObject plateObject;
    public GameObject textObject;
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
        if (plateObject == null)
        {
            plateObject = playerObject.GetComponent<PlayerController>().plateObject;
            plateObject.transform.SetParent(transform);
            plateObject.transform.localPosition = new Vector3(0, 1.2f, 0);
            plateObject.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);

            playerObject.GetComponent<PlayerController>().plateObject = null;
        }
        else
        {
            if (playerObject.GetComponent<PlayerController>().ingredientObject != null)
            {
                plateObject.GetComponent<PlateObject>().AddToPlate(playerObject.GetComponent<PlayerController>().ingredientObject);
                playerObject.GetComponent<PlayerController>().ingredientObject = null;
            }
        }
    }
}
