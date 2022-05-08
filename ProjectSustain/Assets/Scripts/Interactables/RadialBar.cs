using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadialBar : MonoBehaviour
{
    private Image radialImage = null;
    private InteractBase interactBase = null;

  //  private float interactTime;
   // private float timer;

    private void Awake()
    {
        // Reference the circle image that'll be a child under the interactable base
        radialImage = GetComponentInChildren<Image>();
        // Reference to the base to retrieve info
        interactBase = GetComponent<InteractBase>();
       // interactTime = interactBase.interactTime;

    }
    // Start is called before the first frame update
    void Start()
    {
        //TestManager.Instance.GetAvailPosition(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        // If object is being interacted
        if (interactBase.interacting)
        {
            // turn it on first
            if (radialImage.enabled == false)
                radialImage.enabled = true;

            if (radialImage.fillAmount <= 1)
            {
               // float fillAmount = interactBase.interactTime / interactBase.interactTimer;
                radialImage.fillAmount = interactBase.interactTimer / interactBase.interactTime;
            }
        }
    }

    public void ResetRadial()
    {
        radialImage.fillAmount = 0.0f;
    }
}
