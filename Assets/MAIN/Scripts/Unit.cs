using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class Unit : MonoBehaviour
{
    public Player owner;
    public int id { get; set; }
    public string type { get; set; }
    public bool selected { get; set; } = false;

    #region UI
    public HealthBar healthBar;
    public float maxHealth { get; set; }
    public float currentHealth { get; set; }


    #endregion

    protected virtual void Awake()
    {
        GameObject healthbarPrefab = (GameObject)Resources.Load("UI/Health Bar");
        this.healthBar = Instantiate(healthbarPrefab).GetComponent<HealthBar>();
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        
        healthBar.SetMaxHealth(maxHealth);
        //this.healthBar = GameObject.Find("Canvas").GetComponentInChildren<HealthBar>();
        //healthBar.SetMaxHealth(maxHealth);
        SetStartingHealth();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (currentHealth <= 0)
        {
            owner.GetUnits().Remove(gameObject);
            Destroy(gameObject);
        } 

        // test health bar
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(20);
        }
    }

    protected void TakeDamage(float damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

    public void Select()
    {
        Color tmp = gameObject.GetComponent<SpriteRenderer>().color;
        tmp.a = .75f;
        gameObject.GetComponent<SpriteRenderer>().color = tmp;
        selected = true;
    }

    public void Deselect()
    {
        Color tmp = gameObject.GetComponent<SpriteRenderer>().color;
        tmp.a = 1f;
        gameObject.GetComponent<SpriteRenderer>().color = tmp;
        selected = false;
    }

    protected void SetStartingHealth()
    {
        currentHealth = maxHealth;
    }

    public void ShowUI()
    {
        
    }

    public void HideUI()
    {

    }
}
