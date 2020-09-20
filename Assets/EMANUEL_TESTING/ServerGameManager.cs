using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;


/**
 A ServerGameManager is something that keeps track of what state the game is in, and 
 records and stores information for various purposes. 


 Server will use this like a database to keep track of
- player metadata:
    - resources
    - tech unlocks
    - units
    - territory
- map metadata (TileMap)
- other metadata (round, score, etc.)

 From this state data, one should be able to generate game save files.

 A ServerGameManager only exists during the main game scene, i.e. it is not created during a lobby scene.
**/


public class ServerGameManager : NetworkBehaviour
{
    public static ServerGameManager Instance {get; private set;}


    [Server]
    void Awake()
    {
        Debug.Log("Initializing Game Manager");
    }


    [Server]
    // Update is called once per frame
    void Update()
    {

    }
}
