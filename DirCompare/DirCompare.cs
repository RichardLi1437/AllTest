using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirCompare
{
    public class DirCompare
    {
        private string dir1, dir2;
        public DirCompare(string Dir1, string Dir2)
        {
            this.dir1 = Dir1;
            this.dir2 = Dir2;
            dir1.TrimEnd('\\');
            dir2.TrimEnd('\\');
        }

        public void Compare()
        {
            var files = Directory.EnumerateFiles(dir1, "*.*", SearchOption.AllDirectories);
            foreach (string f1 in files)
            {
                string fn = f1.Substring(dir1.Length);
                string f2 = dir2 + fn;
                if (!File.Exists(f2))
                {
                    Console.WriteLine("{0} only exists in {1}", fn, dir1);
                    continue;
                }

                StreamReader r1 = new StreamReader(File.Open(f1, FileMode.Open));
                string txt1 = r1.ReadToEnd();
                StreamReader r2 = new StreamReader(File.Open(f2, FileMode.Open));
                string txt2 = r2.ReadToEnd();
                r1.Close();
                r2.Close();
                if (!txt1.Equals(txt2))
                {
                    Console.WriteLine("{0} content is different.", fn);
                }
            }
            files = Directory.EnumerateFiles(dir2, "*.*", SearchOption.AllDirectories);
            foreach (string f2 in files)
            {
                string fn = f2.Substring(dir2.Length);
                string f1 = dir1 + fn;
                if (!File.Exists(f1))
                    Console.WriteLine("{0} only exists in {1}", fn, dir2);
            }
        }

        async public Task CompareAsync()
        {
            var files = Directory.EnumerateFiles(dir1, "*.*", SearchOption.AllDirectories);
            foreach (string f1 in files)
            {
                string fn = f1.Substring(dir1.Length);
                string f2 = dir2 + fn;
                if (!File.Exists(f2))
                {
                    Console.WriteLine("{0} only exists in {1}", fn, dir1);
                    continue;
                }

                StreamReader r1 = new StreamReader(File.Open(f1, FileMode.Open));
                string txt1 = await r1.ReadToEndAsync();
                StreamReader r2 = new StreamReader(File.Open(f2, FileMode.Open));
                string txt2 = await r2.ReadToEndAsync();
                r1.Close();
                r2.Close();
                if (!txt1.Equals(txt2))
                {
                    Console.WriteLine("{0} content is different.", fn);
                }
            }
            files = Directory.EnumerateFiles(dir2, "*.*", SearchOption.AllDirectories);
            foreach (string f2 in files)
            {
                string fn = f2.Substring(dir2.Length);
                string f1 = dir1 + fn;
                if (!File.Exists(f1))
                    Console.WriteLine("{0} only exists in {1}", fn, dir2);
            }
        }
    }
}
