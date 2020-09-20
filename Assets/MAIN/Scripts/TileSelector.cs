using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.InputSystem;

public class TileSelector : MonoBehaviour
{
    private Tilemap map;
    private Vector3Int currTileCoord; 
    private Vector3Int? prevTileCoord; // Nullable values allows us to set default values to null.
    private Color prevTileColor;

    public Color DefaultColor { get; set; } = new Color(1f, 1f, 1f, 1f);

    void Awake()
    {
        prevTileCoord = null;
    }

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

        // Reset color of previous tile.
        if (prevTileCoord.HasValue && currTileCoord != prevTileCoord.Value)
        {
            WorldTile prevWorldTile;
            if (GameTiles.instance.tiles.TryGetValue(map.GetCellCenterWorld(prevTileCoord.Value), out prevWorldTile)) // don't select outside the map.
            {
                if (prevWorldTile.Owner)
                {
                    prevTileColor = prevWorldTile.Owner.PlayerColor;
                }
                else
                {
                    prevTileColor = DefaultColor;
                }
                map.SetColor(prevTileCoord.Value, prevTileColor);
            }
        }

        // Highlight current tile if it exists.
        if (GameTiles.instance.tiles.ContainsKey(map.GetCellCenterWorld(currTileCoord)))
        {
            map.SetColor(currTileCoord, Color.green);
            prevTileCoord = currTileCoord;
        }
        else
        {
            prevTileCoord = null;
        }
    }

    public Vector3Int? GetSelectedTile()
    {
        Vector3Int? tileCoord;
        if (GameTiles.instance.tiles.ContainsKey(map.GetCellCenterWorld(currTileCoord)))
        {
            tileCoord = currTileCoord;
        }
        else
        {
            tileCoord = null;
        }
        return tileCoord;
    }
}
