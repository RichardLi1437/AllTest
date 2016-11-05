#define TEST

using Expresion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Calculator
{

    class Program
    {
        static void Main(string[] args)
        {
#if(TEST)
            Test();
#else            
            bool cont = true;
            while (cont) 
            {
                System.Console.Write("input expression:");
                string stExp = System.Console.ReadLine();
                Expression exp = new Expression(stExp);
                System.Console.Write("{0} = {1}\nContinue? presss Y or N and then ENTER  ", stExp, exp.Result());
                cont = System.Console.ReadLine().ToUpper() == "Y";
            }
#endif
        }

        static void Test()
        {
            string[] stExp = new string[9] 
            {
                "log(4-2, 8)",
                " - 2.0+3-1.6+ 8.3 ",
                " 2.0*3.5-4.2/3",
                "(2.0-3)*(5/(3+3)-4.2)",
                "4^3^0.5-2^(3^2-2*3)",
                "log(4-2, 8)+lg(123)",
                "log(log(10,100), 8) + 1.2",
                "sin((ln(2)-2)*log(3, 2*3^2))",
                "-min(sin(ln(2.5^1.5+2*0.3)-1.3), cos(0.8^log(2,5)), 0.7^(5/2)) + max(1.8, 2, -2, 2.3, log(5, 118))"
            };
            double[] expectedResult = new double[9] 
            {
                Math.Log(8, 4-2),
                 - 2.0+3-1.6+ 8.3 ,
                 2.0*3.5-4.2/3,
                (2.0-3)*(5.0/(3+3)-4.2),
                Math.Pow(Math.Pow(4,3), 0.5)-Math.Pow(2, (Math.Pow(3,2)-2*3)),
                Math.Log(8, 4-2)+Math.Log10(123),
                Math.Log(8, Math.Log(100,10)) + 1.2,
                Math.Sin((Math.Log(2.0)-2)*Math.Log(2.0*Math.Pow(3.0,2), 3)),
                -Math.Min(Math.Min(Math.Sin(Math.Log(Math.Pow(2.5,1.5)+2.0*0.3)-1.3), Math.Cos(Math.Pow(0.8, Math.Log(5,2)))), Math.Pow(0.7, 5.0/2)) + Math.Max(2.3, Math.Log(118, 5))
            };
            Expression2 exp;
            for (int i = 0; i < stExp.Length; i++)
            {
                exp = new Expression2(stExp[i]);
                double actual = exp.Result();
                Console.WriteLine("{0}\n{1}", stExp[i], exp);
                System.Console.WriteLine("{0}! Expected: {1}, Actual: {2}\n", Math.Abs(actual - expectedResult[i]) < 1e-10 ? "Correct" : "Wrong", expectedResult[i], actual);
            }
        }
    }
}
