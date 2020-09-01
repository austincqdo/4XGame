using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

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


    //// Delete this when I implement fetching players from scene.
    //private Player[] otherPlayersArr = { new Player("P2") };

    public Player currentPlayer;
    //public Queue<Player> otherPlayers;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        cameraFollow.Setup(() => unitTransform.position);        
        spawnPosition = map.GetCellCenterWorld(new Vector3Int(-3, 3, 0));
    }

    public Vector3 GetSpawnPosition()
    {
        return this.spawnPosition;
    }

    public void UpdateSpawnPosition()
    {
        spawnPosition = map.GetCellCenterWorld(map.WorldToCell(spawnPosition) + new Vector3Int(0, 1, 0));
    }
}
