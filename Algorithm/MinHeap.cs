using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Algorithm
{
    public class MinHeap
    {
        int[] data;
        int currSize;
        int maxSize;

        public MinHeap(int maxSize) 
        {
            data = new int[maxSize + 1];
            data[maxSize] = int.MaxValue;
            this.maxSize = maxSize;
            currSize = 0;
        }

        public MinHeap(int[] input)
        {
            data = new int[input.Length + 1];
            data[input.Length] = int.MaxValue;
            Array.Copy(input, data, input.Length);
            maxSize = currSize = input.Length;
            Initialize();
        }

        private void Initialize()
        {
            int idx = (currSize - 2) / 2;
            int minIdx, swap;
            for (int i = idx; i >= 0; i--)
            {
                int j = i;                
                minIdx = data[2 * j + 1] < data[2 * j + 2] ? 2 * j + 1 : 2 * j + 2;
                while (data[j] > data[minIdx]) 
                {
                    swap = data[j]; data[j] = data[minIdx]; data[minIdx] = swap;
                    j = minIdx;
                    if (j <= idx)
                    {
                        minIdx = data[2 * j + 1] < data[2 * j + 2] ? 2 * j + 1 : 2 * j + 2;
                    }
                    else
                    {
                        minIdx = maxSize;
                    }
                }
            }
        }

        public bool Check()
        {
            for (int i = 0; i <= (currSize - 2) / 2; i++)
            {
                if (data[i] > data[2 * i + 1] || data[i] > data[2 * i + 2]) return false;
            }
            return true;
        }

        override public String ToString()
        {
            if (IsEmpty) return "{}";
            StringBuilder sb = new StringBuilder();
            sb.Append('{');
            sb.Append(data[0]);
            for (int i = 1; i < currSize; i++)
            {
                sb.Append(", ");
                sb.Append(data[i]);
            }
            sb.Append('}');
            return sb.ToString();
        }

        public int DeHeap()
        {
            if (IsEmpty) throw new Exception("Empty heap!");
            int min = data[0];
            data[0] = data[--currSize];
            int idx = (currSize - 2) / 2;
            int j = 0, swap;
            int minIdx;
            if (currSize > 1)
            {
                minIdx = data[2 * j + 1] < data[2 * j + 2] ? 2 * j + 1 : 2 * j + 2;
            }
            else
            {
                minIdx = maxSize;
            }
            while (data[j] > data[minIdx])
            {
                swap = data[j]; data[j] = data[minIdx]; data[minIdx] = swap;
                j = minIdx;
                if (j <= idx)
                {
                    minIdx = data[2 * j + 1] < data[2 * j + 2] ? 2 * j + 1 : 2 * j + 2;
                }
                else
                {
                    minIdx = maxSize;
                }
            }
            return min;
        }

        public void EnHeap(int n)
        {
            if (IsFull) throw new Exception("Full heap!");
            data[currSize++] = n;
            int i = currSize - 1, swap;
            while (i > 0 && data[i] < data[(i - 1) / 2])
            {
                swap = data[i]; data[i] = data[(i - 1) / 2]; data[(i - 1) / 2] = swap;
                i = (i - 1) / 2;
            }
        }

        public bool IsFull 
        {
            get { return currSize == maxSize; }
        }

        public bool IsEmpty
        {
            get { return currSize == 0; }
        }
    }
}
