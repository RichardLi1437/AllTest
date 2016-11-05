using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Reflection;
using System.IO;

namespace Algorithm
{
    public struct Pos
    {
        public int Row, Column;
        public Pos(int r, int c)
        {
            Row = r;
            Column = c;
        }
    }
    public class Program
    {
        static void Main(string[] args)
        {
            uint[] bAry = {3, 8, 5, 2, 5, 4, 11, 100};
            uint[] cAry = { 7, 13, 6, 3, 2, 2, 9, 15};
            for (int i = 0; i < bAry.Length; i++) 
            {
                uint b = bAry[i];
                uint c = cAry[i];
                Console.WriteLine("{0}/{1}={2}:{3}", b, c, Circle(b, c), b*1.0 / c);

            }
            //TestMergeList();
            //TestJudgeInterleave();
            //int m = 95, n = 10;
            //int[,] result = SelectMfromN(m,n);
            /*for (int j = 1; j <= 5; j++)
            {
                for (int i = 0; i <= 5; i++)
                {
                    System.Console.Write("f({0}, {1}) = {2}\t", i, j, result[i, j]);
                }
                System.Console.WriteLine();
            }*/
            //System.Console.WriteLine(result[95,9]);
            //System.Console.WriteLine(Power(1.1, 10));
            //AssemblyVersion();
            //TestGenerateMatrix();
            //FindNumber();
            //TestBinaryTree();
            //TestHeap();
            //Maze();
            //double[] data = { 6, 6, 6, 6, 6};
            //Sort.QuickSort(data, 0, 4);
            //Sort.ShowData(data);
            //int[] data = {};
            //SingleLinkedList list = new SingleLinkedList(data);
            //list.ShowList();
            //list.Reverse();
            //list.ShowList();
        }

        class Interleave
        {
            private string[] ss;
            private string s;
            private int[] idxx;
            private int[] solution;
            public Interleave(string[] ss, string s) {
                this.ss = ss;
                this.s = s;
                idxx = new int[ss.Length];
                for (int i = 0; i < idxx.Length; i++) idxx[i] = 0;
                solution = new int[s.Length];
            }

            public void Judge()
            {
                Judge(0);
            }

            private void Judge(int idx)
            {
                if (idx == s.Length)
                {
                    //mark the current state is a solution
                    for (int i = 0; i < solution.Length; i++)
                    {
                        Console.Write("{0}\t", solution[i]);
                    }
                    Console.WriteLine();
                    return;
                }

                for (int i = 0; i < idxx.Length; i++)
                {
                    // try get char from s[i]
                    int index = ss[i].IndexOf(s[idx], idxx[i]);
                    if (index >= 0)
                    {
                        solution[idx] = i;
                        int oldIdxxi = idxx[i];
                        idxx[i] = index + 1;
                        Judge(idx + 1);
                        idxx[i] = oldIdxxi;
                    }
                }
            }
        }

        static void TestJudgeInterleave()
        {
            string s0 = "1357", s1 = "02468", s2 = "6892360", s = "1234563";
            Interleave interleave = new Interleave(new string[3] {s0, s1, s2}, s);
            Console.WriteLine("{0}\t{1}\t{2}", s0, s1, s);
            interleave.Judge();
        }

        static int[,] SelectMfromN(int m, int n)
        {
            int[,] result = new int[m+1, n+1];
            for (int i = 1; i <= n; i++)
            {
                result[0,i] = 1;
            }
            for (int i = 0; i <= m; i++)
            {
                result[i, 1] = 1;
            }
            for (int i = 1; i <= m; i++)
            {
                for (int j = 2; j <= n; j++)
                {
                    result[i, j] = result[i - 1, j] + result[i, j - 1];
                }
            }
            return result;
        }

        public static double Power(double x, int n)
        {
            double b = x;
            double result = 1;
            while (n > 0) 
            {
	            if (n % 2 == 1) 
                {
		            result *= b;
	            }
	            n = n/2;
	            b = b * b;
            }
            return result;
        }

        public static void AssemblyVersion()
        {
            string[] files = Directory.GetFiles(@"D:\Projects\Migration\INSZoom\6.0Patch\INSZoom Forms Upgrade V4.02\Package\INSZoom\Bin", "*.DLL");
            foreach (string file in files)
            {
                Assembly a = Assembly.ReflectionOnlyLoadFrom(file);
                string s = a.ImageRuntimeVersion;
                System.Console.WriteLine(s);
            }
        }

        public static int Reverse(int n)
        {
            int sign = n >= 0 ? 1 : -1;
            n = n >= 0 ? n : -n;
            int m = ReverseUnsign(n);
            return m * sign;
        }

        private static int ReverseUnsign(int n)
        {
            if (n < 0) throw new Exception("n should be an positive integer.");
            int m = 0, d;
            while (n > 0)
            {
                d = n % 10;
                n = n / 10;
                m = m * 10 + d;
            }
            return m;
        }

        public static void TestGenerateMatrix()
        {
            for (int i = 1; i < 10; i++)
            {
                GenerateMatrix(i);
            }
        }

        public static void GenerateMatrix(int n)
        {
            int[,] delta = {{1,0}, {0,1}, {-1,0}, {0,-1}};
            int[] border = { n, n, -1, -1};
            int[,] a = new int[n, n];
            int[] pos = {0,0};
            int dir = 0;
            for (int i = 1; i <= n * n; i++)
            {
                a[pos[0], pos[1]] = i;
                pos[0] += delta[dir,0];
                pos[1] += delta[dir, 1];
                #region check if need to change direction
                bool changeDir = dir < 2 ? pos[dir % 2] >= border[dir] : pos[dir % 2] <= border[dir];
                if (changeDir)
                {
                    pos[0] -= delta[dir, 0];
                    pos[1] -= delta[dir, 1];
                    dir = (dir + 1) % 4;
                    int adjust = dir < 2 ? 1 : -1;
                    border[(dir + 2) % 4] = border[(dir + 2) % 4] + adjust;
                    /*switch (dir)
                    {
                        case 0: border[2]++; break;
                        case 1: border[3]++; break;
                        case 2: border[0]--; break;
                        case 3: border[1]--; break;
                    }*/
                    pos[0] += delta[dir, 0];
                    pos[1] += delta[dir, 1];
                }
                #endregion
            }

            #region Show the matrix
            System.Console.WriteLine("{0} :", n);
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    System.Console.Write("{0}\t", a[j, i]);
                }
                System.Console.WriteLine();
            }
            #endregion
        }

        public static int FindMaxIndex(int[] data)
        {
            if (data == null) return -1;
            int L = data.Length;
            int l = 0, r = data.Length - 1, m = -1;
            //special handling for 0, L-1
            if (data[0]>data[1] && data[0] > data[L-1])
                m = 0;
            else if (data[L-1]>data[L-2] && data[L-1]>data[0])
                m = L-1;
            else {
                while (l <= r)
                {
                    m = (l + r) / 2;
                    if (data[m - 1] < data[m] && data[m] > data[m + 1]) break;
                    if (data[l] < data[m])
                    {
                        if (l < m) l = m;
                        else l++;
                    }
                    else if (data[m] < data[r])
                    {
                        r = m;
                    }
                    else
                    {
                        m = -1; break;
                    }
                }
            }

            return m;
        }

        public static int FindKey(int[] data, int key)
        {
            int L = data.Length;
            //special handlinig for 0 or 1 members in the array
            if (L == 0) return -1;
            if (L == 1) return data[0] == key ? 0 : -1;
            int offset = FindMaxIndex(data);
            int l = 0, r = data.Length - 1, m;
            while (l <= r)
            {
                m = (l + r) / 2;
                //int idx = m - offset - 1 % L;
                int idx = (m + offset + 1 )% L;
                if (idx <0) idx+=L;
                if (data[idx] > key)
                {
                    if (r == m) r--;
                    else r = m;
                }
                else if (data[idx] < key)
                {
                    if (l == m) l++;
                    else l = m;
                }
                else return idx;
            }
            return -1;
        }

        public static Pos FindKey(int[,] a, int key)
        {
            int Rows = a.GetLength(0);
            int Columns = a.GetLength(1);
            int m = 0, n = Columns - 1;
            while (m >= 0 && m < Rows && n >= 0 && n < Columns)
            {
                if (a[m, n] > key) n--;
                else if (a[m, n] < key) m++;
                else return new Pos(m, n);
            }
            return new Pos(-1, -1);
        }

        static void TestHeap()
        {
            int[] input = { 3, 6, 12, 8, 9, 7, 5, 4, 10 };
            //int[] input = { 9, 12, 10 };
            MinHeap mh = new MinHeap(input);
            System.Console.WriteLine(mh);
            for (int i = 0; i < 5; i++)
            {
                System.Console.Write(mh.DeHeap() + " ");
                //System.Console.WriteLine(mh);
            }
            for (int i = 3; i < input.Length; i++)
            {
                mh.EnHeap(input[i]);
                //System.Console.WriteLine(mh.Check());
                System.Console.WriteLine(mh);
            }
            System.Console.WriteLine();
        }

        public static string convert(int n)
        {
            if (n <= 0) return "err";
            StringBuilder sb = new StringBuilder();
            int r;
            while (n > 0)
            {
                r = n % 26;
                n = n / 26;
                if (r == 0)
                {
                    r = 26; 
                    n--;
                }
                sb.Insert(0, (char)('a' + r - 1));
            }
            return sb.ToString();
        }

        static int[,] maze = new int[,] {
                               {0,0,1},
                               {1,0,1},
                               {1,0,0}
                           };

 

        static void Maze()
        {

            Stack<Point> track = new Stack<Point>();
            Attempt(0, 0, 3, 3, 2, 2, track);
        }

        static void Attempt(int x, int y, int m, int n, int xe, int ye, Stack<Point> track)
        {
            if (x<0||x>=m||y<0||y>=n) return;
            if (maze[x,y] == 1) return;
            track.Push(new Point(x,y));
            maze[x,y]=1;
            if (x==xe && y==ye) 
            {
                #region ShowTrack
                for (int i = track.Count<Point>() - 1; i >= 0; i--) 
                {
                    System.Console.Write(track.ElementAt<Point>(i) + " ");
                }
                return;
                #endregion
            }
            Attempt(x + 1, y, m, n, xe, ye, track);
            Attempt(x - 1, y, m, n, xe, ye, track);
            Attempt(x, y + 1, m, n, xe, ye, track);
            Attempt(x, y - 1, m, n, xe, ye, track);
            track.Pop();
        }

        public static int Divide(int divident, int divisor)
        {
            int sign = 1;
            if (divident < 0) { sign = -sign; divident = -divident; }
            if (divisor < 0) { sign = -sign; divisor = -divisor; }
            int quotient = _Divide(divident, divisor);
            if (sign < 0) quotient = -quotient;
            return quotient;
        }

        static int _Divide(int divident, int divisor)  // calculate m/n without /
        {
            if (divident < 0 || divisor < 0)
                throw new Exception("divident and divisor must both not less than 0 to call this method.");
            if (divisor == 0) 
                throw new DivideByZeroException();
            int quotient = 1, product = divisor;
            while (product < divident)
            {
                quotient <<= 1;
                product <<= 1;
            }
            //TODO: quotient < 4?
            int _quotient = quotient >> 2, _product = product >> 2;
            while (true)
            {
                if (product == divident) break;
                if (_quotient == 0)  // must check in the beginning of the block, otherwise the adjustment(_product) is not correct
                {
                    if (product > divident) quotient--;
                    break;
                }

                if (product > divident)
                {
                    quotient -= _quotient;
                    product -= _product;
                }
                else
                {
                    quotient += _quotient;
                    product += _product;
                }
                _quotient >>= 1;
                _product >>= 1;
            }
            return quotient;
        }

        public static int SearhInCircleIncreaseList(int n, int[] data)
        {
            int s = 0, e = data.Length - 1, m;
            if (data[s] < data[e]) return s;
            while (s < e - 1)
            {
                m = (s + e) / 2;
                if (data[m] > data[s])
                {
                    s = m;
                }
                else
                {
                    e = m;
                }
            }
            m = data[s] < data[e] ? s : e;
            //if (data[0] > n)
            return m;
        }

        public static void SortN(int[] data)
        {
            int idx, num, nextNum;
            for (int i = 0; i < data.Length; i++)
            {
                if (i == data[i]) continue;
                idx = -1;
                num = data[i];
                while (idx != i)
                {
                    idx = num;
                    nextNum = data[num];
                    data[num] = num;
                    num = nextNum;
                }
            }
        }

        public static int[] SwapSort(int[] data)
        {
            int curr, temp;
            for (int i = 0; i < data.Length; i++)
            {
                //begin a new round of circle switch
                curr = data[i];
                while (data[curr] != curr)
                {
                    //swap curr and data[curr]
                    temp = curr;
                    curr = data[curr];
                    data[temp] = temp;
                }
            }
            return data;
        }

        static void TestBinaryTree()
        {
            Int32?[] values = { 1, 2, 3, 4, null, 5, null, null, null, null, null, 6, 7 };

            BinaryTree<Int32?> bt = BinaryTree<Int32?>.Construct(values);
            System.Console.WriteLine(bt);
            bt.WidthFirstTraverse();
            System.Console.WriteLine();
            System.Console.WriteLine(bt.Height);

            BinaryTree<Int32?> bt2 = bt.Copy();
            System.Console.WriteLine("COPY");
            System.Console.WriteLine(bt2);

            var list = new List<Element<Int32?>>();
            /*
            //1(23)
            list.Add(new Element<Int32?>(ElementType.Data, 1));
            list.Add(new Element<Int32?>(ElementType.LeftBracket, 1));
            list.Add(new Element<Int32?>(ElementType.Data, 2));
            list.Add(null);
            list.Add(new Element<Int32?>(ElementType.RightBracket, 1));
             */

            // 1(2(4\)3(5(67)\))
            list.Add(new Element<Int32?>(ElementType.Data, 1));
            list.Add(new Element<Int32?>(ElementType.LeftBracket, 1));
            list.Add(new Element<Int32?>(ElementType.Data, 2));
            list.Add(new Element<Int32?>(ElementType.LeftBracket, 1));
            list.Add(new Element<Int32?>(ElementType.Data, 4));
            //list.Add(null);
            list.Add(new Element<Int32?>(ElementType.Data, null));
            list.Add(new Element<Int32?>(ElementType.RightBracket, 1));
            list.Add(new Element<Int32?>(ElementType.Data, 3));
            list.Add(new Element<Int32?>(ElementType.LeftBracket, 1));
            list.Add(new Element<Int32?>(ElementType.Data, 5));
            list.Add(new Element<Int32?>(ElementType.LeftBracket, 1));
            list.Add(new Element<Int32?>(ElementType.Data, 6));
            list.Add(new Element<Int32?>(ElementType.Data, 7));
            list.Add(new Element<Int32?>(ElementType.RightBracket, 1));
            //list.Add(null);
            list.Add(new Element<Int32?>(ElementType.Data, null));
            list.Add(new Element<Int32?>(ElementType.RightBracket, 1));
            list.Add(new Element<Int32?>(ElementType.RightBracket, 1));
            BinaryTree<Int32?> bt1 = BinaryTree<Int32?>.Construct(list);
            System.Console.WriteLine(bt1);

            List<TreeNode<int?>> list1 = bt1.ToList();
            BinaryTree<int?> bt3 = BinaryTree<int?>.FromList(list1);
            System.Console.WriteLine(bt3);
        }

        static void TestMergeList()
        {
            SingleLinkedList la = new SingleLinkedList(new int[] {3, 8, 12, 15});
            SingleLinkedList lb = new SingleLinkedList(new int[] { 4, 7, 13, 16, 17 });
            //la = new SingleLinkedList(new int[] {  });
            //lb = new SingleLinkedList(new int[] {  });
            la.MergeSortedLinkedList(lb);
            la.ShowList();
            lb.ShowList();
        }

        static string Circle(uint b, uint c) {
            StringBuilder res = new StringBuilder();
	        res.Append(b/c);
	        b = (b % c) * 10;
	        if (b == 0) 
            {
                return res.ToString();
            }
	        res.Append('.');
            int idxDot = res.Length;
	        List<uint> bHistory = new List<uint>();
            bHistory.Add(b);
	        while (true) {
		        res.Append(b/c);
		        b = (b % c) * 10;
		        if (b == 0) 
                {
                    return res.ToString();
                }
		        int idx = bHistory.IndexOf(b);
		        if(idx >= 0) {
			        res.Insert(idx + idxDot, '(');
			        res.Append(')');
			        break;
                }
                else
                {
                    bHistory.Add(b);
                }
	        }
            return res.ToString();
        }

        public static IEnumerable<List<int>> MaxSum(int max, int sum)
        {
            if (max < 1) yield break;
            if (sum < 0) yield break;
            if (sum == 0)
            {
                yield return new List<int>();
                yield break;
            }
            if ((max + 1) * max / 2 < sum) yield break; //the max sum possible still less than the requried sum
            if (max == 1)
            {
                if (sum == 1) yield return new List<int> { 1 };
                yield break;
            }

            //recursively call
            //exclude max
            foreach (List<int> subResult in MaxSum(max - 1, sum))
            {
                yield return subResult;
            }
            //include max
            foreach (List<int> subResult in MaxSum(max - 1, sum - max))
            {
                subResult.Add(max);
                yield return subResult;
            }            
        }
    }
}
