using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class PlayerController : MonoBehaviour
{
    Rigidbody playerRigidbody;
    PlayerInput playerInput;
    InputAction movement;

    [Tooltip("Speed of the player")]
    public float playerSpeed = 5.0f;
    
    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        playerRigidbody = GetComponent<Rigidbody>();
        movement = playerInput.actions["Movement"];
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 movementVector = movement.ReadValue<Vector2>();
        //Debug.Log(movementVector);
        // transform.position += new Vector3(movementVector.x, 0, movementVector.y) * playerSpeed;
        Debug.Log(new Vector3(movementVector.x, 0, movementVector.y) * playerSpeed);
        playerRigidbody.AddForce(new Vector3(movementVector.x, 0, movementVector.y) * playerSpeed, ForceMode.Force);
    }

    public void Movement(InputAction.CallbackContext context)
    {
        Debug.Log(context);
        Vector2 movementVector = context.ReadValue<Vector2>();
        Debug.Log(movementVector);
        playerRigidbody.AddForce(new Vector3(movementVector.x, 0, movementVector.y) * playerSpeed, ForceMode.Force);

    }

    public void Interact(InputAction.CallbackContext context)
    {
        Debug.Log(context);
        if (context.performed)
        {
            Debug.Log("Player Interacting");
        }
    }
}
