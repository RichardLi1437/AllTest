using System;
//using System.Collections.Generic;
using System.IO;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
using VcClient;

namespace VcClientTest
{
    class VcClientTest
    {
        static void Main(string[] args)
        {
            string url = "https://cosmos09.osdinfra.net/cosmos/searchDM/local/projects/MasterData/DiscoveredDimensions/2014/06/Browser_2014_06_01.txt";
            VC.SetDefaultCredentials();
            using (StreamReader reader = new StreamReader(VC.ReadStream(url, false)))
            {
                string line, id, name;
                string[] cells;
                while ((line = reader.ReadLine()) != null)
                {
                    cells = line.Split('\t');
                    id = cells[0]; name = cells[1];
                    Console.WriteLine("{0}\t{1}", id, name);
                }
            }
        }
    }
}
