using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using Firebase.Firestore;
using Firebase.Extensions;
using UnityEngine.Assertions;
using database;

public class MenuController : MonoBehaviour
{

    public string progresspath = "progress";
    public string playerpath = "players";
    public string scorepath = "scores";
    public string key = "key";

    public GameObject camera;
    public GameObject optionsPosRot;
    public GameObject mainmenuPosRot;
    public GameObject leaderboardsPosRot;

    public GameObject mainmenuholder;
    public GameObject optionsmenuholder;
    public GameObject leaderboardsmenuholder;

    public bool isLerping;
    public Transform lerpTo;
    public TMP_Text fullscreenToggleText;
    public Scrollbar volumeSlider;

    public float movespeed;
    public float rotspeed;

    public List<TMP_Text> nametext;
    public List<TMP_Text> scoretext;

    public List<player_scores> scores = new List<player_scores>();

    public void Awake()
    {
        GetScoreboardData();
        isLerping = true;
        lerpTo = mainmenuPosRot.transform;
    }



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
    public void LerpMenu()
    {
        isLerping = true;
        lerpTo = mainmenuPosRot.transform;
        optionsmenuholder.SetActive(false);
    }


    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isLerping = true;
            lerpTo = mainmenuPosRot.transform;
            optionsmenuholder.SetActive(false);
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
        PlayerManager.Instance.freezeMovement = false;
        PlayerManager.Instance.InMenu = false;

        gameObject.SetActive(false);
    }

    public void EndGame()
    {
        gameObject.SetActive(true);
        PlayerManager.Instance.freezeMovement = true;
        PlayerManager.Instance.InMenu = true;

    }

    public void onLeaderboardsButtonClicked()
    {
        putscoreboarddata();
        isLerping = true;
        lerpTo = leaderboardsPosRot.transform;
        movespeed = 4;
        rotspeed = 2;
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


    public async void GetScoreboardData()
    {
        Debug.Log("getting score");
        scores.Clear();
        var firestore = FirebaseFirestore.DefaultInstance;
        firestore.Collection(scorepath).GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            Assert.IsNull(task.Exception);

            foreach (var playerdata in task.Result)
            {
                player_scores converted_data = playerdata.ConvertTo<player_scores>();
                scores.Add(converted_data);
                Debug.Log(scores.Count);
            }
        });

        
        
    }

    public void putscoreboarddata()
    {
        Debug.Log(scores.Count);
        for (int i = 0; i < 5; i++)
        {
            Debug.Log(scores.Count + "|" + i.ToString());
            if (scores.Count > i)
            {
                Debug.Log("insert");
                scoretext[i].SetText(scores[i].s_score.ToString());
                nametext[i].SetText(scores[i].s_id.ToString());
            }
            else
            {
                scoretext[i].SetText("---");
                nametext[i].SetText("---");
            }
        }
    }


}
