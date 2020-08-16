using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.InputSystem;
using System.Runtime.InteropServices;
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

    private Tilemap map;
    public TileSelector selector;
    public Tilemap fogOfWar;

    bool hasMoved;
    bool moving;

    public int vision = 1;

    SelectManager selectManager;


    void Awake()
    {
        moving = false;

        map = GameObject.Find("BaseTilemap").GetComponent<Tilemap>();
        selector = GameObject.Find("BaseTilemap").GetComponent<TileSelector>();
        fogOfWar = GameObject.Find("FogOfWar").GetComponent<Tilemap>();

        // For selecting/deselecting unit
        selectManager = GameObject.Find("SelectManager").GetComponent<SelectManager>();

        // Track this unit with the camera
        GameObject.Find("GameManager").GetComponent<GameManager>().unitTransform = transform;
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
        //if (movementInput.x == 0)
        //{
        //    hasMoved = false;
        //}
        //else if (movementInput.x != 0 && !hasMoved)
        //{
        //    hasMoved = true;

        //    GetMovementDirection();
        //}

    }

    public void GetMovementDirection()
    {
        if (movementInput.y == 0f)
        {
            direction = movementInput;
        } else if (movementInput.x == 0f)
        {
            direction = new Vector3();
        } else
        {
            int xs = Math.Sign(movementInput.x);
            int ys = Math.Sign(movementInput.y);
            direction = movementInput - new Vector3(xs * 0.2f, ys * 0.2f);
        }

        transform.position = map.GetCellCenterWorld(map.WorldToCell(transform.position + direction));
        UpdateFogOfWar();
    }

    public void OnMove(InputValue value)
    {
        movementInput = (Vector3) value.Get<Vector2>();
        GetMovementDirection();
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

    IEnumerator OnClick()
    {
        if (unit.selected)
        {
            if (!moving)
            {
                moving = true;
                AStar aStar = gameObject.AddComponent(typeof(AStar)) as AStar;
                List<Vector3Int> path = aStar.FindPath(selector.GetSelectedTile());
                Destroy(aStar);

                foreach (Vector3Int coord in path)
                {
                    yield return StartCoroutine(MoveToTile(coord));
                }
                unit.Deselect();
                moving = false;
            }
        }
    }


    IEnumerator MoveToTile(Vector3Int tile)
    {
        Vector3 startingPosition = transform.position;
        Vector3 destination = map.GetCellCenterWorld(tile);
        float elapsedTime = 0f;
        float totalTransitionTime = .5f;
        while (transform.position != destination)
        {
            transform.position = Vector3.Lerp(startingPosition, destination, elapsedTime / totalTransitionTime);
            UpdateFogOfWar();
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}
