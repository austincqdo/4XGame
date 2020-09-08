using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class AStar : MonoBehaviour
{
    private Vector3Int goal;
    private Tilemap map;
    private List<Vector3Int> path;

    void Awake()
    {
        map = GameObject.Find("BaseTilemap").GetComponent<Tilemap>();
        path = new List<Vector3Int>();
    }

    public List<Vector3Int> FindPath(Vector3Int goal)
    {
        this.goal = goal;

        PriorityQueue<(WorldTile, List<Vector3Int>)> fringe = new PriorityQueue<(WorldTile, List<Vector3Int>)>(PriorityFunction);
        HashSet<WorldTile> closed = new HashSet<WorldTile>();

        fringe.Push((GameTiles.instance.tiles[transform.position], new List<Vector3Int>()));

        while (true)
        {
            if (fringe.IsEmpty()) { break; }
            ((WorldTile, List<Vector3Int>), float) elem = fringe.Pop();
            WorldTile currTile = elem.Item1.Item1;
            List<Vector3Int> pathToCurrTile = elem.Item1.Item2;
            if (currTile.Coord == goal)
            {
                return new List<Vector3Int>(pathToCurrTile) { currTile.Coord };
            }
            if (!closed.Contains(elem.Item1.Item1))
            {
                closed.Add(elem.Item1.Item1);
                List<WorldTile> neighbors = currTile.Neighbors;
                foreach (WorldTile n in neighbors)
                {
                    fringe.Push((n, new List<Vector3Int>(pathToCurrTile) { currTile.Coord }));
                }
            }
        }
        UnityEngine.Debug.Log("Fringe is empty.");
        return new List<Vector3Int>();
    }

    private float PriorityFunction((WorldTile, List<Vector3Int>) node)
    {
        return node.Item2.Count + ManhattanDistance(node.Item1.Coord, goal);
    }
    
    private float ManhattanDistance(Vector3Int s, Vector3Int d)
    {
        return Math.Abs(s.x - d.x) + Math.Abs(s.y - d.y) + Math.Abs(s.z - d.z);
    }

}

