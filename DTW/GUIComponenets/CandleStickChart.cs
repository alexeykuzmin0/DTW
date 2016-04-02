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
            IsShowPointValues = true;
            PointValueEvent += CandleStickChart_PointValueEvent;
        }

        private string CandleStickChart_PointValueEvent(ZedGraph.ZedGraphControl sender, ZedGraph.GraphPane pane, ZedGraph.CurveItem curve, int iPt)
        {
            return candles[iPt].timestamp.ToString("dd.MM.yyyy HH:mm") + "\n" +
                "Open:\t" + candles[iPt].open.ToString() + "\n" +
                "High:\t" + candles[iPt].high.ToString() + "\n" +
                "Low:\t" + candles[iPt].low.ToString() + "\n" +
                "Close:\t" + candles[iPt].close.ToString();
        }

        private string CreatePeriodString(TimeSpan period)
        {
            if (period.Days > 0)
            {
                return period.Days.ToString() + " day" + (period.Days > 1 ? "s" : "");
            }
            if (period.Hours > 0)
            {
                return period.Hours.ToString() + " hour" + (period.Hours > 1 ? "s" : "");
            }
            return period.Minutes.ToString() + " minute" + (period.Minutes > 1 ? "s" : "");
        }

        public void SetCandles(AbstractCandleTokenizer tokenizer)
        {
            GraphPane.CurveList.Clear();
            candles = tokenizer;
            if (tokenizer == null)
            {
                GraphPane.Title.Text = "No data loaded";
                return;
            }
            GraphPane.Title.Text = tokenizer.GetTicker() + " " + 
                CreatePeriodString(tokenizer.GetPeriod());
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
            AxisChange();
        }
    }
}
