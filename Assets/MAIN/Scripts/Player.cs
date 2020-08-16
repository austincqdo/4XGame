using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public GameManager gameManager;
    public SelectManager selectManager;
    public GameObject canvas;

    private Tilemap map;

    [SerializeField]
    [Tooltip("List of units owned by player.")]
    private List<GameObject> units = new List<GameObject>();


    void Awake()
    {
        map = GameObject.Find("BaseTilemap").GetComponent<Tilemap>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        canvas = GameObject.Find("Canvas");
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

        // Add unit UI to canvas
        unit.healthBar.gameObject.GetComponent<RectTransform>().SetParent(canvas.GetComponent<RectTransform>());
        unit.healthBar.gameObject.SetActive(false);

        units.Add(newUnit);
        gameManager.UpdateSpawnPosition();
    }
}