using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance
{
    public abstract class AbstractCandleTokenizer
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
