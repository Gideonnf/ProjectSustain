using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    PlayerManager PMref;
    Vector3 originalZoom;
    Vector3 currentZoom;
    // Start is called before the first frame update
    void Start()
    {
        PMref = PlayerManager.Instance;
        originalZoom = transform.position;
        currentZoom = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // There are more than 1 player in the game
        if (PMref.playerList.Count > 1)
        {
            
        }
    }
}
