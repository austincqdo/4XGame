using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// priority queue that weighs by function parameter
public class PriorityQueue<T>
{

    private List<(T, float)> elemList;
    private Func<T, float> priorityFunction;

    public PriorityQueue(Func<T, float> priorityFunction)
    {
        this.elemList = new List<(T, float)>();
        this.priorityFunction = priorityFunction;
    }

    public void Push(T item)
    {
        (T, float) entry = (item, priorityFunction(item));
        elemList.Insert(0, entry);
        if (this.Count() > 1)
        {
            // Sort item by priority
            int i = 0;
            while (elemList[i].Item2 < elemList[i + 1].Item2)
            {
                (T, float) tempElement = elemList[i + 1];
                elemList[i + 1] = entry;
                elemList[i] = tempElement;
                i++;
                if (i == this.Count() - 1) { return; }
            }
        }
    }

    public (T item, float cost) Pop()
    {
        (T, float) element = elemList[0];
        elemList.RemoveAt(0);
        return element;
    }

    public int Count()
    {
        return elemList.Count;
    }

    public bool IsEmpty()
    {
        return elemList.Count == 0;
    }

}
