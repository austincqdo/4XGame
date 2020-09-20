using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using TMPro;

public class Player : MonoBehaviour
{
    private Tilemap map;

    public Unit SelectedUnit { get; set; }

    [SerializeField]
    [Tooltip("List of units owned by player.")]
    private List<GameObject> units = new List<GameObject>();

    #region territory
    public Color PlayerColor { get; set; }
    #endregion

    public int PlayerID { get; set; }

    public string Theme { get; set; }

    public RectTransform EndTurnButtonRect { get; set; }

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

        // Make sure end turn button blocks raycasts.
        EndTurnButtonRect = GameObject.Find("Canvas").GetComponentInChildren<RectTransform>();
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
                if (SelectedUnit)
                {
                    SelectedUnit.FoundTerritory();
                }
            }

            // If not interacting with the UI.
            if (!RectTransformUtility.RectangleContainsScreenPoint(EndTurnButtonRect, Mouse.current.position.ReadValue()))
            {
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
        return this.SelectedUnit;
    }

    public void SetSelectedUnit(Unit selectedUnit)
    {
        this.SelectedUnit = selectedUnit;
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
        if (SelectedUnit) { SelectedUnit.Deselect(); }
        unit.Select();

        GameTiles.instance.tiles[newUnit.transform.position].Occupied = true;
        GameManager.instance.UpdateSpawnPosition();
    }


    void ProcessClick(string which)
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        int layerMask = 1 << 8;
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, Mathf.Infinity, layerMask);

        // If a collider was hit by the raycast...
        Collider2D hitCollider = hit.collider;
        if (hitCollider)
        {
            GameObject hitGameObject = hitCollider.gameObject;
            switch (which)
            {
                case "Right":
                    // If a unit is clicked
                    Unit unitHit;
                    if ((unitHit = hitGameObject.GetComponent<Unit>()) != null)
                    {
                        if (SelectedUnit) { SelectedUnit.Deselect(); }
                        if (unitHit.owner == this)
                        {
                            unitHit.Select();
                        }
                    }
                    break;
                case "Left":
                    if (SelectedUnit) //selectedUnit attacking the clicked unit
                    {
                        Unit targetUnit;
                        if ((targetUnit = hitGameObject.GetComponent<Unit>()) != null)
                        {
                            // Can't attack your own units
                            if (targetUnit.owner != this)
                            {
                                SelectedUnit.Attack(targetUnit, 20);
                            }
                        }
                    }
                    break;
            }
        }
        else
        {
            if (SelectedUnit) { SelectedUnit.Deselect(); }
        }
    }

    public void ClaimTile(WorldTile tile)
    {
        tile.Owner = this;
        map.SetColor(tile.Coord, PlayerColor);
    }
}