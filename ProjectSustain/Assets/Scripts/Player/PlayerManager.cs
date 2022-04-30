using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : SingletonBase<PlayerManager>
{
    [Tooltip("The prefab of the player that will be created depending on players playing")]
    public GameObject playerBase;

    [Tooltip("List of players currently active")]
    public GameObject[] playerList;

    PlayerInputManager inputManager;

    private void Awake()
    {
        inputManager = GetComponent<PlayerInputManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    /// <summary>
    /// For adding players to the game
    /// Will add them to the list, create an instance of the player and any relevant linking, reading of data
    /// </summary>
    void AddPlayer()
    {

    }

    public void OnPlayerJoin()
    {
        Debug.Log("Player Joined");
    }

    public void OnPlayerLeave()
    {
        Debug.Log("Player Leaved");
    }
}


