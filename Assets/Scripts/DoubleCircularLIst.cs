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
    private int count = 0;

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
        count++;
    }

    public void Insert(int index, T data)
    {
        if (index < 0 || index > count) return;

        DoubleNode<T> newNode = new DoubleNode<T>(data);
        if (head == null)
        {
            head = newNode;
        }
        else
        {
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
        count++;
    }

    public void RemoveAt(int index)
    {
        if (head == null || index < 0 || index >= count) return;

        DoubleNode<T> current = head;

        for (int i = 0; i < index; i++)
        {
            current = current.Next;
        }

        DoubleNode<T> previousNode = current.Previous;
        DoubleNode<T> nextNode = current.Next;

        if (current == head)
        {
            if (head.Next == head)
            {
                head = null;
            }
            else
            {
                head = nextNode;
            }
        }

        previousNode.Next = nextNode;
        nextNode.Previous = previousNode;

        count--;
    }

    public T Get(int index)
    {
        if (head == null || index < 0 || index >= count) return default(T);

        DoubleNode<T> current = head;

        for (int i = 0; i < index; i++)
        {
            current = current.Next;
        }

        return current.Data;
    }

    public int Count
    {
        get { return count; }
    }

    public bool IsEmpty
    {
        get { return count == 0; }
    }
}
