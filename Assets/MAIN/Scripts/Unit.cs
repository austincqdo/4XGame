using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    public Player owner;
    public int id { get; set; }
    public string type { get; set; }
    public float health { get; set; }
    public bool selected { get; set; } = false;
    public SpawnManager spawnManager { get; set; }

    protected void Awake()
    {
        SetType();
        SetHealth();
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        this.spawnManager = owner.GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (health <= 0)
        {
            spawnManager.DespawnUnit(gameObject);
        }
    }

    protected abstract void SetType();

    protected abstract void SetHealth();
}
