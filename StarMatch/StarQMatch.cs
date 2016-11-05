using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarMatch
{
    class StarQMatch
    {
        private string src, pattern;
        private bool[,] allMatch;

        public string Src
        {
            get
            {
                return src;
            }

            set
            {
                src = value.ToLower();
            }
        }

        public string Pattern
        {
            get
            {
                return pattern;
            }

            set 
            { 
                pattern = value.ToLower(); 
            }
        }

        public bool Match()
        {
            return Match(0, 0); 
        }

        private bool Match(int idxSrc, int idxPtn)
        {
            if (isSuccess(idxSrc, idxPtn))
            {
                return true;
            }
            if (idxPtn >= Pattern.Length || idxSrc >= Src.Length) return false;
            
            if (Pattern[idxPtn] == '*')
            {
                //match
                bool match1 = Match(idxSrc + 1, idxPtn);
                if (match1) return true;
                //no match
                /*find the first non * position after current idxPtn
                int i;
                for (i = idxPtn + 1; i < Pattern.Length && Pattern[i] == '*'; i++);*/
                bool match2 = Match(idxSrc, idxPtn + 1);
                return match2;
            }
            else
            {
                char chrSrc = Src[idxSrc], chrPtn = Pattern[idxPtn];
                if (chrSrc == chrPtn || chrPtn == '?')
                {
                    return Match(idxSrc + 1, idxPtn + 1);
                }
                else
                {
                    return false;
                }
            }
        }

        private bool isSuccess(int idxSrc, int idxPtn)
        {
            if (idxSrc != Src.Length) return false;
            if (idxPtn == Pattern.Length) return true;
            //test if all remaining char in pattern is *
            for (int i = idxPtn; i < Pattern.Length; i++)
            {
                if (Pattern[i] != '*') return false;
            }
            return true;
        }

        // match(Si, Pi) = pattern[Pi]=='*' ? 
        //      match(Si+1, Pi) || match(Si, Pi+1) :
        //      source[Si] == pattern[Pi] || pattern[Pi] == '?' ? match(Si+1, Pi+1) : false;
        public bool Match2()
        {
            initBoundaryData();
            for (int i = Src.Length - 1; i >= 0; i--)
            {
                for (int j = Pattern.Length - 1; j >= 0; j--)
                {
                    allMatch[i, j] =
                        Pattern[j] == '*' ?
                        allMatch[i + 1, j] || allMatch[i, j + 1] :
                        Src[i] == Pattern[j] || Pattern[j] == '?' ? allMatch[i + 1, j + 1] : false;
                }
            }
            return allMatch[0, 0];
        }

        private void initBoundaryData()
        {
            allMatch = new bool[Src.Length + 1, Pattern.Length + 1];
            for (int i = 0; i < Src.Length; i++)
            {
                allMatch[i, Pattern.Length] = false;
            }
            int lastNotStarIndex = -1;
            for (int i = Pattern.Length - 1; i >= 0; i--)
            {
                if (Pattern[i] != '*')
                {
                    lastNotStarIndex = i;
                    break;
                }
            }
            for (int i = 0; i <= lastNotStarIndex; i++)
            {
                allMatch[Src.Length, i] = false;
            }
            for (int i = lastNotStarIndex + 1; i < Pattern.Length; i++)
            {
                allMatch[Src.Length, i] = true;
            }
            allMatch[Src.Length, Pattern.Length] = true; 
        }
    }
}
