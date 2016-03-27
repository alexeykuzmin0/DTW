using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Finance;

namespace GUIComponents
{
    public class CandleStickChart : ZedGraph.ZedGraphControl
    {
        Finance.AbstractCandleTokenizer candles;

        public CandleStickChart()
        {
            GraphPane.Title.Text = "No data loaded";
            GraphPane.XAxis.Title.Text = "";
            GraphPane.YAxis.Title.Text = "";
        }

        public void SetCandles(CandleTokenizer tokenizer)
        {
            candles = tokenizer;
            var points = new ZedGraph.StockPointList();
            for (int i = 0; i < candles.GetLength(); ++i)
            {
                points.Add(new ZedGraph.StockPt(
                    i,
                    candles[i].high,
                    candles[i].low,
                    candles[i].open,
                    candles[i].close,
                    0));
            }
            var curve = GraphPane.AddJapaneseCandleStick("", points);
        }
    }
}
