using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.InputSystem;

public class TileSelector : MonoBehaviour
{
    private Tilemap map;
    private Vector3Int currTileCoord;
    private Vector3Int prevTileCoord;
    private Color prevTileColor;

    public Color DefaultColor { get; set; } = new Color(1f, 1f, 1f, 1f);


    // Start is called before the first frame update
    void Start()
    {
        map = GetComponent<Tilemap>();
    }

    // Update is called once per frame
    void Update()
    { 
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        currTileCoord = map.WorldToCell(mousePos);
        if (currTileCoord != prevTileCoord && prevTileCoord != null)
        {
            WorldTile prevWorldTile;
            if (GameTiles.instance.tiles.TryGetValue(map.GetCellCenterWorld(prevTileCoord), out prevWorldTile)) // don't select outside the map.
            {
                if (prevWorldTile.Owner)
                {
                    prevTileColor = prevWorldTile.Owner.PlayerColor;
                }
                else
                {
                    prevTileColor = DefaultColor;
                }
                map.SetColor(currTileCoord, Color.green);
                map.SetColor(prevTileCoord, prevTileColor);
            }
            
        }
        prevTileCoord = currTileCoord;
    }

    public Vector3Int GetSelectedTile()
    {
        return currTileCoord;
    }
}
