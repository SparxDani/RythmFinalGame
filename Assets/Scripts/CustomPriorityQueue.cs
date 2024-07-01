using System;
using System.Collections.Generic;
using UnityEngine;

namespace PriorityQueue
{
    class CustomPriorityQueue
    {
        class Node
        {
            public int FinalScore { get; set; }
            public int Priority { get; set; }

            public Node(int finalScore, int priority)
            {
                FinalScore = finalScore;
                Priority = priority;
            }
        }

        private const int MaxCount = 5;
        private CircularDoublyLinkedList<Node> nodes;

        public CustomPriorityQueue()
        {
            nodes = new CircularDoublyLinkedList<Node>();
        }

        public void PriorityEnqueue(int finalScore)
        {
            Node newNode = new Node(finalScore, finalScore);

            if (nodes.IsEmpty)
            {
                nodes.Add(newNode);
            }
            else
            {
                bool inserted = false;

                for (int i = 0; i < nodes.Count; i++)
                {
                    if (newNode.Priority > nodes.Get(i).Priority)
                    {
                        nodes.Insert(i, newNode);
                        inserted = true;
                        break;
                    }
                }

                if (!inserted)
                {
                    nodes.Add(newNode);
                }

                if (nodes.Count > MaxCount)
                {
                    nodes.RemoveAt(MaxCount);
                }
            }
        }

        public int[] GetTop5Scores()
        {
            int count = Math.Min(MaxCount, nodes.Count);
            int[] top5Scores = new int[count];
            for (int i = 0; i < count; i++)
            {
                top5Scores[i] = nodes.Get(i).FinalScore;
            }
            return top5Scores;
        }
    }
}
