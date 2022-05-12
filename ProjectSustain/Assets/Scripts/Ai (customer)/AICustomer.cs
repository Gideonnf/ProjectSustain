using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AICustomer : MonoBehaviour
{
    public NavMeshAgent agent;
    [System.NonSerialized] public FoodItem foodOrder = null;

    void Update()
    {
        //MoveAgent();
    }

    public void MoveAgent(Transform transform)
    {
     //   if (AIManager.Instance.moveAgent)
      //  {
            Debug.Log(transform.position);
            agent.SetDestination(transform.position);
      //  }
    }

    public void GenerateOrder()
    {
        if (foodOrder == null)
        {
            // Generate a order
            FoodItem newOrder = FoodManager.Instance.GetFoodOrder();
            if (newOrder != null)
            {
                foodOrder = newOrder;
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
