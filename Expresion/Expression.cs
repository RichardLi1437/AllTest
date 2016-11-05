using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Calculator
{
    class Expression
    {
        private string stExp;
        private List<Element> lstElement;
        //private List<Expression> lstExp;
        
        public Expression(string exp)
        {
            this.stExp = exp;
            PreScan();
        }

        public Expression(List<Element> lstElement)
        {
            this.lstElement = lstElement;
        }

        public double Result()
        {
            Scan4Func();
            Scan4Branket();
            Scan4Pow();
            Scan4MulDiv();
            return Scan4AddMinus();
        }

        private void Scan4Func()
        {
            for (int i = 0; i < lstElement.Count; i++)
            {
                if (lstElement[i].type == ElementType.Function)
                {
                    Function func = new Function(lstElement[i].func);
                    int funStart = i, funEnd = -1;
                    int level = 1;
                    Expression subExp;
                    //pass the left branket, to the 1st param
                    //search for , or ), stop at )
                    int paramStart = i + 2, paramEnd = lstElement.Count - 1;
                    for (int j = paramStart; funEnd < 0 && j < lstElement.Count; j++)
                    {
                        switch (lstElement[j].type)
                        {
                            case ElementType.Comma:
                                if (level == 1) // only handle the comma for this function
                                {
                                    paramEnd = j - 1;
                                    // handle the param
                                    subExp = BuildSubExp(paramStart, paramEnd);
                                    func.AddParam(subExp.Result());
                                    paramStart = j + 1;
                                }
                                break;
                            case ElementType.LeftBranket:
                                level++;
                                break;
                            case ElementType.RightBranket:
                                if (--level == 0)
                                {   // the close branket for the function
                                    paramEnd = j - 1;
                                    // handle the param
                                    subExp = BuildSubExp(paramStart, paramEnd);
                                    func.AddParam(subExp.Result());
                                    funEnd = j;
                                }
                                break;
                        }
                    }
                    Replace(funStart, funEnd, func.Result);
                } //if
            } //for
        }

        private Expression BuildSubExp(int start, int end)
        {
            List<Element> subExp = new List<Element>();
            for (int i = start; i <= end; i++)
            {
                subExp.Add(lstElement[i]);
            }
            return new Expression(subExp);
        }

        /// <summary>
        /// Replace the sub element list from start to end, with the given value
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="value"></param>
        private void Replace(int start, int end, double value)
        {
            for (int i = start; i <= end; i++)
            {
                lstElement.RemoveAt(start);
            }
            lstElement.Insert(start, new Element(value));
        }

        //replace all the outest branket pairs with their subexpression value by recursively call
        //e.g. 3-14/(3+4) => 3-14/7
        private void Scan4Branket()
        {
            for (int i = 0; i < lstElement.Count; i++)
            {
                if (lstElement[i].type == ElementType.LeftBranket)
                {
                    //find the match right branket
                    int level = 1, start = i, end = lstElement.Count - 1;
                    for (int j = i + 1; j < lstElement.Count; j++)
                    {
                        if (lstElement[j].type == ElementType.LeftBranket)
                        {
                            level++;
                        } 
                        else if (lstElement[j].type == ElementType.RightBranket) 
                        {
                            level--;
                            if (level == 0)
                            {
                                end = j;
                                break;
                            }
                        }
                    }
                    List<Element> subExp = new List<Element>();
                    for (int j = start; j <= end; j++) 
                    {
                        if (j > start && j < end) // remove the outest branket pair
                        {   
                            subExp.Add(lstElement[start]);
                        }
                        lstElement.RemoveAt(start);
                    }
                    Expression exp = new Expression(subExp);
                    lstElement.Insert(start, new Element(exp.Result()));                    
                }
            }
        }

        private void Scan4Pow()
        {
            for (int i = 0; i < lstElement.Count; i++)
            {
                if (lstElement[i].type == ElementType.Power) // ^
                {
                    int start = i - 1, end = lstElement.Count - 1;
                    for (int j = i + 2; j < lstElement.Count; j += 2)
                    {
                        if (lstElement[j].type != ElementType.Power)
                        {
                            end = j - 1;
                            break;
                        }
                    }
                    double subResult = GetPowerOnlyResult(start, end);
                    // replace the power only sub exp with its result
                    /*for (int j = start; j <= end; j++)
                    {
                        lstElement.RemoveAt(start);
                    }
                    lstElement.Insert(start, new Element(subResult));*/
                    Replace(start, end, subResult);
                    //i = start + 2; //adjust i for next loop
                }
            }
        }

        private double GetPowerOnlyResult(int start, int end)
        {
            double ret = lstElement[start].value;
            ElementType optor;
            for (int i = start + 1; i <= end; i += 2)
            {
                double oprand = lstElement[i + 1].value;
                optor = lstElement[i].type; // should be ^
                ret = System.Math.Pow(ret, oprand);
            }
            return ret;
        }

        //calculate all the * and / operator since they have higher priority than + or -, 
        //so no matter where they are, they should be calculated first. And replaced them by their result
        // e.g. 3-14/7 => 3-2
        private void Scan4MulDiv()
        {
            for (int i = 0; i < lstElement.Count; i++)
            {
                if (lstElement[i].type == ElementType.Multiply || lstElement[i].type == ElementType.Divide) // * or /
                {
                    int start = i - 1, end = lstElement.Count - 1;
                    for (int j = i + 2; j < lstElement.Count; j += 2)
                    {
                        if (lstElement[j].type != ElementType.Multiply && lstElement[j].type != ElementType.Divide)
                        {
                            end = j - 1;
                            break;
                        }
                    }
                    double subResult = GetMulDivOnlyResult(start, end);
                    // replace the muldiv only sub exp with its result
                    /*for (int j = end; j >= start; j--)
                    {
                        lstElement.RemoveAt(j);
                    }
                    lstElement.Insert(start, new Element(subResult));*/
                    Replace(start, end, subResult);
                    //i = start + 2; //adjust i for next loop
                }
            }
        }

        //calculate the subexp with only * and /
        private double GetMulDivOnlyResult(int start, int end)
        {
            double ret = lstElement[start].value;//1.0;
            ElementType optor;// = ElementType.Multiply;
            for (int i = start + 1; i <= end; i += 2)
            {
                double oprand = lstElement[i+1].value;
                optor = lstElement[i].type;
                switch (optor)
                {
                    case ElementType.Multiply:
                        ret *= oprand;
                        break;
                    case ElementType.Divide:
                        ret /= oprand;
                        break;
                }
            }
            return ret;
        }

        //now only +,- operator left in exp, calculate them one by one
        //e.g. 3-2=>1
        private double Scan4AddMinus()
        {
            double ret = 0.0;
            int i = 0;
            ElementType optor = ElementType.Add;
            if (lstElement[0].type == ElementType.Add || lstElement[0].type == ElementType.Minus)
            {
                optor = lstElement[0].type;
                i = 1;
            }
            for (; i < lstElement.Count(); i+=2)
            {
                double oprand = lstElement[i].value;
                switch (optor)
                {
                    case ElementType.Add:
                        ret += oprand;
                        break;
                    case ElementType.Minus:
                        ret -= oprand;
                        break;
                }
                if (i + 1 < lstElement.Count()) optor = lstElement[i + 1].type;
            }
            return ret;
        }

        //build the lstElement
        private void PreScan()
        {
            lstElement = new List<Element>();
            for (int i = 0; i < stExp.Length; i++)
            {
                char ch = stExp[i];
                if (Char.IsDigit(ch))
                {
                    lstElement.Add(new Element(ReadNumber(ref i)));
                }
                else if (Char.IsLetter(ch))
                {
                    lstElement.Add(new Element(ReadFunc(ref i)));
                }
                else
                {
                    switch (ch)
                    {
                        case '+':
                            lstElement.Add(new Element(ElementType.Add));
                            break;
                        case '-':
                            lstElement.Add(new Element(ElementType.Minus));
                            break;
                        case '*':
                            lstElement.Add(new Element(ElementType.Multiply));
                            break;
                        case '/':
                            lstElement.Add(new Element(ElementType.Divide));
                            break;
                        case '^':
                            lstElement.Add(new Element(ElementType.Power));
                            break;
                        case '(':
                            lstElement.Add(new Element(ElementType.LeftBranket));
                            break;
                        case ')':
                            lstElement.Add(new Element(ElementType.RightBranket));
                            break;
                        case ',':
                            lstElement.Add(new Element(ElementType.Comma));
                            break;
                        case ' ':
                            break;
                    }
                }
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < lstElement.Count; i++)
            {
                sb.Append(lstElement[i]);
            }
            return sb.ToString();
        }

        //parse the number
        private double ReadNumber(ref int idx)
        {
            int num = 0;
            double remainder = 0.0;
            while (idx < stExp.Length && Char.IsDigit(stExp[idx]))
            {
                int digit = stExp[idx] - '0';
                num = num * 10 + digit;
                idx++;
            }
            if (idx < stExp.Length && stExp[idx] == '.')
            {
                double radix = 0.1;
                while (++idx < stExp.Length && Char.IsDigit(stExp[idx]))
                {
                    int digit = stExp[idx] - '0';
                    remainder += radix * digit;
                    radix *= 0.1;
                }
            }
            idx--;
            return remainder + num;
        }

        private string ReadFunc(ref int idx)
        {
            StringBuilder func = new StringBuilder();
            while (idx < stExp.Length && Char.IsLetter(stExp[idx]))
            {
                func.Append(stExp[idx]);
                idx++;
            }
            idx--;
            return func.ToString();
        }
    }
}
