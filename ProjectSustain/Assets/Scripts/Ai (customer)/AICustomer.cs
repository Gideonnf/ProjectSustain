using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class AICustomer : MonoBehaviour
{
    public NavMeshAgent agent;
    [System.NonSerialized] public FoodItem foodOrder = null;
    [System.NonSerialized] public bool isFinished = false;
    [System.NonSerialized] public bool isServed = false;
    public GameObject foodBubble;
    [System.NonSerialized] public bool isOrdering;
    float timer;

    // TO NOTE
    // Maybe changing customer timer from food based to customer based
    // Allows for more dishes per customer instead of 1 dish 1 customer.


    void Update()
    {
        //MoveAgent();
        if (isOrdering && foodOrder != null)
        {
            timer += Time.deltaTime;
            if (timer >= foodOrder.waitingTime)
            {
                // AI leaves
            }

        }


    }

    public void MoveAgent(Vector3 vector3)
    {
        //   if (AIManager.Instance.moveAgent)
        //  {
        //Debug.Log(transform.position);
        agent.SetDestination(vector3);
        //  }
    }

    public void GenerateOrder()
    {
        isOrdering = true;

        if (foodOrder == null)
        {
            // Generate a order
            FoodItem newOrder = FoodManager.Instance.GetFoodOrder();
            if (newOrder != null)
            {
                foodOrder = newOrder;
                Debug.Log(foodOrder.foodSprite);
                // Can enable a speech bubble ontop to show the food item
                foodBubble.SetActive(true);
                foodBubble.transform.GetChild(0).GetComponent<Image>().sprite = foodOrder.foodSprite;
            }
            else
            {
                // Shouldn't reach here
                Debug.LogError("No order can be generated");
            }
        }
    }

    public void ServeCustomer(GameObject playerObject)
    {
        PlayerController playerRef = playerObject.GetComponent<PlayerController>();
        // check for plate object
        if (playerRef.plateObject != null)
        {
            if (playerRef.plateObject.GetComponent<PlateObject>().complete)
            {
                // Can make the AI sit down and eat for awhile
                // or straight away leave the shop
                // am doing the 2nd one first

                GameObject plate = playerRef.plateObject;
                playerRef.plateObject = null;

                isServed = true;
                isOrdering = false;
                foodOrder = null;

                foodBubble.SetActive(false);
                MoveAgent(AIManager.Instance.exitPoint.position);

                Destroy(plate);

            }
        }
    }

    public void SimulateFinish()
    {
        isServed = true;
        isOrdering = false;
        foodOrder = null;

        foodBubble.SetActive(false);

        MoveAgent(AIManager.Instance.exitPoint.position);

       // Destroy(plate);
    }
}
