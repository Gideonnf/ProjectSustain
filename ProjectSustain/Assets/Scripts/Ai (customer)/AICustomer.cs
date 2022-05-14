using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AICustomer : MonoBehaviour
{
    public NavMeshAgent agent;
    [System.NonSerialized] public FoodItem foodOrder = null;
    bool isOrdering;
    float timer = 0.0f;
    public bool isFinished;
    

    // TO NOTE
    // Maybe changing customer timer from food based to customer based
    // Allows for more dishes per customer instead of 1 dish 1 customer.

    private void Start()
    {
        isFinished = false;
    }

    void Update()
    {
        //MoveAgent();
        if (isOrdering && foodOrder != null)
        {
            timer += Time.deltaTime;
            if (timer > foodOrder.waitingTime)
            {
                // Too long
            }
        }
    }

    public void MoveAgent(Transform transform)
    {
     //   if (AIManager.Instance.moveAgent)
      //  {
            //Debug.Log(transform.position);
            agent.SetDestination(transform.position);
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
                Debug.Log(foodOrder);
                // Can enable a speech bubble ontop to show the food item
            }
            else
            {
                // Shouldn't reach here
                Debug.LogError("No order can be generated");
            }
        }
    }
}
