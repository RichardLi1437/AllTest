using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Calculator
{
    enum ElementType
    {
        Operand, Add, Minus, Multiply, Divide, Power, LeftBranket, RightBranket, Function, Comma
    }

    class Element
    {
        public ElementType type;
        public readonly double value;
        public readonly string func;
        private static readonly Dictionary<ElementType, string> mapElement;

        static Element()
        {
            mapElement = new Dictionary<ElementType, string>();
            mapElement.Add(ElementType.Add, "+");
            mapElement.Add(ElementType.Comma, ",");
            mapElement.Add(ElementType.Divide, "/");
            mapElement.Add(ElementType.LeftBranket, "(");
            mapElement.Add(ElementType.Minus, "-");
            mapElement.Add(ElementType.Multiply, "*");
            mapElement.Add(ElementType.Power, "^");
            mapElement.Add(ElementType.RightBranket, ")");
        }

        public Element()
        {
        }

        public Element(ElementType type)
        {
            this.type = type;
        }

        public Element(double value)
        {
            type = ElementType.Operand;
            this.value = value;
        }

        public Element(string func)
        {
            type = ElementType.Function;
            this.func = func;
        }

        public override string ToString()
        {
            switch (type)
            {
                case ElementType.Operand:
                    return String.Format("{0}", value);
                case ElementType.Function:
                    return func;
                default:
                    return mapElement[type];
            }
        }
    }
}
