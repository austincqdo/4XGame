using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.InputSystem;
using TMPro;

public class Player : MonoBehaviour
{
    private Tilemap map;

    private Unit selectedUnit;

    [SerializeField]
    [Tooltip("List of units owned by player.")]
    private List<GameObject> units = new List<GameObject>();

    #region territory
    public Color PlayerColor { get; set; }
    #endregion

    public int PlayerID { get; set; }

    public string Theme { get; set; }

    void Awake()
    {
        map = GameObject.Find("BaseTilemap").GetComponent<Tilemap>();
        PlayerColor = Color.blue;
    }

    void Start()
    {
        this.PlayerID = GameManager.instance.NumPlayers;
        GameManager.instance.NumPlayers += 1;

        this.Theme = GameManager.instance.PlayerThemes[PlayerID];
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.CurrentPlayer == this.PlayerID)
        {
            if (Keyboard.current.gKey.wasPressedThisFrame)
            {
                SpawnUnit();
            }

            if (Keyboard.current.hKey.wasPressedThisFrame)
            {
                if (selectedUnit)
                {
                    selectedUnit.FoundTerritory();
                }
            }

            if (Mouse.current.rightButton.wasPressedThisFrame)
            {
                ProcessClick("Right");
            }
            else if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                ProcessClick("Left");
            }
        }
    }


    public List<GameObject> GetUnits()
    {
        return this.units;
    }

    public void AddUnit(GameObject unit)
    {
        this.units.Add(unit);
    }

    public Unit GetSelectedUnit()
    {
        return this.selectedUnit;
    }

    public void SetSelectedUnit(Unit selectedUnit)
    {
        this.selectedUnit = selectedUnit;
    }

    private void SpawnUnit(string unitType = "BasicUnit")
    {
        GameObject unitPrefab = (GameObject) Resources.Load("Units/" + Theme + "/" + unitType);
        GameObject newUnit = Instantiate(unitPrefab, GameManager.instance.GetSpawnPosition(), Quaternion.identity);

        // Initialize unit fields
        Unit unit = newUnit.GetComponent(Theme + unitType) as Unit;
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


    void ProcessClick(string which)
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        int layerMask = 1 << 8;
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, Mathf.Infinity, layerMask);
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

    public void ClaimTile(WorldTile tile)
    {
        tile.Owner = this;
        map.SetColor(tile.Coord, PlayerColor);
    }
}