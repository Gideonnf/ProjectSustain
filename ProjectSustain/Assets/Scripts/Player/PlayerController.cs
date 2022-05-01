using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRigidbody;
    public PlayerInput playerInput;
    private InputAction movement;

    [Tooltip("Speed of the player")]
    public float playerSpeed = 5.0f;

    GameObject interactableObject = null;
    
    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        playerRigidbody = GetComponent<Rigidbody>();
        
        movement = playerInput.actions["Movement"];
    }
    // Start is called before the first frame update
    void Start()
    {
       // playerInput.playerIndex
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 movementVector = movement.ReadValue<Vector2>();
        //Debug.Log(movementVector);
        // transform.position += new Vector3(movementVector.x, 0, movementVector.y) * playerSpeed;
        //Debug.Log(new Vector3(movementVector.x, 0, movementVector.y) * playerSpeed);
        playerRigidbody.AddForce(new Vector3(movementVector.x, 0, movementVector.y) * playerSpeed, ForceMode.Force);

        // Checking for interactable objects infront of the player
        Vector3 origin = transform.position - new Vector3(0, 0.2f, 0);
        Debug.DrawRay(origin, Vector3.forward * 2.0f, Color.green);
        RaycastHit[] hit = Physics.RaycastAll(origin, transform.forward, 2.0f);
        // If theres objects it hit
        if (hit.Length > 0)
        {
            foreach (RaycastHit i in hit)
            {
                // Interactable hit
                if (i.collider.tag == "Interactable")
                {
                    Debug.Log("INTERACTABLE HIT");
                   // i.collider.gameObject.GetComponent<InteractBase>();
                    interactableObject = i.collider.gameObject;
                }
            }
        }
        else
        {
            // It had a collided object but the player ran away
            // If they had interating on, turn it off
            if (interactableObject != null)
            {
                if (interactableObject.GetComponent<InteractBase>().interacting)
                    interactableObject.GetComponent<InteractBase>().interacting = false;
            }
            // Theres no object its colliding with
            interactableObject = null;
        }
    }

    public void Movement(InputAction.CallbackContext context)
    {
        //Debug.Log(context);
        Vector2 movementVector = context.ReadValue<Vector2>();
       // Debug.Log(movementVector);
        playerRigidbody.AddForce(new Vector3(movementVector.x, 0, movementVector.y) * playerSpeed, ForceMode.Force);

    }

    public void Interact(InputAction.CallbackContext context)
    {
        //Debug.Log(context);
        if (context.performed)
        {
            Debug.Log("Player Interacting");
            if (interactableObject != null)
            {
                interactableObject.GetComponent<InteractBase>().interacting = true;
            }
        }
        else if (context.canceled)
        {
            if (interactableObject != null)
            {
                interactableObject.GetComponent<InteractBase>().interacting = false;
            }

        }
    }
}
