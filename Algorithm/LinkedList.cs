using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithm
{
    class Node
    {
        public int value;
        public Node next;

        public Node(int value)
        {
            this.value = value;
            next = null;
        }
    }

    class LinkedList
    {
        private Node head, tail;

        public Node Head 
        {
            get { return head; }
        }

        public Node Tail
        {
            get { return tail; }
        }

        public LinkedList()
        {
            head = tail = null;
        }

        public LinkedList(int[] values)
        {
            head = tail = null;
            if (values.Length > 0)
            {
                head = new Node(values[0]);
                tail = head;
            }
            for (int i = 1; i < values.Length; i++)
            {
                tail.next = new Node(values[i]);
                tail = tail.next;
            }
        }

        public void Reverse()
        {
            Node pPre = null;
            Node p = head;
            Node pNext;
            tail = head;
            while (p != null)
            {
                pNext = p.next;
                p.next = pPre;
                pPre = p;
                p = pNext;
            }
            head = pPre;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("head->");
            if (head == null)
            {
                sb.Append("null\t");
            }
            else
            {
                sb.Append(head.value);
                sb.Append('\t');
            }
            Node p = head;

            while (p != null)
            {
                sb.Append(p.value);
                sb.Append("->");
                p = p.next;
            }
            sb.Append("null\t");
            if (tail != null)
            {
                sb.Append(tail.value);
                sb.Append("<-tail");
            }
            return sb.ToString();
        }

    }
}
