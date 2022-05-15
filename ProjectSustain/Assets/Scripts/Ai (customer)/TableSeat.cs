using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableSeat : MonoBehaviour
{
    [Tooltip("Assign an ID to the table")]
    // Any new seats will just be an increment of the current number of seats
    // if in the future addable seats were available
    public int tableID;
    public bool isAvailable;
    public bool isSeatable;

    // Start is called before the first frame update
    void Start()
    {
        isAvailable = true;
        // Set isSeatable to false always
        // Checking for if it is seatable will be done
        isSeatable = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Check for a table infront of it
        if (isSeatable == false)
        {
            // Set the origin to be a bit above the chair as the chair is just a plate on the ground at the moment
            Vector3 origin = transform.position + new Vector3(0, 0.2f, 0);
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            Debug.DrawRay(origin, forward * 2.0f, Color.red);
            RaycastHit[] hit = Physics.RaycastAll(origin, forward, 2.0f);
            if (hit.Length > 0)
            {
                // Loop through the collided objects
                foreach(RaycastHit collidable in hit)
                {
                    if (collidable.collider.tag == "Table")
                    {
                        isSeatable = true;
                        //Debug.Log("Collided with table");
                        break;
                    }
                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Colliding with " + other.name);
        // Can call AI here to start food ordering
        AICustomer ai = other.GetComponent<AICustomer>();
        if (ai != null)
        {
            // Only if they aren't serve yet
            if (ai.isServed == false)
            {
                ai.GenerateOrder();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        AICustomer ai = other.GetComponent<AICustomer>();
        if (ai != null)
        {
            TableManager.Instance.UpdateTable(gameObject);

        }
     }
 }
