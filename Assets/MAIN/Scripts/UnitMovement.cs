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


    void Awake()
    {
        moving = false;

        map = GameObject.Find("BaseTilemap").GetComponent<Tilemap>();
        selector = GameObject.Find("BaseTilemap").GetComponent<TileSelector>();
        fogOfWar = GameObject.Find("FogOfWar").GetComponent<Tilemap>();
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
            GameTiles.instance.tiles[transform.position].Occupied = false;
            GetMovementDirection();
        }
    }

    public void GetMovementDirection()
    {
        int xs = Math.Sign(movementInput.x);
        int ys = Math.Sign(movementInput.y);
        if (movementInput.y == 0f)
        {
            direction = new Vector3(xs * 3, 0, 0);
        } else
        {
            direction = new Vector3(xs * 1.5f, ys * 1.5f, 0);
        }

        Vector3 futurePosition = map.GetCellCenterWorld(map.WorldToCell(transform.position + direction));
        WorldTile tile;
        if (GameTiles.instance.tiles.TryGetValue(futurePosition, out tile))
        {
            transform.position = futurePosition;
            tile.Occupied = true;
            GameTiles.instance.UpdateFogOfWar(transform.position);
        }
    }

    public void OnMove(InputValue value)
    {
        if (owner.GetSelectedUnit() == unit)
        {
            movementInput = (Vector3) value.Get<Vector2>();
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        transform.position -= direction;
    }


    IEnumerator OnClick()
    {
        Vector3Int destination = selector.GetSelectedTile();
        //Debug.Log(destination);
        // Check if unit we want to move is selected and if the tile we want to move to exists and isn't occupied.
        if (owner.GetSelectedUnit() == unit && GameTiles.instance.tiles.ContainsKey(map.GetCellCenterWorld(destination)) && GameTiles.instance.tiles[map.GetCellCenterWorld(destination)].Occupied == false)
        {
            if (!moving)
            {
                GameTiles.instance.tiles[transform.position].Occupied = false;
                moving = true;
                AStar aStar = gameObject.AddComponent(typeof(AStar)) as AStar;
                List<Vector3Int> path = aStar.FindPath(destination);
                Destroy(aStar);

                foreach (Vector3Int coord in path)
                {
                    yield return StartCoroutine(MoveToTile(coord));
                }
                unit.Deselect();
                moving = false;
                GameTiles.instance.tiles[transform.position].Occupied = true;
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
            GameTiles.instance.UpdateFogOfWar(transform.position);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}
