using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FBTest
{
    class Program
    {
        static public void WriteOutput(bool answer)
        {
            string output = answer ? "Yes" : "No";
            System.Console.WriteLine("{0}", output);
        }

        static void Main(string[] args)
        {
            //ResolvePuzzle();
            ResolveBalance();
            //System.Console.ReadKey();
        }


        static void ResolveBalance()
        {
            List<Scale> allScales = new List<Scale>();
/*
            allScales.Add(new Scale(0, 3, new int[]{}, 0, new int[]{5}, allScales));
            allScales.Add(new Scale(1, 0, new int[] { }, 0, new int[] { }, allScales));
            allScales.Add(new Scale(2, 0, new int[] { }, 0, new int[] { }, allScales));
            allScales.Add(new Scale(3, 0, new int[] { }, 1, new int[] { 4 }, allScales));
            allScales.Add(new Scale(4, 7, new int[] { }, 0, new int[] {  }, allScales));
            allScales.Add(new Scale(5, 0, new int[] { 2, 3 }, 6, new int[] { 1 }, allScales));
 */

            allScales.Add(new Scale(0, 3, new int[] { 6 }, 0, new int[] { 5 }, allScales));
            allScales.Add(new Scale(1, 0, new int[] { }, 0, new int[] { }, allScales));
            allScales.Add(new Scale(2, 0, new int[] { }, 0, new int[] { }, allScales));
            allScales.Add(new Scale(3, 0, new int[] { }, 1, new int[] { 4 }, allScales));
            allScales.Add(new Scale(4, 7, new int[] { }, 0, new int[] { }, allScales));
            allScales.Add(new Scale(5, 0, new int[] { 2, 3 }, 6, new int[] { 1 }, allScales));
            allScales.Add(new Scale(6, 4, new int[] { 7 }, 6, new int[] { 8 }, allScales));
            allScales.Add(new Scale(7, 0, new int[] {  }, 3, new int[] {  }, allScales));
            allScales.Add(new Scale(8, 4, new int[] {  }, 0, new int[] {  }, allScales));
            
            //balance
            for (int i = 0; i < allScales.Count; i++)
            {
                if (allScales[i].WBalanced < 0)
                {
                    allScales[i].balance();
                }
                allScales[i].ShowBalance();
            }

            //check
            bool allBalanced = true;
            for (int i = 0; i < allScales.Count; i++)
            {
                if (!allScales[i].Balanced) 
                {
                    System.Console.WriteLine("Scale {0} is NOT balanced!", i);
                    allBalanced = false;
                }
            }
            if (allBalanced)
                System.Console.WriteLine("All scales are balanced!");
        }

        static void ResolvePuzzle()
        {
            string stC = System.Console.ReadLine();
            int c = Int32.Parse(stC);
            //System.Console.ReadLine();  // the empty input line
            for (int i = 0; i < c; i++)
            {
                Puzzle puzzle = new Puzzle();
                bool answer = puzzle.Resolve();
                WriteOutput(answer);
            }
        }
    }

    class Scale
    {
        const int WEIGHT_SCALE = 10;

        int id;
        int Wl, Wr;
        int AddL, AddR;
        public int WBalanced = -1;
        //List<Scale> Sl, Sr;
        int[] Sli, Sri;
        List<Scale> allScales;

        public int Weight {
            get 
            {
                int w = Wl + Wr + AddL + AddR + WEIGHT_SCALE;
                foreach (int idx in Sli) w += allScales[idx].Weight;
                foreach (int idx in Sri) w += allScales[idx].Weight;
                return w;
            }
        }

        public bool Balanced
        {
            get
            {
                int wl = Wl + AddL, wr = Wr + AddR;
                foreach (int idx in Sli) wl += allScales[idx].Weight;
                foreach (int idx in Sri) wr += allScales[idx].Weight;
                return wl == wr;
            }
        }

        public Scale(int id, int Wl, int[] Sli, int Wr, int[] Sri, List<Scale> allScales) 
        {
            this.id = id;
            this.Wl = Wl;
            this.Wr = Wr;
            this.Sli = Sli;
            this.Sri = Sri;
            this.allScales = allScales;
        }

        public int balance()
        {
            if (WBalanced > 0) return WBalanced;

            //balance all the scales in left;
            int wl = Wl, wr = Wr;
            //foreach (Scale s in Sl) wl += s.balance();
            foreach (int idx in Sli) wl += allScales[idx].balance();
            //balance all the scales in right;
            //foreach (Scale s in Sr) wr += s.balance();
            foreach (int idx in Sri) wr += allScales[idx].balance();
            if (wl > wr) { AddL = 0; AddR = wl - wr; }
            else { AddL = wr - wl; AddR = 0; }
            WBalanced = wl + wr + AddL + AddR + WEIGHT_SCALE;
            return WBalanced;
        }

        public void ShowBalance()
        {
            System.Console.WriteLine("{0}: {1}, {2}", id, AddL, AddR);
        }
    }

    class Puzzle
    {
        private int n, k, q;
        private int[,] g;
        private int[] b, bCurr;
        private int[] s;
        private Dictionary<int, bool> failPath;

        public Puzzle()
        {
            string L0 = System.Console.ReadLine();
            string[] stNKQ = L0.Split(new char[] { ' ' });
            n = Int32.Parse(stNKQ[0]);
            k = Int32.Parse(stNKQ[1]);
            q = Int32.Parse(stNKQ[2]);
            g = new int[q + 1, k + 1];
            s = new int[k + 1];
            b = new int[q + 1];

            for (int i = 1; i <= q; i++)
            {
                string Li = System.Console.ReadLine();
                string[] LiSep = Li.Split(new char[] { ' ' });
                for (int j = 1; j <= k; j++)
                {
                    g[i, j] = Int32.Parse(LiSep[j - 1]);
                }
                b[i] = Int32.Parse(LiSep[k]);
            }
        }

        private void WriteSolution()
        {
            for (int i = 1; i <= k; i++) 
            {
                System.Console.Write("{0}\t", s[i]);
            }
            System.Console.WriteLine();
        }

        public bool Resolve()
        {
            bCurr = new int[q + 1];
            for (int i = 1; i <= q; i++)
            {
                bCurr[i] = 0;
            }
            failPath = new Dictionary<int, bool>();

            return Resolve(1);
        }

        public bool Resolve(int idx)
        {
            if (idx < 1) return false;
            if (idx > k) 
            {
                WriteSolution();
                return true;
            }
            if (failPath.ContainsKey(getKey(bCurr, idx)))
                return false;
            for (s[idx] = 1; s[idx] <= n; s[idx]++)
            {
                bool fail = false;
                int rollbackIdx = q;
                for (int i = 1; i <= q; i++)
                {
                    if (s[idx] == g[i, idx])
                    {
                        bCurr[i]++;
                    }
                    if (bCurr[i] > b[i] || bCurr[i] + k - idx < b[i])
                    {
                        fail = true;
                        rollbackIdx = i;
                        break;
                    }
                }
                if (!fail)
                {
                    if (Resolve(idx + 1)) return true;
                }
                // rollback bCurr for next attemp
                for (int i = 1; i <= rollbackIdx; i++)
                {
                    if (s[idx] == g[i, idx]) 
                        bCurr[i]--;
                }
            }
            // mark false track
            failPath.Add(getKey(bCurr, idx), true);
            return false;
        }

        private int getKey(int[] bCurr, int idx)
        {
            int r = k + 1;
            int key = idx;
            for (int i = 1; i < bCurr.Length; i++)
            {
                key = key * r + bCurr[i];
            }
            return key;
        }
    }
}
