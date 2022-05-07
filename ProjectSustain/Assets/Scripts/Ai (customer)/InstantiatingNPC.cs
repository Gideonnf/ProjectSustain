using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class InstantiatingNPC : MonoBehaviour
{
    public GameObject gameObject;
    public NavMeshSurface surface;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(gameObject, transform.position, gameObject.transform.rotation);
            surface.BuildNavMesh();
        }
    }
}
