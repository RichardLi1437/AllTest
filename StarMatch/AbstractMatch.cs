using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarMatch
{
    abstract class AbstractMatch
    {
        private string src, pattern;

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

        abstract public bool FullMatch();

        public string FullMatchResult()
        {
            return FullMatch() ? "YES" : "NO";
        }
    }
}
