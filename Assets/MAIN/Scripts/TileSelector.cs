using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.InputSystem;

public class TileSelector : MonoBehaviour
{
    private Tilemap map;
    private Vector3Int prevTileCoord;
    private Color prevTileColor;
    // Start is called before the first frame update
    void Start()
    {
        map = GetComponent<Tilemap>();
        prevTileCoord = new Vector3Int(-9999, -9999, -9999);
    }

    // Update is called once per frame
    void Update()
    { 
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector3Int tileCoord = map.WorldToCell(mousePos);
        if (tileCoord != prevTileCoord)
        {
            prevTileColor = map.GetColor(tileCoord);
            map.SetTileFlags(tileCoord, TileFlags.None);
            map.SetColor(tileCoord, Color.green);
            if (prevTileCoord != new Vector3Int(-9999, -9999, -9999))
            {
                map.SetColor(prevTileCoord, prevTileColor);
            }
            prevTileCoord = tileCoord;
        }
    }
}
