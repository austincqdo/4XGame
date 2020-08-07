using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public Player owner;
    public int id;
    public float health;
    public bool selected;
    public string type;
    private SpawnManager spawnManager;

    protected virtual void Awake()
    {
        this.spawnManager = owner.GetComponent<SpawnManager>();
        selected = false;
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (!health)
        {
            spawnManager.DespawnUnit(this);
        }
    }
}
