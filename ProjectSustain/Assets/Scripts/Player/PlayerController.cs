using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class PlayerController : MonoBehaviour
{
    public GameObject playerHand;
    public PlayerInput playerInput;

    private Rigidbody playerRigidbody;
    private InputAction movement;
    private Vector3 lookDirection;

    [Tooltip("Speed of the player")]
    public float playerSpeed = 5.0f;

    GameObject interactableObject = null;
    GameObject interactableCustomer = null;
    [System.NonSerialized] public GameObject ingredientObject = null;
    [System.NonSerialized] public GameObject plateObject = null;
    
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
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Debug.DrawRay(origin, forward * 2.0f, Color.green);
        RaycastHit[] hit = Physics.RaycastAll(origin, transform.forward, 2.0f);
        // If theres objects it hit
        if (hit.Length > 0)
        {
            foreach (RaycastHit i in hit)
            {
                // Interactable hit
                if (i.collider.tag == "Interactable")
                {
                   // Debug.Log("INTERACTABLE HIT");
                   // i.collider.gameObject.GetComponent<InteractBase>();
                    interactableObject = i.collider.gameObject;
                }
                
                // Check for NPC collidable 
                if (i.collider.tag == "NPC")
                {
                    interactableCustomer = i.collider.gameObject;
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
            interactableCustomer = null;
        }
    }

    public void Movement(InputAction.CallbackContext context)
    {
        //Debug.Log(context);
        Vector2 movementVector = context.ReadValue<Vector2>();
       // Debug.Log(movementVector);
        playerRigidbody.AddForce(new Vector3(movementVector.x, 0, movementVector.y) * playerSpeed, ForceMode.Force);

    }

    public void Rotation(InputAction.CallbackContext context)
    {
       // Debug.Log(context.phase);
        if (context.performed)
        {
            Vector2 rotationVector = context.ReadValue<Vector2>();
            //Debug.Log(rotationVector);
            if (rotationVector != Vector2.zero)
            {
                lookDirection = new Vector3(rotationVector.x, 0, rotationVector.y);
                Quaternion targetRotation = Quaternion.LookRotation(lookDirection, Vector3.up);

                // transform.rotation = targetRotation;
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 30 );
            }
        }
    }

    public void Interact(InputAction.CallbackContext context)
    {
        //Debug.Log(context);
        if (context.performed)
        {
            Debug.Log("Player Interacting");
            if (interactableCustomer != null)
            {
                interactableCustomer.GetComponent<AICustomer>().ServeCustomer(gameObject);
            }
            else if (interactableObject != null)
            {
                Debug.Log("Before" + ingredientObject);
                Debug.Log("Before" + plateObject);
                interactableObject.GetComponent<InteractBase>().Interact(gameObject);
                Debug.Log("After" + ingredientObject);
                Debug.Log("After" + plateObject);

            }
        }
        else if (context.canceled)
        {
            if (interactableObject != null)
            {
                interactableObject.GetComponent<InteractBase>().InteractEnd();
            }

        }
    }

    public void UIBack(InputAction.CallbackContext context)
    {
        if (context.performed)
        { 
            // if active
            if (PlayerManager.Instance.bookObject.activeSelf)
            {
                PlayerManager.Instance.bookObject.SetActive(false);
            }
        }
    }

}
