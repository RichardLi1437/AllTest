using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithm
{
    class Sort
    {
        public static void QuickSort(double[] data, int start, int end)
        {
            if (start >= end) return;
            double pivot = data[(start + end) / 2];
            int left = start;
            int right = end;
            double swap;
            while (left < right)
            {
                while ( left <= end && data[left] < pivot) left++;
                while (right >= start && data[right] > pivot) right--;
                if (left >= right) break;
                swap = data[left];
                data[left] = data[right];
                data[right] = swap;
                left++;
                right--;
            }

            if (left == right) { right--; left++; }
            QuickSort(data, start, right);
            QuickSort(data, left, end);
        }

        public static void ShowData(double[] data)
        {
            for (int i = 0; i < data.Length; i++)
            {
                System.Console.Write(data[i] + " ");
            }
            System.Console.WriteLine();
        }
    }
}
