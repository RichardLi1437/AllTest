using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace WindowsThreadTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(ShowMessage);
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
        }

        private void ShowMessage()
        {            
            SaveFileDialog saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            saveFileDialog.Title = "Save Excel As";
            saveFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx | All files (*.*)|*.*";
            saveFileDialog.FilterIndex = 1;
            saveFileDialog.ShowDialog();
            Thread.Sleep(3000);
            MessageBox.Show("finished!");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("richard");
        }
    }
}
