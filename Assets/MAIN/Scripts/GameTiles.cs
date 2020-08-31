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
                Occupied = false,
                Neighbors = new List<WorldTile>()
            };
            tiles.Add(tile.WorldLocation, tile);
        }

        // Iterate through again to set neighbors.
        foreach (var tile in tiles)
        {
            if (tile.Value.Coord.y % 2 == 0) // even tile row;
            {
                for (int i = -1; i <= 1; i++)
                {
                    for (int j = -1; j <= 1; j++)
                    {
                        if ((i == 1 && j == 1) || (i == 1 && j == -1) || (i == 0 && j == 0)) { continue; }

                        Vector3Int neighborTile = new Vector3Int(tile.Value.Coord.x + i, tile.Value.Coord.y + j, tile.Value.Coord.z);
                        WorldTile _tile;
                        Vector3 neighborTileCenter = Tilemap.GetCellCenterWorld(neighborTile);
                        if (tiles.TryGetValue(neighborTileCenter, out _tile))
                        {
                            tile.Value.Neighbors.Add(tiles[neighborTileCenter]);
                        }
                    }
                }
            }
            else // odd tile row
            {
                for (int i = -1; i <= 1; i++)
                {
                    for (int j = -1; j <= 1; j++)
                    {
                        if ((i == -1 && j == -1) || (i == -1 && j == 1) || (i == 0 && j == 0)) { continue; }

                        Vector3Int neighborTile = new Vector3Int(tile.Value.Coord.x + i, tile.Value.Coord.y + j, tile.Value.Coord.z);
                        WorldTile _tile;
                        Vector3 neighborTileCenter = Tilemap.GetCellCenterWorld(neighborTile);
                        if (tiles.TryGetValue(neighborTileCenter, out _tile))
                        {
                            tile.Value.Neighbors.Add(tiles[neighborTileCenter]);
                        }
                    }
                }
            }
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
