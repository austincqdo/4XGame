using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PriorityQueue
{
    private List<(float p, Vector3Int i)> itemList;
    private Func<Vector3Int, float> priorityFunction;

    public PriorityQueue(Func<Vector3Int, float> priorityFunction)
    {
        this.itemList = new List<(float p, Vector3Int)>();
        this.priorityFunction = priorityFunction;
    }

    public void Push(Vector3Int item)
    {
        (float p, Vector3Int i) entry = (this.priorityFunction(item), item);
        itemList.Insert(0, entry);
        if (this.Count() > 1)
        {
            // Sort item by priority
            int i = 0;
            while (itemList[i].p < itemList[i + 1].p)
            {
                (float p, Vector3Int i) tempItem = itemList[i + 1];
                itemList[i + 1] = entry;
                itemList[i] = tempItem;
                i++;
                if (i == this.Count() - 1) { return; }
            }
        }
    }

    public (float p, Vector3Int i) Pop()
    {
        (float p, Vector3Int i) entry = itemList[0];
        itemList.RemoveAt(0);
        return entry;
    }

    public int Count()
    {
        return itemList.Count;
    }

}
