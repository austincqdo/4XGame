using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpawnManager : MonoBehaviour
{
    private Player player;
    private Vector3 spawnPosition;

    private List<GameObject> units;
    //private List<bool> selectedUnits;


    void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
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
        spawnPosition.y++;
    }

    public void DespawnUnit(GameObject unit)
    {
        player.GetUnits().Remove(unit);
        Destroy(unit);
    }
}
