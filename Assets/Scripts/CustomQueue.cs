using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomQueue<T>
{
    private CircularDoublyLinkedList<T> elements = new CircularDoublyLinkedList<T>();
    private int count = 0;

    public void Enqueue(T item)
    {
        elements.Add(item);
        count++;
    }

    public T Dequeue()
    {
        if (count == 0)
        {
            throw new InvalidOperationException("The queue is empty.");

        }

        T value = elements.Get(0);
        elements.RemoveAt(0);
        count--;
        return value;
    }

    public T Peek()
    {
        if (count == 0)
        {
            throw new InvalidOperationException("The queue is empty.");
        }

        return elements.Get(0);
    }

    public int Count
    {
        get { return count; }
    }
}
