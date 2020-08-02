using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.InputSystem;



/**
Handles logic related to unit movement
**/


public class UnitMovement : MonoBehaviour
{
    private PlayerInfo playerInfo;

    private Vector2 movementInput;
    private Vector3 direction;

    public Tilemap fogOfWar;

    bool hasMoved;

    public int vision = 1;


    void Awake()
    {
        fogOfWar = GameObject.Find("FogOfWar").GetComponent<Tilemap>();

        // Track this unit with the camera
        GameObject.Find("GameManager").GetComponent<GameManager>().playerTransform = transform;

        //int id = gameObject.GetComponent<BasicUnit>();


    }

    // Start is called before the first frame update
    void Start()
    {

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

        // click-to-move

    }


    public void GetMovementDirection()
    {
        if (movementInput.x < 0)
        {
            if (movementInput.y > 0)
            {
                direction = new Vector3(-0.5f, 0.5f);
            }
            else if (movementInput.y < 0)
            {
                direction = new Vector3(-0.5f, -0.5f);
            }
            else
            {
                direction = new Vector3(-1, 0, 0);
            }
            transform.position += direction;
            UpdateFogOfWar();
        }
        else if (movementInput.x > 0)
        {
            if (movementInput.y > 0)
            {
                direction = new Vector3(0.5f, 0.5f);
            }
            else if (movementInput.y < 0)
            {
                direction = new Vector3(0.5f, -0.5f);
            }
            else
            {
                direction = new Vector3(1, 0, 0);
            }

            transform.position += direction;
            UpdateFogOfWar();
        }
    }

    public void OnMove(InputValue value)
    {
        movementInput = value.Get<Vector2>();
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

    // point-and-click movement. rename.
    private void ClickToMove(Vector3Int dest)
    {
        AStar aStar = gameObject.AddComponent(typeof(AStar)) as AStar;
        List<Vector3Int> path = aStar.FindPath(dest);

        // do stuff
        print(path);

        Destroy(aStar);
    }
}
