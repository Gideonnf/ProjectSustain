using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class MenuController : MonoBehaviour
{
    public GameObject camera;
    public GameObject optionsPosRot;
    public GameObject mainmenuPosRot;
    public GameObject prestartPosRot;

    public GameObject mainmenuholder;
    public GameObject prestartmenuholder;
    public GameObject optionsmenuholder;

    public bool isLerping;
    public Transform lerpTo;
    public TMP_Text fullscreenToggleText;
    public Scrollbar volumeSlider;

    public float movespeed;
    public float rotspeed;

    public void Start()
    {
        movespeed = 1;
        rotspeed = 1;
        
        if(Screen.fullScreen == true)
        {
            fullscreenToggleText.text = "WINDOWED";
        }
        else if(Screen.fullScreen == false)
        {
            fullscreenToggleText.text = "WINDOWED";
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isLerping = true;
            lerpTo = mainmenuPosRot.transform;
            optionsmenuholder.SetActive(false);
            prestartmenuholder.SetActive(false);
        }   
        if(camera==null)
        {
            return;
        }
        if (isLerping == false)
        {
            return;
        }
        lerpRotPos(lerpTo, movespeed, rotspeed);
    }
    
    public void onStartGameButtonClicked()
    {
        Debug.Log("GAME START");
        isLerping = true;
        lerpTo = prestartPosRot.transform;
        movespeed = 4;
        rotspeed = 2;
        prestartmenuholder.SetActive(true);
    }

    public void onStartMultiplayer()
    {

    }

    public void onStartSinglePlayer()
    {

    }

    public void onOptionsButtonClicked()
    {
        Debug.Log("Options CLICKED");
        isLerping = true;
        lerpTo = optionsPosRot.transform;
        movespeed = 1;
        rotspeed = 4;
        optionsmenuholder.SetActive(true);
    }

    public void onVolumeSliderSlided()
    {
        AudioListener.volume = volumeSlider.value;
    }

    public void onToggleFullscreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
        Debug.Log(Screen.fullScreen);
        if (Screen.fullScreen == true)
        {
            fullscreenToggleText.text = "WINDOWED";
        }
        else if (Screen.fullScreen == false)
        {
            fullscreenToggleText.text = "FULLSCREEN";
        }
    }

    public void onExitButtonClicked()
    {
        Application.Quit();
    }


    public void lerpRotPos(Transform endPosRot, float movespeed, float rotspeed)
    {
        camera.transform.position = Vector3.Lerp(camera.transform.position, endPosRot.position, movespeed * Time.deltaTime);
        camera.transform.rotation = Quaternion.Lerp(camera.transform.rotation, endPosRot.rotation, rotspeed * Time.deltaTime);
        if (Vector3.Distance(camera.transform.position, endPosRot.position) < 0.1 && Mathf.Abs(Quaternion.Dot(camera.transform.rotation, endPosRot.rotation)) < 0.1)
        {
            camera.transform.position = endPosRot.position;
            camera.transform.rotation = endPosRot.rotation;
            //lerp complete
            isLerping = false;
            lerpTo = null;
        }
    }

}
