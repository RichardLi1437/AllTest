using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Calculator
{
    class Function
    {
        private string name;
        private List<double> paramList;

        public Function(string name)
        {
            this.name = name;
            paramList = new List<double>();
        }

        public void AddParam(double param)
        {
            paramList.Add(param);
        }

        public double Result
        {
            get
            {
                switch (name)
                {
                    case "log":
                        return Math.Log(paramList[1], paramList[0]);
                    case "ln":
                        return Math.Log(paramList[0]);
                    case "lg":
                        return Math.Log10(paramList[0]);
                    case "sin":
                        return Math.Sin(paramList[0]);
                    case "cos":
                        return Math.Cos(paramList[0]);
                    case "max":
                        return paramList.Max<double>();
                    case "min":
                        return paramList.Min<double>();
                    case "avg":
                        return paramList.Average();
                    default:
                        return -1;
                }
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(name);
            sb.Append('(');
            if (paramList.Count > 0)
            {
                sb.Append(paramList[0]);
            }
            for (int i = 1; i < paramList.Count; i++)
            {
                sb.Append(", ");
                sb.Append(paramList[i]);
            }
            sb.Append(')');
            return sb.ToString();
        }
    }
}
