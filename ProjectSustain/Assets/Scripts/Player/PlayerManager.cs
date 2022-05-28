using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

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
    public float currentScore;

    Light lightRef;
    float gameTimer = 0.0f;
    Color positiveColor;
    Color negativeColor;
    bool eventText = false;
    float eventTimer = 0.0f;

    private PlayerInputManager inputManager;

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
        lightRef = controllableLight.GetComponent<Light>();
        positiveColor = scoreObject.GetComponent<TextMeshProUGUI>().color;
        negativeColor = timeObject.GetComponent<TextMeshProUGUI>().color;
    }

    // Update is called once per frame
    void Update()
    {
        gameTimer += Time.deltaTime;
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


