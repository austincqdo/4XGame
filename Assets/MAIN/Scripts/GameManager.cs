using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.InputSystem;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    #region Camera
    public CameraFollow cameraFollow;
    public Transform unitTransform;
    #endregion

    public Tilemap map;
    private Vector3 spawnPosition;

    #region Turn System
    public int TurnCounter { get; set; }
    #endregion


    public int NumPlayers { get; set; }
    public int CurrentPlayer { get; set; }

    public GameObject playerPrefab;

    public List<Player> PlayersList { get; set; } = new List<Player>();
    public List<string> PlayerThemes { get; set; } = new List<string>() { "Orange", "Purple" };

    void Awake()
    {
        instance = this;
        NumPlayers = 0;
        CurrentPlayer = 0;
    }

    void Start()
    {
        cameraFollow.Setup(() => unitTransform.position);        
        spawnPosition = map.GetCellCenterWorld(new Vector3Int(-3, 3, 0));

        // Delete later
        PlayersList.Add(GameObject.Find("Player").GetComponent<Player>());
    }

    void Update()
    {
        // Delete this when we instantiate players upon connect to host.
        if (Keyboard.current.pKey.wasPressedThisFrame)
        {
            GameObject newPlayer = Instantiate(playerPrefab);
            newPlayer.GetComponent<Player>().PlayerID = NumPlayers;
            PlayersList.Add(newPlayer.GetComponent<Player>());
        }
    }

    public Vector3 GetSpawnPosition()
    {
        return this.spawnPosition;
    }

    public void UpdateSpawnPosition()
    {
        spawnPosition = map.GetCellCenterWorld(map.WorldToCell(spawnPosition) + new Vector3Int(0, 1, 0));
    }

    public void EndTurn()
    {
        // Hide current player unit canvas.
        Unit selectedUnit = PlayersList[CurrentPlayer].SelectedUnit;
        if (selectedUnit)
        {
            selectedUnit.Deselect();
        }

        if (CurrentPlayer == NumPlayers - 1)
        {
            TurnCounter += 1;
            DefaultUI.instance.TurnDisplay.text = "Turn " + TurnCounter.ToString();
        }
        CurrentPlayer = (CurrentPlayer + 1) % NumPlayers;
    }
}
