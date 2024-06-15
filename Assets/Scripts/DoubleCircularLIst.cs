using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleNode<T>
{
    public T Data { get; set; }
    public DoubleNode<T> Next { get; set; }
    public DoubleNode<T> Previous { get; set; }

    public DoubleNode(T data)
    {
        Data = data;
        Next = this;
        Previous = this;
    }
}

public class CircularDoublyLinkedList<T>
{
    private DoubleNode<T> head;

    public void Add(T data)
    {
        DoubleNode<T> newNode = new DoubleNode<T>(data);

        if (head == null)
        {
            head = newNode;
        }
        else
        {
            DoubleNode<T> tail = head.Previous;

            tail.Next = newNode;
            newNode.Previous = tail;
            newNode.Next = head;
            head.Previous = newNode;
        }
    }

    public void Insert(int index, T data)
    {
        if (index < 0) return;

        DoubleNode<T> newNode = new DoubleNode<T>(data);
        if (head == null)
        {
            head = newNode;
            return;
        }

        DoubleNode<T> current = head;
        for (int i = 0; i < index; i++)
        {
            current = current.Next;
        }

        DoubleNode<T> previousNode = current.Previous;

        previousNode.Next = newNode;
        newNode.Previous = previousNode;
        newNode.Next = current;
        current.Previous = newNode;

        if (index == 0)
        {
            head = newNode;
        }
    }

    public void RemoveAt(int index)
    {
        if (head == null || index < 0) return;

        DoubleNode<T> current = head;

        for (int i = 0; i < index; i++)
        {
            current = current.Next;
        }

        DoubleNode<T> previousNode = current.Previous;
        DoubleNode<T> nextNode = current.Next;

        previousNode.Next = nextNode;
        nextNode.Previous = previousNode;

        if (index == 0)
        {
            head = nextNode;
        }
    }

    public T Get(int index)
    {
        if (head == null || index < 0) return default(T);

        DoubleNode<T> current = head;

        for (int i = 0; i < index; i++)
        {
            current = current.Next;
        }

        return current.Data;
    }

    public int Count
    {
        get
        {
            if (head == null) return 0;

            int count = 1;
            DoubleNode<T> current = head;

            while (current.Next != head)
            {
                count++;
                current = current.Next;
            }

            return count;
        }
    }

    public DoubleNode<T> Head => head;
}
