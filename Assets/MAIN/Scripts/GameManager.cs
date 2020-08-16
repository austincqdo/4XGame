using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    #region Camera
    public CameraFollow cameraFollow;
    public Transform unitTransform;
    #endregion

    private Tilemap map;
    private Vector3 spawnPosition;

    //// Delete this when I implement fetching players from scene.
    //private Player[] otherPlayersArr = { new Player("P2") };

    //public Player currentPlayer;
    //public Queue<Player> otherPlayers;
    //// Start is called before the first frame update

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        cameraFollow.Setup(() => unitTransform.position);
        
        map = GameObject.Find("BaseTilemap").GetComponent<Tilemap>();
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
