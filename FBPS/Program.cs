using System;
using System.Collections.Generic;

namespace FBPS
{
    class Program
    {
        static void Main(string[] args)
        {
            //string e = "-(x+1+(x-((x+3)+x)+1))=6+(x-3)";
            string e = "(-x-x+3-(4-x))=-(3+x)";
            Equation eq = new Equation(e);
            double x = eq.SolveForX();
            System.Console.WriteLine("x={0}", x);
        }
    }

    class Equation
    {
        private string eq;
        private Expression expL, expR;

        public Equation(string eq)
        {
            this.eq = eq;
            split();
        }

        // get any value for X that satisfies the expression. -1 if no such value exists.
        // 1) "x+1=5"
        // 2) "x+(x-3+(x-2))+1=-x+1
        // We have +-()x=0-9
        // Not valid:  "3x" or anything with multiplication or division
        // assume expression is valid.
        public double SolveForX()
        {
            expL.processR();
            expR.processR();
            //expL.process();
            //expR.process();
            if (expR.B - expL.B == 0 && expL.A - expR.A == 0) return 0;
            if (expL.A - expR.A == 0) return -1;
            return (expR.B - expL.B) * 1.0 / (expL.A - expR.A);
        }

        private void split()
        {
            int idx = eq.IndexOf('=');
            expL = new Expression(eq.Substring(0, idx));
            expR = new Expression(eq.Substring(idx + 1));
        }        

    }

    class Expression
    {
        private string e;
        private int pos;
        private int a, b;

        public Expression(string e)
        {
            this.e = e;
            pos = 0;
        }

        public int A { get { return a; } }

        public int B { get { return b; } }

        public void processR()
        {
            a = 0; b = 0;
            Expression expSub;
            string st, eSub;
            while ((st = readNext()) != null)
            {
                switch (st)
                {
                    case "x":
                        a++;
                        break;
                    case "-x":
                        a--;
                        break;
                    case "+(":
                    case "(":
                    case "-(":
                        eSub = readtoMatchCloseBracket();
                        expSub = new Expression(eSub);
                        expSub.processR();
                        if (st == "-(")
                        {
                            a -= expSub.A; b -= expSub.B;
                        }
                        else
                        {
                            a += expSub.A; b += expSub.B;
                        }
                        break;
                    //case ')': //there should be no ) since all ) have been read by readtoMatchCloseBracket()
                    default:  //should be numbers with its immediate sign at the beginning
                        b += Int32.Parse(st);
                        break;
                }
            }
        }

        public void process()
        {
            a = 0; b = 0;
            Stack<int> signStack = new Stack<int>();
            int effectiveSign = 1;

            string st;
            while ((st = readNext()) != null)
            {
                switch (st)
                {
                    case "x":
                        a += effectiveSign;
                        break;
                    case "-x":
                        a += -effectiveSign;
                        break;
                    case "+(":
                    case "(":
                        signStack.Push(1);
                        break;
                    case "-(":
                        signStack.Push(-1);
                        effectiveSign = -effectiveSign;
                        break;
                    case ")":
                        int sign = signStack.Pop();
                        if (sign < 0) effectiveSign = -effectiveSign;
                        break;
                    default:  //should be numbers with its immediate sign at the beginning
                        b += effectiveSign * Int32.Parse(st);
                        break;
                }
            }
        }

        private string readtoMatchCloseBracket()
        {
            int b = 1, start = pos;
            while (pos < e.Length)
            {
                switch (e[pos++])
                {
                    case '(':
                        b++;
                        break;
                    case ')':
                        b--;
                        break;
                }
                if (b == 0) break;
            }
            return e.Substring(start, pos - 1 - start);
        }

        private string readNext()
        {
            if (pos >= e.Length) return null;
            char ch = e[pos++];
            switch (ch)
            {
                case 'x':
                    return "x";
                case '+':
                case '-':
                    switch (e[pos++])
                    {
                        case '(':
                            return ch == '+' ? "+(" : "-(";
                        case 'x':
                            return ch == '+' ? "x" : "-x";
                        default:
                            return ch + readNumber();
                    }
                case '(':
                    return "+(";
                case ')':
                    return ")";
                default: //digits
                    return readNumber();
            }
        }

        private string readNumber()
        {
            int start = pos - 1;
            while (pos < e.Length && e[pos] >= '0' && e[pos] <= '9') pos++;
            int len = pos - start;
            return e.Substring(start, len);
        }
    }
}
