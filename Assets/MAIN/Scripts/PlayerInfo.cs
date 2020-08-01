using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInfo : MonoBehaviour
{
    public string Username { get; set; }
    private SpawnManager spawnManager;

    public List<GameObject> units = new List<GameObject>();
    public List<bool> selectedUnits = new List<bool>();


    void Awake()
    {
        //units = new List<GameObject>();
        //selectedUnits = new List<bool>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //spawnManager.SpawnUnit("BasicUnit");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
