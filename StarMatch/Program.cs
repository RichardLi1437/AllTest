using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarMatch
{
    class Program
    {
        static void Main(string[] args)
        {
            TextReader inReader, outReader;
            string actualResult, expectedResult;
            Stopwatch sw = new Stopwatch();
            sw.Start();
            for (int j = 1; j <= 10; j++)
            {
                string inFile = string.Format("match\\{0}.in", j);
                string outFile = string.Format("match\\{0}.out", j);
                inReader = File.OpenText(inFile);
                outReader = File.OpenText(outFile);
                string source = inReader.ReadLine();
                StringMatch sMatch = new StringMatch(source, "");
                StarQMatch sqMatch = new StarQMatch();
                sqMatch.Src = source;
                for (int i = 1; i <= 10; i++)
                { 
                    //sMatch.Pattern = inReader.ReadLine();
                    sqMatch.Pattern = inReader.ReadLine();
                    expectedResult = outReader.ReadLine();
                    //actualResult = sMatch.FullMatch() ? "YES" : "NO";
                    actualResult = sqMatch.Match2() ? "YES" : "NO";
                    Console.Write("{0}.{1}\t", j, i);
                    if (actualResult == expectedResult)
                        Console.WriteLine("PASS");
                    else
                        Console.WriteLine("{0}/{1}, expected:{2}, actual:{3}", sqMatch.Src, sqMatch.Pattern, expectedResult, actualResult);
                }
                inReader.Close();
                outReader.Close();
            }
            sw.Stop();
            Console.WriteLine("run for {0} ms", sw.ElapsedMilliseconds);
        }
    }
}
