using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInfo : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The player this refers to.")]
    private Player player;

    public int numUnits { get; set; }

    [SerializeField]
    [Tooltip("List of units owned by player.")]
    private List<GameObject> units = new List<GameObject>();


    public Dictionary<string, GameObject> UnitTypes { get; set; }

    void Awake()
    {
        numUnits = 0;

        UnitTypes = new Dictionary<string, GameObject>();
        UnitTypes.Add("BasicUnit", (GameObject) Resources.Load("UnitTypes/BasicUnit"));
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public Player GetPlayer()
    {
        return this.player;
    }

    public List<GameObject> GetUnits()
    {
        return this.units;
    }

    public void AddUnit(GameObject unit)
    {
        this.units.Add(unit);
    }
}
