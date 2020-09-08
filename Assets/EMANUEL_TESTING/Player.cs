using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using Mirror;




public class Player : NetworkBehavior
{
    [SerializeField]
    [Tooltip("List of units owned by player.")]
    private List<GameObject> units = new List<GameObject>();


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer) return;
        
        if (Keyboard.current.gKey.wasPressedThisFrame)
        {
            SpawnUnit();
        }

        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            ProcessClick("Right");
        
        } else if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            ProcessClick("Left");
        }
    }


    void ProcessClick(string which)
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()), Vector2.zero);
        switch (which)
        {
            case "Right":
                if (selectedUnit) { selectedUnit.Deselect(); }
                if (hit.collider)
                {
                    Unit unit = hit.collider.gameObject.GetComponent<Unit>();
                    unit.Select();
                }
                break;
            case "Left":
                if (selectedUnit && hit.collider) //selectedUnit attacking the clicked unit
                {
                    Unit targetUnit = hit.collider.gameObject.GetComponent<Unit>();
                    if (selectedUnit != targetUnit)
                    {
                        selectedUnit.Attack(targetUnit, 20);
                    }
                }
                break;
        }
    }


    #region Commands (run on Server)

    [Command]
    private void unitSpawn(string unitType = "BasicUnit")
    {
        GameObject unitPrefab = (GameObject) Resources.Load("UnitTypes/" + unitType);
        GameObject newUnit = Instantiate(unitPrefab, GameManager.instance.GetSpawnPosition(), Quaternion.identity);

        // Initialize unit fields
        Unit unit = newUnit.GetComponent(unitType) as Unit;
        unit.owner = this;
        unit.id = units.Count;
        unit.nameInUI.text = unit.type + " " + unit.id;

        units.Add(newUnit);

        // Select only new unit.
        if (selectedUnit) { selectedUnit.Deselect(); }
        unit.Select();

        GameTiles.instance.tiles[newUnit.transform.position].Occupied = true;
        GameManager.instance.UpdateSpawnPosition();
    }



    [Command]
    void unitMove(){




    }
    [Command]
    void unitAttack(){



    }


    #endregion



}
