using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public abstract class Unit : MonoBehaviour
{
    public Player owner;
    public int id { get; set; }
    public string type { get; set; }

    #region UI
    public GameObject canvas;
    public HealthBar healthBar;
    public TextMeshProUGUI nameInUI;
    public Canvas unitCanvas;
    public float maxHealth { get; set; }
    public float currentHealth { get; set; }
    #endregion

    protected virtual void Awake()
    {
        canvas = GameObject.Find("Canvas");
        
        GameObject unitCanvasPrefab = (GameObject) Resources.Load("UI/UnitCanvas");

        this.unitCanvas = Instantiate(unitCanvasPrefab, canvas.GetComponent<RectTransform>()).GetComponent<Canvas>();
        this.healthBar = unitCanvas.GetComponentInChildren<HealthBar>();
        this.nameInUI = unitCanvas.GetComponentInChildren<TextMeshProUGUI>();

        GameTiles.instance.UpdateFogOfWar(transform.position);
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        healthBar.SetMaxHealth(maxHealth);
        SetStartingHealth();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (currentHealth <= 0)
        {
            GameTiles.instance.tiles[transform.position].Occupied = false;
            owner.GetUnits().Remove(gameObject);
            Destroy(gameObject);
            Destroy(unitCanvas.gameObject);
        } 

        // test health bar
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(20);
        }
    }

    private void TakeDamage(float damage)
    {
        StartCoroutine(FlashRed());
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

    public void Select()
    {
        Color tmp = gameObject.GetComponent<SpriteRenderer>().color;
        tmp.a = .75f;
        gameObject.GetComponent<SpriteRenderer>().color = tmp;

        owner.SetSelectedUnit(this);

        if (!unitCanvas.gameObject.activeSelf)
        {
            ShowUI();
        }

        // Center the camera on this unit.
        GameManager.instance.unitTransform = transform;
    }

    public void Deselect()
    {
        Color tmp = gameObject.GetComponent<SpriteRenderer>().color;
        tmp.a = 1f;
        gameObject.GetComponent<SpriteRenderer>().color = tmp;

        owner.SetSelectedUnit(null);

        HideUI();
    }

    protected void SetStartingHealth()
    {
        currentHealth = maxHealth;
    }

    public void ShowUI()
    {
        unitCanvas.gameObject.SetActive(true);
    }

    public void HideUI()
    {
        unitCanvas.gameObject.SetActive(false);
    }

    public void Attack(Unit target, float amount)
    {
        target.TakeDamage(amount);
    }

    public IEnumerator FlashRed()
    {
        Color tmp = gameObject.GetComponent<SpriteRenderer>().color;
        gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(.1f);
        gameObject.GetComponent<SpriteRenderer>().color = tmp;
    }
}
