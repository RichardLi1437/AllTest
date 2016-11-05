using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThreadSafeQueue
{
    public class TwoLockQueue<T>
    {
        public TwoLockQueue()
        {
            Head = new Node<T>();
            Tail = Head;
        }
        
        public Node<T> Head { get; private set; }

        public Node<T> Tail { get; private set; }

        public void Enqueue(T value)
        {
            Node<T> node = new Node<T>(value);
            lock (Tail)
            {
                Tail.Next = node;
                Tail = node;
            }
        }

        public bool Dequeue(out T value)
        {
            Node<T> newHead;            
            lock (Head)
            {
                newHead = Head.Next;
                if (newHead != null)
                {
                    Head = newHead;
                }
            }
            if (newHead == null)
            {
                value = default(T);
                return false;
            }
            else
            {
                value = newHead.Value;
                return true;
            }
        }
    }

    public class Node<T>
    {
        public Node(T value)
        {
            Value = value;
            Next = null;
        }

        public Node() : this(default(T))
        {
        }

        public T Value { get; set; }

        public Node<T> Next { get; set; }
    }
}
