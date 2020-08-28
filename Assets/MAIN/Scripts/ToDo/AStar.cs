using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
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

        PriorityQueue<(Vector3Int, List<Vector3Int>)> fringe = new PriorityQueue<(Vector3Int, List<Vector3Int>)>(PriorityFunction);
        HashSet<Vector3Int> closed = new HashSet<Vector3Int>();

        fringe.Push((map.WorldToCell(transform.position), new List<Vector3Int>()));

        while (true)
        {
            if (fringe.IsEmpty()) { break; }
            ((Vector3Int, List<Vector3Int>), float) elem = fringe.Pop();
            Vector3Int currTile = elem.Item1.Item1;
            List<Vector3Int> pathToCurrTile = elem.Item1.Item2;
            if (currTile == goal)
            {
                return new List<Vector3Int>(pathToCurrTile) { currTile };
            }
            if (!closed.Contains(elem.Item1.Item1))
            {
                closed.Add(elem.Item1.Item1);
                List<Vector3Int> neighbors = GetNeighbors(currTile);
                foreach (Vector3Int n in neighbors)
                {
                    fringe.Push((n, new List<Vector3Int>(pathToCurrTile) { currTile }));
                }
            }
        }
        UnityEngine.Debug.Log("Fringe is empty.");
        return new List<Vector3Int>();
    }


    private List<Vector3Int> GetNeighbors(Vector3Int tile)
    {
        List<Vector3Int> neighbors = new List<Vector3Int>();
        if (tile.y % 2 == 0) // even tile row;
        {
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if ((i == 1 && j == 1) || (i == 1 && j == -1) || (i == 0 && j == 0)) { continue; }
                    neighbors.Add(new Vector3Int(tile.x + i, tile.y + j, tile.z));
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
                    neighbors.Add(new Vector3Int(tile.x + i, tile.y + j, tile.z));
                }
            }
        }
        return neighbors;
    }

    private float PriorityFunction((Vector3Int, List<Vector3Int>) node)
    {
        return node.Item2.Count + ManhattanDistance(node.Item1, goal);
    }
    
    private float ManhattanDistance(Vector3Int s, Vector3Int d)
    {
        return Math.Abs(s.x - d.x) + Math.Abs(s.y - d.y) + Math.Abs(s.z - d.z);
    }

}

