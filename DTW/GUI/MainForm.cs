using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class MainForm : Form
    {
        Finance.CandleTokenizer ct;
        public MainForm()
        {
            InitializeComponent();
        }

        private void openDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                ct = new Finance.CandleTokenizer(
                    new System.IO.StreamReader(openFileDialog1.FileName));
                toolStripStatusLabel1.Text = "Loaded " + openFileDialog1.FileName;
            }
        }
    }
}
