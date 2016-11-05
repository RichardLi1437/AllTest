using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DirCompare
{
    class Program
    {
        static void Main(string[] args)
        {
            string dir1 = args[0];
            string dir2 = args[1];
            DirCompare dc = new DirCompare(dir1, dir2);
            Task t = dc.CompareAsync();
            t.Wait();
        }

        static async Task ReadCharacters()
        {
            String result;
            using (StreamReader reader = File.OpenText(@"D:\Projects\COGS\COGS_DB_Update\COGS_DB\COGS_DB\COGS_New\COGS\dbo\Functions\ufn_GenerateFiscalMonthlyRevenue.sql"))
            {
                Console.WriteLine("Opened file.");
                result = await reader.ReadToEndAsync();
                Console.WriteLine("Contains: " + result);
            }
        }
    }
}
