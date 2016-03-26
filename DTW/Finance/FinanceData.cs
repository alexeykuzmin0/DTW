using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance
{
    public class Candle
    {
        public DateTime timestamp;
        public double open, high, low, close;

        public Candle(Dictionary<string, int> captions, string[] values)
        {
            string date = values[captions["<DATE>"]];
            string time = values[captions["<TIME>"]];
            timestamp = DateTime.ParseExact(
                date + ';' + time,
                "yyyyMMdd;HHmmss",
                new System.Globalization.DateTimeFormatInfo());
            var provider = new System.Globalization.NumberFormatInfo() { NumberDecimalSeparator = "." };
            open = Convert.ToDouble(values[captions["<OPEN>"]], provider);
            high = Convert.ToDouble(values[captions["<HIGH>"]], provider);
            low = Convert.ToDouble(values[captions["<LOW>"]], provider);
            close = Convert.ToDouble(values[captions["<CLOSE>"]], provider);
        }

        public Candle(Candle rhs)
        {
            timestamp = rhs.timestamp;
            open = rhs.open;
            high = rhs.high;
            low = rhs.low;
            close = rhs.close;
        }
    }
}
