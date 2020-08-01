using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public CameraFollow cameraFollow;
    public Transform playerTransform;

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
        cameraFollow.Setup(() => playerTransform.position);
        //AddPlayers();
    }


    //public void NextPlayer()
    //{
    //    otherPlayers.Enqueue(currentPlayer);
    //    currentPlayer = otherPlayers.Dequeue();
    //}

    //void AddPlayers()
    //{
    //    currentPlayer = new Player("P1");
    //    otherPlayers = new Queue<Player>(otherPlayersArr);
    //}
}
