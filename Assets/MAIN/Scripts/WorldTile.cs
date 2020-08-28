using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WorldTile
{
    public Vector3Int Coord { get; set; }
    public Vector3 WorldLocation { get; set; }
    public bool Occupied { get; set; }
}
