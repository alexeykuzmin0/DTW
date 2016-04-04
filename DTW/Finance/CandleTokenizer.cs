using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance
{
    public abstract class AbstractCandleTokenizer : IEnumerable<Candle>
    {
        protected string ticker;
        protected TimeSpan period;

        public abstract int GetLength();

        public abstract Candle this[int index]
        {
            get;
        }

        public void Save(StreamWriter streamWriter)
        {
            streamWriter.WriteLine("<TICKER>;<PER>;<DATE>;<TIME>;<OPEN>;<HIGH>;<LOW>;<CLOSE>");
            for (int i = 0; i < GetLength(); ++i)
            {
                streamWriter.Write(GetTicker() + ";");
                streamWriter.Write(GetPeriod().TotalMinutes.ToString() + ";");
                streamWriter.WriteLine(this[i].ToString());
            }
            streamWriter.Flush();
        }

        public Task SaveAsync(StreamWriter sw)
        {
            return Task.Run(() => Save(sw));
        }

        public string GetTicker()
        {
            return ticker;
        }

        public TimeSpan GetPeriod()
        {
            return period;
        }

        public IEnumerator<Candle> GetEnumerator()
        {
            return new Enumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        class Enumerator : IEnumerator<Candle>
        {
            int index;
            AbstractCandleTokenizer candles;

            public Enumerator(AbstractCandleTokenizer candles)
            {
                index = 0;
                this.candles = candles;
            }

            public Candle Current
            {
                get
                {
                    return candles[index];
                }
            }

            object IEnumerator.Current
            {
                get
                {
                    return Current;
                }
            }

            public void Dispose()
            {
            }

            public bool MoveNext()
            {
                ++index;
                return index < candles.GetLength();
            }

            public void Reset()
            {
                index = 0;
            }
        }
    }

    public class CandleTokenizer : AbstractCandleTokenizer
    {
        List<Candle> candles;

        public CandleTokenizer(System.IO.StreamReader sr)
        {
            string[] captions = sr.ReadLine().Split(';');
            Dictionary<string, int> ids = new Dictionary<string, int>();
            for (int i = 0; i < captions.Count(); ++i)
            {
                ids[captions[i]] = i;
            }
            CheckCaptions(ids);
            candles = new List<Candle>();
            while (!sr.EndOfStream)
            {
                string[] line = sr.ReadLine().Split(';');
                if (ticker == null && ids.ContainsKey("<TICKER>"))
                {
                    ticker = line[ids["<TICKER>"]];
                }
                if (period == new TimeSpan() && ids.ContainsKey("<PER>"))
                {
                    period = TimeSpan.FromMinutes(Convert.ToInt32(line[ids["<PER>"]]));
                }
                candles.Add(new Candle(ids, line));
            }
            sr.Close();
        }

        public CandleTokenizer(AbstractCandleTokenizer ct, TimeSpan interval)
        {
            if (interval.Ticks % ct.GetPeriod().Ticks != 0)
            {
                throw new ArgumentException("New interval is not divided by old interval");
            }
            if (TimeSpan.FromDays(1).Ticks % interval.Ticks != 0)
            {
                throw new ArgumentException("24 hours are not divided by new interval");
            }
            period = interval;
            ticker = ct.GetTicker();
            candles = new List<Candle>();
            if (ct.GetLength() == 0)
            {
                return;
            }
            DateTime prev = new DateTime(ct[0].timestamp.Ticks / interval.Ticks * interval.Ticks);
            Candle candle = new Candle(prev, ct[0].open, ct[0].high, ct[0].low, ct[0].close);
            for (int i = 0; i < ct.GetLength(); ++i)
            {
                DateTime cur = new DateTime(ct[i].timestamp.Ticks / interval.Ticks * interval.Ticks);
                if (cur != prev)
                {
                    candles.Add(candle);
                    candle = new Candle(cur, ct[i].open, ct[i].high, ct[i].low, ct[i].close);
                    prev = cur;
                }
                else
                {
                    candle.close = ct[i].close;
                    candle.high = Math.Max(candle.high, ct[i].high);
                    candle.low = Math.Min(candle.low, ct[i].low);
                }
            }
            candles.Add(candle);
        }

        public static Task<CandleTokenizer> CreateAsync(System.IO.StreamReader sr)
        {
            return Task<CandleTokenizer>.Run(() => { return new CandleTokenizer(sr); });
        }

        private static void CheckCaptions(Dictionary<string, int> ids)
        {
            if (!ids.ContainsKey("<DATE>") ||
                !ids.ContainsKey("<TIME>") ||
                !ids.ContainsKey("<OPEN>") ||
                !ids.ContainsKey("<HIGH>") ||
                !ids.ContainsKey("<LOW>") ||
                !ids.ContainsKey("<CLOSE>"))
            {
                throw new FormatException("CSV file contains not all required columns");
            }
        }

        public override int GetLength()
        {
            return candles.Count;
        }

        public override Candle this[int index]
        {
            get
            {
                return candles[index];
            }
        }
    }
}
