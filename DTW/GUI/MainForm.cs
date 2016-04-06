using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Finance;

namespace GUI
{
    public partial class MainForm : Form
    {
        Finance.AbstractCandleTokenizer ct;
        List<Tuple<int, int, double>> results;

        public MainForm()
        {
            InitializeComponent();
        }

        private async void openDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                toolStripStatusLabel1.Text = "Loading...";
                if (ct == null)
                {
                    ct = await Finance.CandleTokenizer.CreateAsync(
                        new System.IO.StreamReader(openFileDialog1.FileName));
                }
                else
                {
                    var ct2 = await Finance.CandleTokenizer.CreateAsync(
                        new System.IO.StreamReader(openFileDialog1.FileName));
                    ct = new Finance.DisjointMergeCandleTokenizer(ct, ct2);
                }
                candleStickChart1.SetCandles(ct);
                candleStickChart1.Invalidate();
                toolStripStatusLabel1.Text = "Loaded " + openFileDialog1.FileName;
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private async void saveDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                toolStripStatusLabel1.Text = "Saving...";
                await ct.SaveAsync(new System.IO.StreamWriter(saveFileDialog1.FileName));
                toolStripStatusLabel1.Text = "Saved " + saveFileDialog1.FileName;
            }
        }

        private void clearDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ct = null;
            candleStickChart1.SetCandles(ct);
            candleStickChart1.Invalidate();
        }

        private void changePeriodToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var getPeriod = new GetInterval();
            if (getPeriod.ShowDialog() == DialogResult.OK)
            {
                ct = new Finance.CandleTokenizer(ct, getPeriod.dateTimePicker1.Value.TimeOfDay);
                candleStickChart1.SetCandles(ct);
                candleStickChart1.Invalidate();
            }
        }

        private void candleStickChart1_CandlesSelected(ZedGraph.ZedGraphControl sender, int start, int end)
        {
            var pc = new Finance.PartialCandleTokenizer(ct, start, end - start);
            var pf = new PatternForm(pc);
            if (pf.ShowDialog() == DialogResult.OK)
            {
                Match(ct, pf.GetCandles());
            }
        }

        private void openPatternWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var pf = new PatternForm(null);
            if (pf.ShowDialog() == DialogResult.OK)
            {
                Match(ct, pf.GetCandles());
            }
        }

        private void Match(AbstractCandleTokenizer candles, AbstractCandleTokenizer pattern)
        {
            var dtw = new Core.DTW<Candle>(pattern);
            List<Tuple<int, double>> results = new List<Tuple<int, double>>();
            for (int i = 0; i < candles.GetLength(); ++i)
            {
                dtw.Process(candles[i]);
                results.Add(new Tuple<int, double>(i, dtw.GetResult()));
            }
            this.results = new List<Tuple<int, int, double>>();
            for (int i = 0; i < 20; ++i)
            {
                results.Sort((Tuple<int, double> lhs, Tuple<int, double> rhs) =>
                {
                    return lhs.Item2.CompareTo(rhs.Item2);
                });
                int end = results[0].Item1;
                int start = Math.Max(0, end - pattern.GetLength());
                this.results.Add(new Tuple<int, int, double>(start, end, results[0].Item2));
                listBox1.Items.Add(
                    "Start: " + candles[start].timestamp.ToString("dd.MM.yyyy HH:mm") +
                    "\tEnd: " + candles[end].timestamp.ToString("dd.MM.yyyy HH:mm") +
                    "\tDistance: " + results[0].Item2.ToString());
                results.RemoveAll((Tuple<int, double> item) =>
                {
                    return item.Item1 >= start && item.Item1 <= end + pattern.GetLength();
                });
            }
        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int selected = listBox1.SelectedIndex;
            if (selected < 0 || selected >= results.Count)
            {
                return;
            }
            int start = results[selected].Item1;
            int end = results[selected].Item2;
            double center = (start + end) / 2;
            double min = candleStickChart1.GraphPane.XAxis.Scale.Min;
            double max = candleStickChart1.GraphPane.XAxis.Scale.Max;
            candleStickChart1.GraphPane.XAxis.Scale.Min += center - (min + max) / 2;
            candleStickChart1.GraphPane.XAxis.Scale.Max += center - (min + max) / 2;
            candleStickChart1.ChangeYScale();
            candleStickChart1.AxisChange();
            candleStickChart1.Invalidate();
        }
    }
}
