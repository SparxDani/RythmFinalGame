using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
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
    public DoubleNode<T> Head => head;
}

