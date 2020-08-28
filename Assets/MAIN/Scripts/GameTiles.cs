using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameTiles : MonoBehaviour
{
    public static GameTiles instance;
    public Tilemap Tilemap;
    public Tilemap FogOfWar;
    int vision = 1;

    public Dictionary<Vector3, WorldTile> tiles;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        GetWorldTiles();
    }

    private void GetWorldTiles()
    {
        tiles = new Dictionary<Vector3, WorldTile>();
        foreach (Vector3Int coord in Tilemap.cellBounds.allPositionsWithin)
        {
            if (!Tilemap.HasTile(coord)) { continue; }

            WorldTile tile = new WorldTile
            {
                Coord = coord,
                WorldLocation = Tilemap.GetCellCenterWorld(coord),
                Occupied = false
            };

            tiles.Add(tile.WorldLocation, tile);
        }
        //now loop through each WorldTile again to get neighbors.
        foreach (var tile in tiles)
        {
            //Debug.Log(tile.Key);
            Collider2D[] neighbors = Physics2D.OverlapCircleAll((Vector2)tile.Key, 3.0f);
            //Debug.Log(neighbors[0].gameObject.transform.position);
        }

    }

    public void UpdateFogOfWar(Vector3 position)
    {
        Vector3Int currentTile = FogOfWar.WorldToCell(position);

        //Clear the surrounding tiles
        for (int x = -vision; x <= vision; x++)
        {
            for (int y = -vision; y <= vision; y++)
            {
                FogOfWar.SetTile(currentTile + new Vector3Int(x, y, 0), null);
            }

        }
    }
}
