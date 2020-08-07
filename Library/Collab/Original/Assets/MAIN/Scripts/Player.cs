using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public SpawnManager spawnManager;
    public SelectManager selectManager;

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
