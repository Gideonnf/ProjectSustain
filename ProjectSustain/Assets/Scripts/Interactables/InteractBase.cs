using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractBase : MonoBehaviour
{
    [Header("Interactable Configuration")]
    [Tooltip("Time to interact")]
    public float interactTime = 2.0f;

    public bool interacting = false;
    public bool interactDone = false;

    public float interactTimer = 0.0f;
    GameObject playerReference;

    // Start is called before the first frame update
    public virtual void Start()
    {
        
    }

    // Update is called once per frame
   public virtual void Update()
    {
        if (interacting)
        {
            interactTimer += Time.deltaTime;
            if (interactTimer > interactTime)
            {
                interactDone = true;
                InteractEnd();
            }
        }
    }

    public virtual void Interact(GameObject playerObject)
    {
        interacting = true;

        playerReference = playerObject;
       // return;
    }

    public virtual void InteractEnd()
    {
        if (interactDone)
        {
            // Completed
            // Can call player object after done interacting
            // Example, if cutting vegetables, can pass in any values to tell the player that it has the cut vegetables
        }

        interacting = false;
        //return;
    }
}
