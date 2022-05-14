using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ServingStation : InteractBase
{
    GameObject plateObject;
    float timer;

    public GameObject textObject;
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();

        if (plateObject != null)
        {
            if (plateObject.GetComponent<PlateObject>().complete == true)
            {
                timer += Time.deltaTime;
                if (timer > 1.0f)
                {
                    textObject.SetActive(true);
                }
            }
        }
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
            if (plateObject.GetComponent<PlateObject>().complete)
            {
                //plateObject = playerObject.GetComponent<PlayerController>().plateObject;
                plateObject.transform.SetParent(playerObject.GetComponent<PlayerController>().playerHand.transform);
                plateObject.transform.localPosition = new Vector3(0, 0, 0);
                playerObject.GetComponent<PlayerController>().plateObject = plateObject;

                plateObject = null;

                //plateObject.transform.localRotation = Quaternion.Euler(new Vector3())
            }
            else if (playerObject.GetComponent<PlayerController>().ingredientObject != null)
            {
                plateObject.GetComponent<PlateObject>().AddToPlate(playerObject.GetComponent<PlayerController>().ingredientObject);
                playerObject.GetComponent<PlayerController>().ingredientObject = null;
            }
        }
    }
}
