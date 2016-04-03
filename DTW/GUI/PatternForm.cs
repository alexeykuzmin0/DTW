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
    public partial class PatternForm : Form
    {
        public PatternForm()
        {
            InitializeComponent();
            candleStickChart1.GraphPane.Title.IsVisible = false;
            candleStickChart1.IsModifiable = true;
        }
    }
}
