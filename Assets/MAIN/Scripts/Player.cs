using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public GameManager gameManager;
    private Tilemap map;

    [SerializeField]
    [Tooltip("List of units owned by player.")]
    private List<GameObject> units = new List<GameObject>();


    void Awake()
    {
        map = GameObject.Find("BaseTilemap").GetComponent<Tilemap>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
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

        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            DetectSelect();
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

    private void SpawnUnit(string unitType = "BasicUnit")
    {
        GameObject unitPrefab = (GameObject) Resources.Load("UnitTypes/" + unitType);
        GameObject newUnit = Instantiate(unitPrefab, gameManager.GetSpawnPosition(), Quaternion.identity);

        // Initialize unit fields
        Unit unit = newUnit.GetComponent(unitType) as Unit;
        unit.owner = this;
        unit.id = units.Count;

        units.Add(newUnit);

        // Select only new unit.
        DeselectAll();
        unit.Select();

        gameManager.UpdateSpawnPosition();
    }


    void DetectSelect()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()), Vector2.zero);
        if (hit.collider)
        {
            Unit unit = hit.collider.gameObject.GetComponent<Unit>();
            if (!unit.selected)
            {
                DeselectAll();
                unit.Select();
            }
        }
        else //no hit, so deselect all
        {
            DeselectAll();
        }
    }

    public void DeselectAll()
    {
        GameObject selectedUnitObject = units.Find(s => s.GetComponent<Unit>().selected);
        if (selectedUnitObject)
        {
            selectedUnitObject.GetComponent<Unit>().Deselect();
        }
    }
}