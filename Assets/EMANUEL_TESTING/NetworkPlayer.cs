using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;


using Mirror;




public class NetworkPlayer : NetworkBehaviour
{

    #region Fields

    [SerializeField]
    [Tooltip("List of unit prefabs that player can spawn.")]
    private List<GameObject> unitPrefabs = new List<GameObject>();


    [SerializeField]
    [Tooltip("List of units owned by player.")]
    private List<GameObject> units = new List<GameObject>();



    /**
    Player's Current Selected Unit (moveable, displaying unit context menu)
    **/
    private NetworkUnit selectedUnit;

    //private Tilemap map;

    private Vector2 mousePointer = new Vector2(0,0);


    private Vector3 spawnPos = new Vector3(0 ,0 ,0);

    #endregion


    #region Unity Functions

    // Start is called before the first frame update
    void Awake()
    {
        print(ServerGameManager.Instance);
    }

    // Update is called once per frame
    void Update()
    {
    }

    #endregion




    #region Local Input Events (only run from Local Player)
    void OnSpawnUnit()
    {
        if (!isLocalPlayer) return;
        
        
        CmdSpawnUnit(spawnPos);
    }



    /**
    Left Click handles selecting units, selecting tiles?, and interacting with ui

    
    **/
    void OnClick()
    {
        if (!isLocalPlayer) return;


        Debug.Log("Clicked");

        //deselect currently selected unit
        if (selectedUnit != null) { deselectUnit(); }




        RaycastHit2D hit = Physics2D.Raycast(mousePointer, Vector2.zero);

        //RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()), Vector2.zero);
       
        if (hit.collider != null){
            selectUnit(hit.collider.gameObject);
        }



    }


    void onPoint(InputValue val){
        if (!isLocalPlayer) return;
        mousePointer = val.Get<Vector2>();
    }



    #endregion



    #region Local Utilities

    private void selectUnit(GameObject unit){
        // update selected UI, like highlighting/context menu
        Debug.Log("Selected Unit");
        selectedUnit = unit.GetComponent<NetworkUnit>();
    }


    private void deselectUnit(){
        // remove selected UI, like highlighting/context menu
        Debug.Log("Deselected Unit");
        selectedUnit = null;
    }
    #endregion


    #region Commands (code run server side)

    [Command]
    private void CmdSpawnUnit(Vector3 spawnPosition)
    {
        Debug.Log("Spawning unit");
        GameObject unit = Instantiate(unitPrefabs[0], spawnPosition, Quaternion.identity);
        NetworkServer.Spawn(unit);
    }


    [Command]
    void CmdMoveUnit()
    {




    }
    [Command]
    void CmdAttackWithUnit()
    {



    }


    #endregion



}
