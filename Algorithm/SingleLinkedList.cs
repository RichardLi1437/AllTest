using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithm
{
    class SingleLinkedList
    {
        public SingleLinkedListNode Head { get; set; }

        public SingleLinkedList(int[] ary)
        {
            if (ary.Length == 0)
            {
                Head = null; return;
            }

            SingleLinkedListNode node = new SingleLinkedListNode(ary[0]);
            Head = node;
            SingleLinkedListNode last = Head;
            for (int i = 1; i < ary.Length; i++)
            {
                node = new SingleLinkedListNode(ary[i]);
                last.Next = node;
                last = node;
            }
        }

        public void Reverse()
        {
            if (Head == null) return;
            SingleLinkedListNode p, q, r;
            p = null; q = Head;
            while (q != null)
            {
                r = q.Next;
                q.Next = p;
                p = q;
                q = r;
            }
            Head = p;
        }

        public SingleLinkedList MergeSortedLinkedList(SingleLinkedList other)
        {
            SingleLinkedListNode head = new SingleLinkedListNode(-1);
            SingleLinkedListNode curr = head;
            SingleLinkedListNode la = this.Head, lb = other.Head;

            while (la != null && lb != null)
            {
                if (la.Data <= lb.Data)
                {
                    curr.Next = la;
                    la = la.Next;
                }
                else
                {
                    curr.Next = lb;
                    lb = lb.Next;
                }
                curr = curr.Next;
            }

            curr.Next = la == null ? lb : la;

            Head = head.Next;

            return this;
        }

        public void ShowList()
        {
            SingleLinkedListNode node = Head;
            while (node != null)
            {
                System.Console.Write(node.Data + " ");
                node = node.Next;
            }
            System.Console.WriteLine();
        }
    }

    class SingleLinkedListNode
    {
        public SingleLinkedListNode(int d)
        {
            Data = d;
            Next = null;
        }
        public int Data { get; set; }
        public SingleLinkedListNode Next { get; set; }
    }
}
