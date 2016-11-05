using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Expresion
{
    public enum SymbolType
    {
        OPERATOR, OPERAND, LEFT_BRACKET, RIGHT_BRACKET, COMMA
    }

    public enum Operator 
    {
        ADD, MINUS, MUL, DIV, POW, SIN, COS, LOG, LG, LN, MIN, MAX
    }

    public class Symbol
    {
        //public string stSymbol;
        public SymbolType Type;
        public double Value;
        public Operator Op;
        public int OpPriority;

        public Symbol(Operator op)
        {
            Type = SymbolType.OPERATOR;
            Op = op;
        }

        public Symbol(double num)
        {
            Type = SymbolType.OPERAND;
            Value = num;
        }

        public Symbol(SymbolType type)
        {
            Type = type;
        }

        public override string ToString()
        {
            switch (Type)
            {
                case SymbolType.COMMA: return ",";
                case SymbolType.LEFT_BRACKET: return "(";
                case SymbolType.RIGHT_BRACKET: return ")";
                case SymbolType.OPERAND: return Value.ToString();
                case SymbolType.OPERATOR: return Op.ToString();
                default: return "Invalid";
            }
        }
    }

    public class ExpNode
    {
        public Symbol Value { get; set;}

        //public ExpNode ExpLeft {get; set;}
        
        //public ExpNode ExpRight { get; set; }

        public List<ExpNode> SubExps { get; set; }

        public ExpNode(Symbol value)
        {
            Value = value;
            //ExpLeft = null;
            //ExpRight = null;
            SubExps = null;
        }

        public Double Result
        {
            get
            {
                if (Value.Type == SymbolType.OPERAND)
                    return Value.Value;
                else //must be Operator
                {
                    //double leftResult = ExpLeft != null ? ExpLeft.Result : 0;
                    //double rightResult = ExpRight != null ? ExpRight.Result : 0;
                    double result;
                    switch (Value.Op)
                    {
                        case Operator.ADD:
                            result = 0;
                            foreach (ExpNode subExp in SubExps)
                            {
                                result += subExp.Result;
                            }
                            return result;
                        case Operator.MINUS:
                            if (SubExps.Count == 0) return 0;
                            else if (SubExps.Count == 1) return -SubExps[0].Result;
                            else return SubExps[0].Result - SubExps[1].Result;                            
                        case Operator.MUL:
                            result = 1;
                            foreach (ExpNode subExp in SubExps)
                            {
                                result *= subExp.Result;
                            }
                            return result;
                        case Operator.DIV: 
                            if (SubExps.Count == 0) return 1;
                            else if (SubExps.Count == 1) return 1.0/SubExps[0].Result;
                            else return SubExps[0].Result / SubExps[1].Result;                            
                        case Operator.POW:
                            if (SubExps.Count == 2) return Math.Pow(SubExps[0].Result, SubExps[1].Result);
                            else return 0; //TODO: invalid expression
                        case Operator.SIN:
                            return Math.Sin(SubExps[0].Result);
                        case Operator.COS:
                            return Math.Cos(SubExps[0].Result);
                        case Operator.MAX:
                            result = double.MinValue;
                            foreach (ExpNode subExp in SubExps)
                            {
                                double subResult = subExp.Result;
                                if (subResult > result) result = subResult;
                            }
                            return result;
                        case Operator.MIN:
                            result = double.MaxValue;
                            foreach (ExpNode subExp in SubExps)
                            {
                                double subResult = subExp.Result;
                                if (subResult < result) result = subResult;
                            }
                            return result;
                        case Operator.LOG:
                            return Math.Log(SubExps[1].Result, SubExps[0].Result);
                        case Operator.LG:
                            return Math.Log10(SubExps[0].Result);
                        case Operator.LN:
                            return Math.Log(SubExps[0].Result, Math.E);
                        default: return 0;
                    }
                }
            }
        }

        public string PreOrder()
        {
            if (Value.Type == SymbolType.OPERAND)
            {
                return Value.Value.ToString();
            }
            else
            {
                StringBuilder sBuilder = new StringBuilder(string.Format("{0}(", Value.Op));
                foreach (ExpNode subExp in SubExps)
                {
                    sBuilder.AppendFormat("{0},", subExp.PreOrder());
                }
                sBuilder.Remove(sBuilder.Length - 1, 1);
                sBuilder.Append(')');
                return sBuilder.ToString();
            }
        }

        public string PostOrder()
        {
            if (Value.Type == SymbolType.OPERAND) return Value.Value.ToString();

            StringBuilder sBuilder = new StringBuilder('(');
            foreach (ExpNode subExp in SubExps)
            {
                sBuilder.Append(subExp.PostOrder());
                sBuilder.Append(',');
            }
            sBuilder[sBuilder.Length - 1] = ')';
            sBuilder.Append(Value.Op);
            return sBuilder.ToString();
        }

        public string NormalOrder()
        {
            if (Value.Type == SymbolType.OPERAND)
            {
                return Value.Value.ToString();
            }
            else
            {
                char op;
                switch (Value.Op)
                {
                    case Operator.ADD: op = '+'; break;
                    case Operator.MINUS: op = '-'; break;
                    case Operator.MUL: op = '*'; break;
                    case Operator.DIV: op = '/'; break;
                    case Operator.POW: op = '^'; break;
                    default: op = '\\'; break;
                }
                if (op == '\\')
                {
                    StringBuilder sBuilder = new StringBuilder(string.Format("{0}(", Value.Op));
                    foreach (ExpNode subExp in SubExps)
                    {
                        sBuilder.Append(string.Format("{0},", subExp.NormalOrder()));
                    }
                    sBuilder.Remove(sBuilder.Length - 1, 1);
                    sBuilder.Append(')');
                    return sBuilder.ToString();
                }
                else
                {
                    StringBuilder sBuilder = new StringBuilder();
                    ExpNode leftExp, rightExp;
                    if (SubExps.Count == 0)
                    {
                        leftExp = null; rightExp = null;
                    }
                    else if (SubExps.Count == 1)
                    {
                        // the -xxx and +xxx case
                        leftExp = null; rightExp = SubExps[0];
                    }
                    else
                    {
                        leftExp = SubExps[0]; rightExp = SubExps[1];
                    }

                    if (leftExp != null)
                    {
                        if (leftExp.Value.Type == SymbolType.OPERATOR && Expression2.OpPriMap[leftExp.Value.Op] < Expression2.OpPriMap[Value.Op])
                        {
                            sBuilder.Append(string.Format("({0})", leftExp.NormalOrder()));
                        }
                        else
                            sBuilder.Append(leftExp.NormalOrder());
                    }
                    
                    sBuilder.Append(op);

                    if (rightExp != null)
                    {
                        if (rightExp.Value.Type == SymbolType.OPERATOR && Expression2.OpPriMap[rightExp.Value.Op] <= Expression2.OpPriMap[Value.Op])
                        {
                            sBuilder.Append(string.Format("({0})", rightExp.NormalOrder()));
                        }
                        else
                            sBuilder.Append(rightExp.NormalOrder());
                    }

                    return sBuilder.ToString();
                }
            }
        }

        public override string ToString()
        {
            return NormalOrder();
        }
    }

    public class Expression2
    {
        private string stExp;
        private List<Symbol> lstSymbol;
        private ExpNode expTree;
        public static readonly Dictionary<Operator, int> OpPriMap;
        private static readonly Dictionary<string, Operator> FuncMap;

        public Expression2(string stExp) 
        {
            this.stExp = Normalize(stExp);
            lstSymbol = Str2SymbolList();
            PrioritizeOperators();
            expTree = SymbolLst2ExpTree(0, lstSymbol.Count() - 1);
        }

        static Expression2()
        {
            OpPriMap = new Dictionary<Operator, int>();
            OpPriMap.Add(Operator.ADD, 1);
            OpPriMap.Add(Operator.MINUS, 1);
            OpPriMap.Add(Operator.MUL, 2);
            OpPriMap.Add(Operator.DIV, 2);
            OpPriMap.Add(Operator.POW, 3);
            OpPriMap.Add(Operator.SIN, 4);
            OpPriMap.Add(Operator.COS, 4);
            OpPriMap.Add(Operator.LOG, 4);
            OpPriMap.Add(Operator.MIN, 4);
            OpPriMap.Add(Operator.MAX, 4);
            OpPriMap.Add(Operator.LG, 4);
            OpPriMap.Add(Operator.LN, 4);

            FuncMap = new Dictionary<string, Operator>();
            FuncMap.Add("sin", Operator.SIN);
            FuncMap.Add("cos", Operator.COS);
            FuncMap.Add("log", Operator.LOG);
            FuncMap.Add("min", Operator.MIN);
            FuncMap.Add("max", Operator.MAX);
            FuncMap.Add("lg", Operator.LG);
            FuncMap.Add("ln", Operator.LN);
        }

        public double Result()
        {
            return expTree.Result;
        }

        public string PreOrder()
        {
            return expTree.PreOrder();
        }

        public override string ToString()
        {
            return expTree.NormalOrder();
        }
        private string Normalize(string strExp)
        {
            return strExp.Replace(" ", "").Replace("\t", "").Replace("\n", "").Replace("\r", "");
        }

        private List<Symbol> Str2SymbolList()
        {
            List<Symbol> lstSym = new List<Symbol>();
            Symbol symbol;
            int i = 0;
            while (i < stExp.Length)
            {
                char ch = stExp[i];
                if (Char.IsDigit(ch))
                {
                    i += (ReadNumber(i, out symbol) - 1); //we already have i++ for each time, so need -1 here
                }
                else if (Char.IsLetter(ch))
                {
                    //TODO: identify function and/or variables
                    //if reserved function words, function, otherwise, variables
                    i += ReadIdentifier(i, out symbol) - 1;
                }
                else if (ch == '(')
                {
                    symbol = new Symbol(SymbolType.LEFT_BRACKET);
                }
                else if (ch == ')')
                {
                    symbol = new Symbol(SymbolType.RIGHT_BRACKET);
                }
                else if (ch == '+')
                {
                    symbol = new Symbol(Operator.ADD);
                }
                else if (ch == '-')
                {
                    symbol = new Symbol(Operator.MINUS);
                }
                else if (ch == '*')
                {
                    symbol = new Symbol(Operator.MUL);
                }
                else if (ch == '/')
                {
                    symbol = new Symbol(Operator.DIV);
                }
                else if (ch == '^')
                {
                    symbol = new Symbol(Operator.POW);
                }
                else if (ch == ',')
                {
                    symbol = new Symbol(SymbolType.COMMA);
                }
                else
                    symbol = null;

                lstSym.Add(symbol);
                i++;
            }
            return lstSym;
        }

        private int ReadNumber(int idx, out Symbol symbol)
        {
            int i = idx;
            while (i < stExp.Length)
            {
                char ch = stExp[i];
                if (Char.IsDigit(ch) || ch == '.')
                    i++;
                else
                    break;
            }
            int len = i - idx;
            string stNum = stExp.Substring(idx, len);
            double num = Double.Parse(stNum);
            symbol = new Symbol(num);
            return len;
        }

        private int ReadIdentifier(int idx, out Symbol symbol)
        {
            int i = idx;
            while (i < stExp.Length)
            {
                char ch = stExp[i];
                if (Char.IsLetter(ch))
                    i++;
                else
                    break;
            }
            int len = i - idx;
            string stIdentifier = stExp.Substring(idx, len);
            if (FuncMap.ContainsKey(stIdentifier))
            {
                symbol = new Symbol(FuncMap[stIdentifier]);
            }
            else
            {
                //TOOD: variables
                symbol = null;
            }
            return len;
        }

        const int PRI_PER_LEVEL = 10;
        private void PrioritizeOperators()
        {            
            int count = lstSymbol.Count();
            int level = 0;
            for (int i = 0; i < count; i++)
            {
                switch (lstSymbol[i].Type)
                {
                    case SymbolType.LEFT_BRACKET: level++; break;
                    case SymbolType.RIGHT_BRACKET: level--; break;
                    case SymbolType.OPERATOR: lstSymbol[i].OpPriority = PRI_PER_LEVEL * level + OpPriMap[lstSymbol[i].Op]; break;
                }
            }
        }

        private ExpNode SymbolLst2ExpTree(int start, int end)
        {
            if (start > end) return null;
            while(RemoveOutestBracketPari(ref start, ref end)); //use while to handle the case like (((...))), we need to remove all the 3 pairs
            int idx = FindLowestPriOperator(start, end);
            if (idx < 0)
            {
                //no operators, return the first symbol, should be a operand for a valid expression
                return new ExpNode(lstSymbol[start]);
            }
            else
            {
                ExpNode rootOp = new ExpNode(lstSymbol[idx]);
                switch (rootOp.Value.Op)
                {
                    case Operator.ADD:
                    case Operator.MINUS:
                    case Operator.MUL:
                    case Operator.DIV:
                    case Operator.POW:
                        // operator in middle, and 2 operands operators
                        rootOp.SubExps = new List<ExpNode>();
                        if (start <= idx - 1)
                            rootOp.SubExps.Add(SymbolLst2ExpTree(start, idx - 1));
                        if (idx + 1 <= end)
                            rootOp.SubExps.Add(SymbolLst2ExpTree(idx + 1, end));
                        break;
                    default:
                        // others are function like operators, i.e. func(p1, p2, ..., pn)
                        rootOp.SubExps = ReadFuncParameters(start, end);
                        break;
                }
                return rootOp;
            }
        }

        private List<ExpNode> ReadFuncParameters(int start, int end)
        {
            var parameters = new List<ExpNode>();
            int level = 0;
            int paramStart = start, paramEnd;
            for (int i = start; i <= end; i++)
            {
                switch (lstSymbol[i].Type)
                {
                    case SymbolType.LEFT_BRACKET:
                        level++;
                        if (level == 1)
                        {
                            paramStart = i + 1;
                        }
                        break;
                    case SymbolType.RIGHT_BRACKET:
                        level--;
                        if (level == 0)
                        {
                            paramEnd = i - 1;
                            parameters.Add(SymbolLst2ExpTree(paramStart, paramEnd));
                        }
                        break;
                    case SymbolType.COMMA:
                        if (level == 1)
                        {
                            paramEnd = i - 1;
                            parameters.Add(SymbolLst2ExpTree(paramStart, paramEnd));
                            paramStart = i + 1;
                        }
                        break;
                }
            }
            return parameters;
        }

        private bool RemoveOutestBracketPari(ref int start, ref int end)
        {
            if (lstSymbol[start].Type != SymbolType.LEFT_BRACKET || lstSymbol[end].Type != SymbolType.RIGHT_BRACKET) return false;
            int level = 1;
            for (int i = start + 1; i <= end; i++)
            {
                switch (lstSymbol[i].Type)
                {
                    case SymbolType.LEFT_BRACKET: level++; break;
                    case SymbolType.RIGHT_BRACKET:
                        level--;
                        if (level == 0 && i != end) return false;
                        break;
                }
            }
            if (level == 0)
            {
                start++;
                end--;
                return true;
            }
            else
            {
                //bracket mismatch
                return false;
            }
        }

        private int FindLowestPriOperator(int start, int end)
        {
            int idx = -1;
            int minPri = Int32.MaxValue;
            for (int i = start; i <= end; i++)
            {
                if (lstSymbol[i].Type != SymbolType.OPERATOR) continue;
                // same priority, calculate the left operator first, so should use <= instead of < here
                if (lstSymbol[i].OpPriority <= minPri)
                {
                    idx = i;
                    minPri = lstSymbol[i].OpPriority;
                }
            }
            return idx;
        }
    }
}
