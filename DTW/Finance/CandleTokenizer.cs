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
        public abstract int GetLength();

        public abstract Candle this[int index]
        {
            get;
        }

        public void Save(StreamWriter streamWriter)
        {
            streamWriter.WriteLine("<DATE>;<TIME>;<OPEN>;<HIGH>;<LOW>;<CLOSE>");
            for (int i = 0; i < GetLength(); ++i)
            {
                streamWriter.WriteLine(this[i].ToString());
            }
            streamWriter.Flush();
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
                candles.Add(new Candle(ids, line));
            }
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
