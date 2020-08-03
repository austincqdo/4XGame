using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpawnManager : MonoBehaviour
{
    private PlayerInfo playerInfo;
    private Vector3 spawnPosition;

    private List<GameObject> units;
    //private List<bool> selectedUnits;


    void Awake()
    {
        playerInfo = GameObject.Find("PlayerInfo").GetComponent<PlayerInfo>();
        spawnPosition = new Vector3(0f, 0f, 0f);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.gKey.wasPressedThisFrame)
        {
            SpawnUnit();
        }
    }

    public void SpawnUnit(string unitType="BasicUnit")
    {
        GameObject newUnit = Instantiate(playerInfo.UnitTypes[unitType], spawnPosition, Quaternion.identity);
        switch (unitType)
        {
            case "BasicUnit":
                BasicUnit basicUnit = newUnit.GetComponent<BasicUnit>();
                basicUnit.owner = playerInfo.GetPlayer();
                basicUnit.id = playerInfo.numUnits + 1;
                break;
        }
        playerInfo.numUnits++;

        playerInfo.AddUnit(newUnit);
        spawnPosition.y++;
    }

    public void DespawnUnit(GameObject unit)
    {
        playerInfo.GetUnits().Remove(unit);
        Destroy(unit);
        playerInfo.numUnits--;
    }
}
