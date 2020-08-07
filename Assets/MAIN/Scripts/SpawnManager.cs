using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.InputSystem;

public class SpawnManager : MonoBehaviour
{
    private Player player;
    private Vector3 spawnPosition;

    private List<GameObject> units;
    private Tilemap map;

    void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        map = GameObject.Find("BaseTilemap").GetComponent<Tilemap>();

        spawnPosition = map.GetCellCenterWorld(new Vector3Int(-3, 3, 0));

        
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
        GameObject unit = (GameObject) Resources.Load("UnitTypes/" + unitType);
        GameObject newUnit = Instantiate(unit, spawnPosition, Quaternion.identity);
        switch (unitType)
        {
            case "BasicUnit":
                BasicUnit basicUnit = newUnit.GetComponent<BasicUnit>();
                basicUnit.owner = player;
                basicUnit.id = player.GetUnits().Count;
                break;
        }

        player.AddUnit(newUnit);
        spawnPosition = map.GetCellCenterWorld(map.WorldToCell(spawnPosition) + new Vector3Int(0, 1, 0));
    }

    public void DespawnUnit(GameObject unit)
    {
        player.GetUnits().Remove(unit);
        Destroy(unit);
    }
}
