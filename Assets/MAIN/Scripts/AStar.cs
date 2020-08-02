using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class AStar : MonoBehaviour
{
    private Vector3Int goal;
    private Tilemap map;

    void Start()
    {
        map = GameObject.Find("BaseTilemap").GetComponent<Tilemap>();
    }

    public List<Vector3Int> FindPath(Vector3Int goal)
    {
        this.goal = goal;

        PriorityQueue<(Vector3Int, List<Vector3Int>)> fringe = new PriorityQueue<(Vector3Int, List<Vector3Int>)>(PriorityFunction);
        HashSet<(Vector3Int, List<Vector3Int>)> closed = new HashSet<(Vector3Int, List<Vector3Int>)>();

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
            if (!closed.Contains(elem.Item1))
            {
                closed.Add(elem.Item1);
                int z = currTile.z;
                for (int i = -1; i <= 1; i++)
                {
                    for (int j = -1; j <= 1; j++)
                    {
                        // only add tiles adjacent to parent tile.
                        if ((i == j * -1) || (new Vector3Int(i, j, z) == currTile)) { continue; }
                        fringe.Push((new Vector3Int(i, j, z), new List<Vector3Int>(pathToCurrTile){ currTile }));
                    }
                }
            }
        }
        return new List<Vector3Int>();
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

