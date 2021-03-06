using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System;
using database;
using Firebase.Firestore;
using Firebase.Extensions;
public class PlayerObject
{
    // Stores a reference to the input component
    public PlayerInput playerInput;
    // Stores reference to the index of the player
    public int playerIndex;
    
    public PlayerObject (PlayerInput input)
    {
        playerInput = input;
        playerIndex = input.playerIndex;
    }
}

public class PlayerManager : SingletonBase<PlayerManager>
{
    public enum Page
    {
        MainMenu,
        Leaderboard,
        Options
    };

    [Tooltip("The prefab of the player that will be created depending on players playing")]
    public GameObject playerBase;

    [Tooltip("List of players currently active")]
    public List<PlayerObject> playerList = new List<PlayerObject>();

    public float gameTime = 0.0f;
    public GameObject controllableLight;
    public GameObject bookObject;
    public GameObject scoreObject;
    public GameObject timeObject;
    public GameObject eventObjec;
    public GameObject menuObject;
    public float currentScore;
    public Page currPage;

    Light lightRef;
    float gameTimer = 0.0f;
    Color positiveColor;
    Color negativeColor;
    bool eventText = false;
    public bool gameEnd = false;
    float eventTimer = 0.0f;

    private PlayerInputManager inputManager;

    public bool freezeMovement = false;
    [System.NonSerialized] public bool InBook = false;
    [System.NonSerialized] public bool InMenu = false;
    [System.NonSerialized] public int playerID;

    // For testing setting spawn on creating
    // Ideally will be handled by a scene management script instead of the player manager since spawn locations will
    // be different depending on map, scene, etc
    public List<Transform> ListOfSpawns = new List<Transform>();

    public override void Awake()
    {
        base.Awake();

        inputManager = GetComponent<PlayerInputManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        InMenu = true;
        freezeMovement = true;
        lightRef = controllableLight.GetComponent<Light>();
        positiveColor = scoreObject.GetComponent<TextMeshProUGUI>().color;
        negativeColor = timeObject.GetComponent<TextMeshProUGUI>().color;
    }

    // Update is called once per frame
    void Update()
    {
        if (InMenu == false)
        {
            gameTimer += Time.deltaTime;
        }

        if (InMenu || InBook)
            freezeMovement = true;
        else
            freezeMovement = false;

        float scaledTime = gameTimer / gameTime;
        Vector3 currentRGB = new Vector3(255, lightRef.color.g, lightRef.color.b);
        Color newColor = lightRef.color;
        //Debug.Log(scaledTime);
        Vector3 newRGB = Vector3.Lerp(currentRGB, new Vector3(255, 0, 0), scaledTime * Time.deltaTime);
        newColor.g = newRGB.y;
        newColor.b = newRGB.z;
        lightRef.color = newColor;

        float timeLeft = gameTime - gameTimer;
        timeObject.GetComponent<TextMeshProUGUI>().text = "Time: " + timeLeft.ToString("F2");

        if (eventText)
        {
            eventTimer += Time.deltaTime;
            if(eventTimer > 1.0f)
            {
                eventText = false;
                eventObjec.SetActive(false);
            }
        }

        // Game end
        if (gameTimer >= gameTime)
        {
            gameEnd = true;
            gameTimer = 0.0f;
            EndGame();
        }
    }

    //temporarily put here
    public string scorepath = "scores";
    public void WriteScores(float score)
    {
        Int32 key = (Int32)(DateTime.Now.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
        var playerData = new player_scores();
        playerData.p_id = 1;
        playerData.s_id = key;
        playerData.s_level = 1;
        playerData.s_score = score;

        var firestore = FirebaseFirestore.DefaultInstance;
        firestore.Document(scorepath + "/" + key).SetAsync(playerData);
    }


    void EndGame()
    {
        menuObject.GetComponent<MenuController>().EndGame();

        WriteScores(currentScore);
        AIManager.Instance.ResetAI();
        //JUNHAO LOOK HERE

        //JUNHAO LOOK HERE
        menuObject.GetComponent<MenuController>().GetScoreboardData();

        //reset their position
        for (int i = 0; i < playerList.Count; ++i)
        {
            playerList[i].playerInput.gameObject.transform.position = ListOfSpawns[i].position;
        }
    }



    /// <summary>
    /// For adding players to the game
    /// Will add them to the list, create an instance of the player and any relevant linking, reading of data
    /// </summary>
    /// <param name="input"> References the player input component that all players spawn with</param>
    void AddPlayer(PlayerInput input)
    {
        playerList.Add(new PlayerObject(input));
        // Store the new players as a child of the manager
        // That way, the manager can drag the players between scenes
        // since the manager is a singleton
        input.transform.SetParent(transform);
        
    }

    public void AddScore(float score)
    {
        currentScore += score;
        // update the score
        scoreObject.GetComponent<TextMeshProUGUI>().text = "Score: " + currentScore.ToString();
        eventText = true;
        eventObjec.SetActive(true);
        eventObjec.GetComponent<TextMeshProUGUI>().color = positiveColor;
        eventObjec.GetComponent<TextMeshProUGUI>().text = "CO2 Emission Prevented + 2.11kg";
    }

    public void RemoveScore(float score)
    {
        currentScore -= score;
        scoreObject.GetComponent<TextMeshProUGUI>().text = "Score: " + currentScore.ToString();
        eventText = true;
        eventObjec.SetActive(true);
        eventObjec.GetComponent<TextMeshProUGUI>().color = negativeColor;
        eventObjec.GetComponent<TextMeshProUGUI>().text = "You have wasted " + score.ToString();

    }

    public void OnPlayerJoin(PlayerInput input)
    {
        Debug.Log(input.playerIndex);
        // If the new player that joined is not in the list of players we keep track of
        // Add them back in
        if (!playerList.Any(player => player.playerIndex == input.playerIndex))
        {
            //Debug.Log(ListOfSpawns[input.playerIndex].position);
           // Debug.Log("Before " + input.transform.position);
            input.transform.position = ListOfSpawns[input.playerIndex].position;
          //  Debug.Log("After " + input.transform.position);

            AddPlayer(input);
            // For testing of spawn setting
        }
        Debug.Log("Player Joined");
    }

    public void OnPlayerLeave()
    {
        Debug.Log("Player Leaved");
    }
}


