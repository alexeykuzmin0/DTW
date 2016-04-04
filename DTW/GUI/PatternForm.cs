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
        Finance.AbstractCandleTokenizer candles;

        public PatternForm(Finance.AbstractCandleTokenizer ct)
        {
            InitializeComponent();
            candles = ct;
            candleStickChart1.SetCandles(ct);
            candleStickChart1.GraphPane.Title.IsVisible = false;
            candleStickChart1.IsModifiable = true;
            candleStickChart1.IsShowHScrollBar = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                candles.Save(new System.IO.StreamWriter(saveFileDialog1.FileName));
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                candles = new Finance.CandleTokenizer(new System.IO.StreamReader(openFileDialog1.FileName));
                candleStickChart1.SetCandles(candles);
                candleStickChart1.Invalidate();
            }
        }

        public Finance.AbstractCandleTokenizer GetCandles()
        {
            return candles;
        }
    }
}
