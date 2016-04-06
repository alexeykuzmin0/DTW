using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Finance;
using ZedGraph;

namespace GUIComponents
{
    public class CandleStickChart : ZedGraph.ZedGraphControl
    {
        Finance.AbstractCandleTokenizer candles;
        bool scaling = false;
        double initialX = 0.0;
        double initialY = 0.0;
        bool selecting = false;
        ZedGraph.LineObj[] selectingSegments;

        public bool IsModifiable = false;
        bool modifying = false;
        int modifyingId = 0;
        Finance.Candle.Fields modifyingField;

        public delegate void CandlesSelectedHandler(ZedGraphControl sender, int start, int end);
        public event CandlesSelectedHandler CandlesSelected;

        public CandleStickChart()
        {
            GraphPane.Title.Text = "No data loaded";
            GraphPane.XAxis.Title.Text = "";
            GraphPane.YAxis.Title.Text = "";
            IsShowPointValues = true;
            PointValueEvent += CandleStickChart_PointValueEvent;
            GraphPane.XAxis.MajorTic.IsAllTics = false;
            GraphPane.XAxis.MinorTic.IsAllTics = false;
            GraphPane.XAxis.Scale.IsVisible = false;
            GraphPane.XAxis.Scale.MagAuto = false;
            GraphPane.XAxis.Scale.MaxAuto = false;
            GraphPane.XAxis.Scale.Mag = 0;
            GraphPane.AxisChangeEvent += GraphPane_AxisChangeEvent;
            IsEnableWheelZoom = false;
            MouseDownEvent += CandleStickChart_MouseDownEvent;
            MouseUpEvent += CandleStickChart_MouseUpEvent;
            MouseMoveEvent += CandleStickChart_MouseMoveEvent;
            IsShowHScrollBar = true;
            IsAutoScrollRange = true;
            ScrollProgressEvent += CandleStickChart_ScrollProgressEvent;
            ScrollDoneEvent += CandleStickChart_ScrollDoneEvent;
        }

        private void CandleStickChart_ScrollDoneEvent(ZedGraphControl sender, System.Windows.Forms.ScrollBar scrollBar, ZoomState oldState, ZoomState newState)
        {
            int minId = (int)Math.Ceiling(sender.GraphPane.XAxis.Scale.Min);
            minId = Math.Max(0, Math.Min(candles.GetLength() - 1, minId));
            int maxId = (int)Math.Floor(sender.GraphPane.XAxis.Scale.Max);
            maxId = Math.Max(0, Math.Min(candles.GetLength() - 1, maxId));

            DateTime minTime = candles[minId].timestamp;
            DateTime maxTime = candles[maxId].timestamp;

            ChangeYScale(minTime, maxTime, sender.GraphPane);
            sender.AxisChange();
        }

        private void CandleStickChart_ScrollProgressEvent(ZedGraphControl sender, System.Windows.Forms.ScrollBar scrollBar, ZoomState oldState, ZoomState newState)
        {
            int minId = (int)Math.Ceiling(sender.GraphPane.XAxis.Scale.Min);
            minId = Math.Max(0, Math.Min(candles.GetLength() - 1, minId));
            int maxId = (int)Math.Floor(sender.GraphPane.XAxis.Scale.Max);
            maxId = Math.Max(0, Math.Min(candles.GetLength() - 1, maxId));

            DateTime minTime = candles[minId].timestamp;
            DateTime maxTime = candles[maxId].timestamp;

            ChangeYScale(minTime, maxTime, sender.GraphPane);
            sender.AxisChange();
        }

        private bool CandleStickChart_MouseMoveEvent(ZedGraphControl sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (scaling)
            {
                double x, y;
                sender.GraphPane.ReverseTransform(e.Location, out x, out y);
                var scale = sender.GraphPane.XAxis.Scale;
                scale.Min = scale.Max - (scale.Max - scale.Min) / (scale.Max - x) * (scale.Max - initialX);
                Invalidate();
                int minId = (int)Math.Ceiling(sender.GraphPane.XAxis.Scale.Min);
                minId = Math.Max(0, Math.Min(candles.GetLength() - 1, minId));
                int maxId = (int)Math.Floor(sender.GraphPane.XAxis.Scale.Max);
                maxId = Math.Max(0, Math.Min(candles.GetLength() - 1, maxId));

                DateTime minTime = candles[minId].timestamp;
                DateTime maxTime = candles[maxId].timestamp;

                ChangeYScale(minTime, maxTime, sender.GraphPane);
                sender.AxisChange();
            }
            if (selecting)
            {
                double x, y;
                sender.GraphPane.ReverseTransform(e.Location, out x, out y);
                selectingSegments[0].Location.Rect =
                    new System.Drawing.RectangleF((float)Math.Min(initialX, x), (float)initialY, (float)Math.Abs(x - initialX), 0);
                selectingSegments[1].Location.Rect =
                    new System.Drawing.RectangleF((float)Math.Min(initialX, x), (float)y, (float)Math.Abs(x - initialX), 0);
                selectingSegments[2].Location.Rect =
                    new System.Drawing.RectangleF((float)initialX, (float)Math.Min(initialY, y), 0, (float)Math.Abs(y - initialY));
                selectingSegments[3].Location.Rect =
                    new System.Drawing.RectangleF((float)x, (float)Math.Min(initialY, y), 0, (float)Math.Abs(y - initialY));
                sender.Invalidate();
            }
            if (modifying)
            {
                double x, y;
                sender.GraphPane.ReverseTransform(e.Location, out x, out y);
                if (modifyingField == Candle.Fields.OPEN)
                {
                    candles[modifyingId].open = y;
                    (sender.GraphPane.CurveList[0].Points[modifyingId] as ZedGraph.StockPt).Open = y;
                }
                else if (modifyingField == Candle.Fields.HIGH)
                {
                    candles[modifyingId].high = y;
                    (sender.GraphPane.CurveList[0].Points[modifyingId] as ZedGraph.StockPt).High = y;
                }
                else if (modifyingField == Candle.Fields.LOW)
                {
                    candles[modifyingId].low = y;
                    (sender.GraphPane.CurveList[0].Points[modifyingId] as ZedGraph.StockPt).Low = y;
                }
                else
                {
                    candles[modifyingId].close = y;
                    (sender.GraphPane.CurveList[0].Points[modifyingId] as ZedGraph.StockPt).Close = y;
                }
                sender.Invalidate();
            }
            return false;
        }

        public void SelectCandles(int start, int end)
        {
            double max = double.NegativeInfinity;
            double min = double.PositiveInfinity;
            for (int i = start; i <= end; ++i)
            {
                max = Math.Max(max, candles[i].high);
                min = Math.Min(min, candles[i].low);
            }
            GraphPane.GraphObjList.Add(new ZedGraph.LineObj(start - 0.5, min, start - 0.5, max));
            GraphPane.GraphObjList.Add(new ZedGraph.LineObj(end + 0.5, min, end + 0.5, max));
            GraphPane.GraphObjList.Add(new ZedGraph.LineObj(start - 0.5, min, end + 0.5, min));
            GraphPane.GraphObjList.Add(new ZedGraph.LineObj(start - 0.5, max, end + 0.5, max));
        }

        private bool CandleStickChart_MouseUpEvent(ZedGraphControl sender, System.Windows.Forms.MouseEventArgs e)
        {
            scaling = false;
            if (selecting)
            {
                double x, y;
                sender.GraphPane.ReverseTransform(e.Location, out x, out y);
                OnCandlesSelected((int)initialX + 1, (int)x + 1);
                for (int i = 0; i < 4; ++i)
                {
                    sender.GraphPane.GraphObjList.Remove(selectingSegments[i]);
                    selectingSegments[i] = null;
                }
                sender.Invalidate();
            }
            selecting = false;
            modifying = false;
            return true;
        }

        private bool CandleStickChart_MouseDownEvent(ZedGraphControl sender, System.Windows.Forms.MouseEventArgs e)
        {
            double x, y;
            sender.GraphPane.ReverseTransform(e.Location, out x, out y);
            if (y < sender.GraphPane.YAxis.Scale.Min)
            {
                scaling = true;
                initialX = x;
            }
            else if (IsModifiable)
            {
                modifying = true;
                modifyingId = (int)(x + 0.5);
                Finance.Candle c = candles[modifyingId];
                if (Math.Abs(y - c.open) <= Math.Abs(y - c.high) &&
                    Math.Abs(y - c.open) <= Math.Abs(y - c.low) &&
                    Math.Abs(y - c.open) <= Math.Abs(y - c.close))
                {
                    modifyingField = Candle.Fields.OPEN;
                }
                else if (Math.Abs(y - c.high) <= Math.Abs(y - c.open) &&
                          Math.Abs(y - c.high) <= Math.Abs(y - c.low) &&
                          Math.Abs(y - c.high) <= Math.Abs(y - c.close))
                {
                    modifyingField = Candle.Fields.HIGH;
                }
                else if (Math.Abs(y - c.low) <= Math.Abs(y - c.high) &&
                          Math.Abs(y - c.low) <= Math.Abs(y - c.open) &&
                          Math.Abs(y - c.low) <= Math.Abs(y - c.close))
                {
                    modifyingField = Candle.Fields.LOW;
                }
                else
                {
                    modifyingField = Candle.Fields.CLOSE;
                }
            }
            else
            {
                initialX = x;
                initialY = y;
                selecting = true;
                selectingSegments = new LineObj[4];
                for (int i = 0; i < 4; ++i)
                {
                    selectingSegments[i] = new LineObj(System.Drawing.Color.Black, x, y, x, y);
                    selectingSegments[i].Line.Style = System.Drawing.Drawing2D.DashStyle.Dash;
                    sender.GraphPane.GraphObjList.Add(selectingSegments[i]);
                }
            }
            sender.Invalidate();
            return true;
        }

        private void GraphPane_AxisChangeEvent(ZedGraph.GraphPane pane)
        {
            pane.GraphObjList.Clear();

            int minId = (int)Math.Ceiling(pane.XAxis.Scale.Min);
            minId = Math.Max(0, Math.Min(candles.GetLength() - 1, minId));
            int maxId = (int)Math.Floor(pane.XAxis.Scale.Max);
            maxId = Math.Max(0, Math.Min(candles.GetLength() - 1, maxId));

            DateTime minTime = candles[minId].timestamp;
            DateTime maxTime = candles[maxId].timestamp;

            pane.XAxis.Scale.Mag = 0;
            AddTicks(minTime, maxTime, pane);
        }

        public void ChangeYScale()
        {
            int minId = (int)Math.Ceiling(GraphPane.XAxis.Scale.Min);
            minId = Math.Max(0, Math.Min(candles.GetLength() - 1, minId));
            int maxId = (int)Math.Floor(GraphPane.XAxis.Scale.Max);
            maxId = Math.Max(0, Math.Min(candles.GetLength() - 1, maxId));

            DateTime minTime = candles[minId].timestamp;
            DateTime maxTime = candles[maxId].timestamp;

            ChangeYScale(minTime, maxTime, GraphPane);
        }

        private void ChangeYScale(DateTime minTime, DateTime maxTime, GraphPane pane)
        {
            double xmin = getX(minTime);
            double xmax = getX(maxTime);
            int from = (int)Math.Floor(xmin);
            int to = (int)Math.Ceiling(xmax);

            double ymin = double.PositiveInfinity;
            double ymax = double.NegativeInfinity;
            for (int i = from; i <= to; ++i)
            {
                Candle c = candles[i];
                ymin = Math.Min(ymin, c.low);
                ymax = Math.Max(ymax, c.high);
            }
            pane.YAxis.Scale.Min = ymin - (ymax - ymin) * 0.05;
            pane.YAxis.Scale.Max = ymax + (ymax - ymin) * 0.05;
        }

        private void AddTicks(DateTime minTime, DateTime maxTime, ZedGraph.GraphPane pane)
        {
            if (maxTime - minTime > TimeSpan.FromDays(365 * 2))
            {
                AddYearTicks(minTime, maxTime, pane);
                return;
            }
            if (maxTime - minTime > TimeSpan.FromDays(95))
            {
                AddMonthTicks(minTime, maxTime, pane);
                return;
            }
            if (maxTime - minTime > TimeSpan.FromDays(20))
            {
                AddWeekTicks(minTime, maxTime, pane);
                return;
            }
            if (maxTime - minTime > TimeSpan.FromDays(2))
            {
                AddDayTicks(minTime, maxTime, pane);
                return;
            }
            if (maxTime - minTime > TimeSpan.FromHours(4))
            {
                AddHourTicks(minTime, maxTime, pane);
                return;
            }
            AddMinuteTicks(minTime, maxTime, pane);
        }

        private void AddMinuteTicks(DateTime minTime, DateTime maxTime, GraphPane pane)
        {
            var start = new DateTime(minTime.Year, minTime.Month, minTime.Day, minTime.Hour, 0, 0);
            while (start.AddMinutes(15) < minTime)
            {
                start = start.AddMinutes(15);
            }
            for (var cur = start; cur < maxTime; cur = cur.AddMinutes(15))
            {
                double x = getX(cur);
                if (getX(cur) == getX(cur.AddMinutes(15)))
                {
                    continue;
                }
                var text = new ZedGraph.TextObj(
                    cur.ToString("HH:mm"),
                    x,
                    pane.YAxis.Scale.Min - (pane.YAxis.Scale.Max - pane.YAxis.Scale.Min) * 0.025);
                text.Location.AlignH = ZedGraph.AlignH.Center;
                text.FontSpec.Border.IsVisible = false;
                text.FontSpec.Fill.IsVisible = false;
                pane.GraphObjList.Add(text);
            }
            start = new DateTime(minTime.Year, minTime.Month, minTime.Day, minTime.Hour, 0, 0);
            while (start < minTime)
            {
                start = start.AddMinutes(1);
            }
            for (var cur = start; cur < maxTime; cur = cur.AddMinutes(1))
            {
                double x = getX(cur);
                double size = cur.Minute % 15 == 0 ? 0.025 : 0.01;
                var line = new ZedGraph.LineObj(
                    x,
                    pane.YAxis.Scale.Min - (pane.YAxis.Scale.Max - pane.YAxis.Scale.Min) * size,
                    x,
                    pane.YAxis.Scale.Min + (pane.YAxis.Scale.Max - pane.YAxis.Scale.Min) * size);
                line.Line.Style = System.Drawing.Drawing2D.DashStyle.Solid;
                line.Line.Width = 1f;
                pane.GraphObjList.Add(line);
            }
        }

        private void AddHourTicks(DateTime minTime, DateTime maxTime, GraphPane pane)
        {
            var start = new DateTime(minTime.Year, minTime.Month, minTime.Day, minTime.Hour, 30, 0);
            for (var cur = start; cur < maxTime; cur = cur.AddHours(1))
            {
                double x = (getX(cur.AddMinutes(-30)) + getX(cur.AddMinutes(30))) / 2;
                if (getX(cur.AddMinutes(-30)) == getX(cur.AddMinutes(30)))
                {
                    continue;
                }
                var text = new ZedGraph.TextObj(
                    cur.ToString("HH"),
                    x,
                    pane.YAxis.Scale.Min - (pane.YAxis.Scale.Max - pane.YAxis.Scale.Min) * 0.025);
                text.Location.AlignH = ZedGraph.AlignH.Center;
                text.FontSpec.Border.IsVisible = false;
                text.FontSpec.Fill.IsVisible = false;
                pane.GraphObjList.Add(text);
            }
            start = new DateTime(minTime.Year, minTime.Month, minTime.Day, minTime.Hour, 0, 0);
            while (start < minTime)
            {
                start = start.AddMinutes(15);
            }
            for (var cur = start; cur < maxTime; cur = cur.AddMinutes(15))
            {
                double x = getX(cur);
                double size = cur.Minute == 0 ? 0.025 : 0.01;
                var line = new ZedGraph.LineObj(
                    x,
                    pane.YAxis.Scale.Min - (pane.YAxis.Scale.Max - pane.YAxis.Scale.Min) * size,
                    x,
                    pane.YAxis.Scale.Min + (pane.YAxis.Scale.Max - pane.YAxis.Scale.Min) * size);
                line.Line.Style = System.Drawing.Drawing2D.DashStyle.Solid;
                line.Line.Width = 1f;
                pane.GraphObjList.Add(line);
            }
        }

        private void AddDayTicks(DateTime minTime, DateTime maxTime, GraphPane pane)
        {
            var start = new DateTime(minTime.Year, minTime.Month, minTime.Day, 15, 0, 0).AddDays(1);
            for (var cur = start; cur < maxTime; cur = cur.AddDays(1))
            {
                double x = (getX(cur.AddHours(-15)) + getX(cur.AddHours(9))) / 2;
                if (getX(cur.AddHours(-15)) == getX(cur.AddHours(9)))
                {
                    continue;
                }
                var text = new ZedGraph.TextObj(
                    cur.ToString("dd"),
                    x,
                    pane.YAxis.Scale.Min - (pane.YAxis.Scale.Max - pane.YAxis.Scale.Min) * 0.025);
                text.Location.AlignH = ZedGraph.AlignH.Center;
                text.FontSpec.Border.IsVisible = false;
                text.FontSpec.Fill.IsVisible = false;
                pane.GraphObjList.Add(text);
            }
            start = new DateTime(minTime.Year, minTime.Month, minTime.Day, minTime.Hour, 0, 0).AddHours(1);
            for (var cur = start; cur < maxTime; cur = cur.AddHours(1))
            {
                double x = getX(cur);
                double size = (getX(cur.AddHours(-2)) == getX(cur.AddHours(-1)) && x != getX(cur.AddHours(-1))) ? 0.025 : 0.01;
                var line = new ZedGraph.LineObj(
                    x,
                    pane.YAxis.Scale.Min - (pane.YAxis.Scale.Max - pane.YAxis.Scale.Min) * size,
                    x,
                    pane.YAxis.Scale.Min + (pane.YAxis.Scale.Max - pane.YAxis.Scale.Min) * size);
                line.Line.Style = System.Drawing.Drawing2D.DashStyle.Solid;
                line.Line.Width = 1f;
                pane.GraphObjList.Add(line);
            }
        }

        private void AddWeekTicks(DateTime minTime, DateTime maxTime, ZedGraph.GraphPane pane)
        {
            var start = new DateTime(minTime.Year, minTime.Month, minTime.Day);
            while (start < minTime || start.DayOfWeek != DayOfWeek.Thursday)
            {
                start = start.AddDays(1);
            }
            for (var cur = start; cur < maxTime; cur = cur.AddDays(7))
            {
                double x = (getX(cur.AddDays(-3)) + getX(cur.AddDays(4))) / 2;
                var text = new ZedGraph.TextObj(
                    cur.ToString("dd.MM"),
                    x,
                    pane.YAxis.Scale.Min - (pane.YAxis.Scale.Max - pane.YAxis.Scale.Min) * 0.025);
                text.Location.AlignH = ZedGraph.AlignH.Center;
                text.FontSpec.Border.IsVisible = false;
                text.FontSpec.Fill.IsVisible = false;
                pane.GraphObjList.Add(text);
            }
            start = new DateTime(minTime.Year, minTime.Month, minTime.Day).AddDays(1);
            for (var cur = start; cur < maxTime; cur = cur.AddDays(1))
            {
                double x = getX(cur);
                double size = cur.DayOfWeek == DayOfWeek.Monday ? 0.025 : 0.01;
                var line = new ZedGraph.LineObj(
                    x,
                    pane.YAxis.Scale.Min - (pane.YAxis.Scale.Max - pane.YAxis.Scale.Min) * size,
                    x,
                    pane.YAxis.Scale.Min + (pane.YAxis.Scale.Max - pane.YAxis.Scale.Min) * size);
                line.Line.Style = System.Drawing.Drawing2D.DashStyle.Solid;
                line.Line.Width = 1f;
                pane.GraphObjList.Add(line);
            }
        }

        private void AddMonthTicks(DateTime minTime, DateTime maxTime, ZedGraph.GraphPane pane)
        {
            var start = new DateTime(minTime.Year, (minTime.Month + (minTime.Day > 15 ? 1 : 0) - 1) % 12 + 1, 15);
            for (var cur = start; cur < maxTime;
                cur = new DateTime(cur.Year + cur.Month / 12, cur.Month % 12 + 1, 15))
            {
                double x = getX(cur);
                var text = new ZedGraph.TextObj(
                    cur.ToString("MMM"),
                    x,
                    pane.YAxis.Scale.Min - (pane.YAxis.Scale.Max - pane.YAxis.Scale.Min) * 0.025);
                text.Location.AlignH = ZedGraph.AlignH.Center;
                text.FontSpec.Border.IsVisible = false;
                text.FontSpec.Fill.IsVisible = false;
                pane.GraphObjList.Add(text);
            }
            start = new DateTime(minTime.Year, minTime.Month, 1);
            var next = new DateTime(start.Year + start.Month / 12, start.Month % 12 + 1, 1);
            for (var cur = start; cur < maxTime.AddDays(40); cur = next)
            {
                next = new DateTime(next.Year + next.Month / 12, next.Month % 12 + 1, 1);
                for (int i = 0; i < 4; ++i)
                {
                    DateTime c = new DateTime((cur.Ticks * (4 - i) + next.Ticks * i) / 4);
                    if (c > maxTime || c < minTime)
                    {
                        continue;
                    }
                    double x = getX(c);
                    double size = i == 0 ? 0.025 : 0.01;
                    var line = new ZedGraph.LineObj(
                        x,
                        pane.YAxis.Scale.Min - (pane.YAxis.Scale.Max - pane.YAxis.Scale.Min) * size,
                        x,
                        pane.YAxis.Scale.Min + (pane.YAxis.Scale.Max - pane.YAxis.Scale.Min) * size);
                    line.Line.Style = System.Drawing.Drawing2D.DashStyle.Solid;
                    line.Line.Width = 1f;
                    pane.GraphObjList.Add(line);
                }
            }
        }

        private void AddYearTicks(DateTime minTime, DateTime maxTime, ZedGraph.GraphPane pane)
        {
            var start = new DateTime(minTime.Year + (minTime.Month > 6 ? 1 : 0), 7, 1);
            for (var cur = start; cur < maxTime; cur = new DateTime(cur.Year + 1, 7, 1))
            {
                double x = getX(cur);
                var text = new ZedGraph.TextObj(
                    cur.ToString("yyyy"),
                    x,
                    pane.YAxis.Scale.Min - (pane.YAxis.Scale.Max - pane.YAxis.Scale.Min) * 0.025);
                text.Location.AlignH = ZedGraph.AlignH.Center;
                text.FontSpec.Border.IsVisible = false;
                text.FontSpec.Fill.IsVisible = false;
                pane.GraphObjList.Add(text);
            }
            start = new DateTime(
                minTime.Year + (minTime.Month == 12 ? 1 : 0), minTime.Month % 12 + 1, 1);
            for(var cur = start; cur < maxTime; 
                cur = new DateTime(cur.Year + (cur.Month == 12 ? 1 : 0), cur.Month % 12 + 1, 1))
            {
                double x = getX(cur);
                double size = cur.Month == 1 ? 0.025 : 0.01;
                var line = new ZedGraph.LineObj(
                    x,
                    pane.YAxis.Scale.Min - (pane.YAxis.Scale.Max - pane.YAxis.Scale.Min) * size,
                    x,
                    pane.YAxis.Scale.Min + (pane.YAxis.Scale.Max - pane.YAxis.Scale.Min) * size);
                line.Line.Style = System.Drawing.Drawing2D.DashStyle.Solid;
                line.Line.Width = 1f;
                pane.GraphObjList.Add(line);
            }
        }

        private double getX(DateTime cur)
        {
            if (candles.GetLength() == 0)
            {
                return 0;
            }
            if (cur < candles[0].timestamp)
            {
                return 0;
            }
            if (cur >= candles[candles.GetLength() - 1].timestamp)
            {
                return candles.GetLength() - 1;
            }
            int l = 0;
            int r = candles.GetLength();
            while (r - l > 1)
            {
                int m = (l + r) / 2;
                if (candles[m].timestamp > cur)
                {
                    r = m;
                }
                else
                {
                    l = m;
                }
            }
            return (l * (candles[r].timestamp - cur).Ticks + r * (cur - candles[l].timestamp).Ticks)
                / (candles[r].timestamp - candles[l].timestamp).Ticks;
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
            GraphPane.XAxis.Scale.Min = -0.75;
            GraphPane.XAxis.Scale.Max = candles.GetLength();
            int minId = (int)Math.Ceiling(GraphPane.XAxis.Scale.Min);
            minId = Math.Max(0, Math.Min(candles.GetLength() - 1, minId));
            int maxId = (int)Math.Floor(GraphPane.XAxis.Scale.Max);
            maxId = Math.Max(0, Math.Min(candles.GetLength() - 1, maxId));

            DateTime minTime = candles[minId].timestamp;
            DateTime maxTime = candles[maxId].timestamp;
            ChangeYScale(minTime, maxTime, GraphPane);
            AxisChange();
        }

        private void OnCandlesSelected(int begin, int end)
        {
            if (CandlesSelected != null)
            {
                CandlesSelected(this, Math.Min(begin, end), Math.Max(begin, end));
            }
        }
    }
}
