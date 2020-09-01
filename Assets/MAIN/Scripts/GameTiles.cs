using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameTiles : MonoBehaviour
{
    public static GameTiles instance;
    public Tilemap Map;
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
        foreach (Vector3Int coord in Map.cellBounds.allPositionsWithin)
        {
            if (!Map.HasTile(coord)) { continue; }
            WorldTile tile = new WorldTile
            {
                Coord = coord,
                WorldLocation = Map.GetCellCenterWorld(coord),
                Occupied = false,
                Neighbors = new List<WorldTile>(),
                Owner = null
            };
            tiles.Add(tile.WorldLocation, tile);

            // Set tile flags to None so color can be set.
            Map.SetTileFlags(tile.Coord, TileFlags.None);
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
                        Vector3 neighborTileCenter = Map.GetCellCenterWorld(neighborTile);
                        WorldTile _tile;
                        if (tiles.TryGetValue(neighborTileCenter, out _tile))
                        {
                            tile.Value.Neighbors.Add(_tile);
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
                        Vector3 neighborTileCenter = Map.GetCellCenterWorld(neighborTile);
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
