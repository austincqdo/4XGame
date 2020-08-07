using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.InputSystem;
//using UnityEngine.EventSystems;


/**
Handles logic related to unit movement
**/


public class UnitMovement : MonoBehaviour
{
    private Unit unit;
    private Player owner;

    private int id;

    private Vector3 movementInput;
    private Vector3 direction;

    public TileSelector selector;
    public Tilemap fogOfWar;

    bool hasMoved;

    public int vision = 1;


    void Awake()
    {
        selector = GameObject.Find("BaseTilemap").GetComponent<TileSelector>();
        fogOfWar = GameObject.Find("FogOfWar").GetComponent<Tilemap>();

        // Track this unit with the camera
        GameObject.Find("GameManager").GetComponent<GameManager>().playerTransform = transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        unit = gameObject.GetComponent<Unit>();
        owner = unit.owner;
        id = unit.id;
    }

    // Update is called once per frame
    void Update()
    {

        if (movementInput.x == 0)
        {
            hasMoved = false;
        }
        else if (movementInput.x != 0 && !hasMoved)
        {
            hasMoved = true;

            GetMovementDirection();
        }

    }


    public void GetMovementDirection()
    {
        if (movementInput.y == 0f)
        {
            direction = Vector3.Scale(movementInput, new Vector3(0.6f, 0.6f));
            print(direction);
        } else
        {
            int xs = Math.Sign(movementInput.x);
            int ys = Math.Sign(movementInput.y);
            direction = movementInput - new Vector3(xs * 0.2f, ys * 0.2f);
            print(direction);
        }

        transform.position += direction;
        UpdateFogOfWar();
    }

    public void OnMove(InputValue value)
    {
        movementInput = (Vector3) value.Get<Vector2>();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        transform.position -= direction;
    }


    void UpdateFogOfWar()
    {
        Vector3Int currentPlayerTile = fogOfWar.WorldToCell(transform.position);

        //Clear the surrounding tiles
        for (int x = -vision; x <= vision; x++)
        {
            for (int y = -vision; y <= vision; y++)
            {
                fogOfWar.SetTile(currentPlayerTile + new Vector3Int(x, y, 0), null);
            }

        }

    }

    void OnMouseDown()
    {
        // Point-to-click movement.
        print("does this work?");
        AStar aStar = gameObject.AddComponent(typeof(AStar)) as AStar;
        List<Vector3Int> path = aStar.FindPath(selector.GetSelectedTile());

        // do stuff

        Destroy(aStar);
        print(path);
    }
}
