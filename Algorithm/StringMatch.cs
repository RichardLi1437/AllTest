using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Algorithm
{
    public struct MyMatch 
    { 
        public bool matched; 
        public int start, end;
        public MyMatch(bool m, int s, int e)
        {
            matched = m;
            start = s;
            end = e;
        }
    }

    public class StringMatch
    {
        public StringMatch(string src, string pattern)
        {
            Src = src.ToLower();
            Pattern = pattern.ToLower();
        }

        public string Src { get; set; }
        public string Pattern { get; set; }

        //pattern here without *
        public MyMatch SimpleMatch(string pattern, int srchStart) {
            int idxSrc = srchStart, idxTryStart = srchStart;
            int idxPtn = 0;
            int matchType =IsMatch(pattern, idxPtn, idxSrc);
            while (true)
            {
                switch (matchType)
                {
                    case -1:  // no match at all
                        return new MyMatch(false, -1, -1);
                    case 0:  // no match for this position, try match next position
                        idxTryStart++;
                        idxSrc = idxTryStart;
                        idxPtn = 0;
                        break;
                    case 1:  // match for this position, try match next position
                        idxPtn++; idxSrc++;
                        break;
                    case 2: // full match
                        return new MyMatch(true, idxTryStart, idxSrc - 1); 
                }
                matchType = IsMatch(pattern, idxPtn, idxSrc);
            }
            //if (
            /*Regex regx = new Regex(pattern);
            Match match = regx.Match(Src, srchStart);
            MyMatch myMatch;
            myMatch.matched = match.Success;
            myMatch.start = match.Index;
            myMatch.end = match.Index + match.Length - 1;
            return myMatch;*/
        }

        private int IsMatch(string pattern, int idxPtn, int idxSrc)
        {
            if (idxPtn >= pattern.Length) return 2;  //match!
            if (idxSrc >= Src.Length)
            {
                return -1; //no match! 
            }
            char chP = pattern[idxPtn], chS = Src[idxSrc];
            return (chP == '?' || chP == chS) ? 1 : 0;
        }

        public MyMatch Match() {
	        //divide pattern with * to multiple patterns without *
	        //e.g. ab*cde*f ==> ab, cde, f
            string[] patterns = Pattern.Split(new char[] { '*' });
	        int start = 0;
	        MyMatch globalMatch = new MyMatch(false, -1, -1);
            MyMatch partialMatch = new MyMatch(false, -1, -1);
	        for (int i = 0; i < patterns.Length; i++) {
		        partialMatch = SimpleMatch(patterns[i], start);
                if (!partialMatch.matched)
                {
                    return globalMatch;
                }
		        if (i == 0) { globalMatch.start =  partialMatch.start;}
		        start = partialMatch.end + 1;
	        }

	        globalMatch.end = start - 1;
	        globalMatch.matched = true;
            return globalMatch;  // substring match
        }

    }
}
