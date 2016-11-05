using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace MutexTest
{
    class SingleChecker
    {
        public void AlreadyRun()
        {
            alreadyRun();
        }

        private void alreadyRun()
        {
            bool runone;
            Mutex run = new Mutex(true, "richard single app", out runone);
            if (!runone) 
            {
                MessageBox.Show("Already run");
                throw new ApplicationException("already run!");
            }
        }
    }
}
