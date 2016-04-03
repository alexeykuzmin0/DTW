using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance
{
    public class Candle
    {
        public enum Fields
        {
            OPEN,
            HIGH,
            LOW,
            CLOSE
        }

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

        public Candle(DateTime timestamp, double open, double high, double low, double close)
        {
            this.timestamp = timestamp;
            this.open = open;
            this.high = high;
            this.low = low;
            this.close = close;
        }

        public static bool operator == (Candle lhs, Candle rhs)
        {
            if ((object)lhs == null)
            {
                return (object)rhs == null;
            }
            if ((object)rhs == null)
            {
                return false;
            }
            return (lhs.timestamp == rhs.timestamp) &&
                (lhs.open == rhs.open) &&
                (lhs.high == rhs.high) &&
                (lhs.low == rhs.low) &&
                (lhs.close == rhs.close);
        }

        public static bool operator != (Candle lhs, Candle rhs)
        {
            return !(lhs == rhs);
        }

        public override bool Equals(object o)
        {
            try
            {
                var rhs = (Candle)o;
                if (rhs == null)
                {
                    return false;
                }
                return this == rhs;
            }
            catch (System.InvalidCastException)
            {
                return false;
            }
            catch
            {
                throw;
            }
        }

        public override int GetHashCode()
        {
            int ans = timestamp.GetHashCode();
            const int BASE = 239017;
            unchecked
            {
                ans = ans * BASE + open.GetHashCode();
                ans = ans * BASE + high.GetHashCode();
                ans = ans * BASE + low.GetHashCode();
                ans = ans * BASE + close.GetHashCode();
            }
            return ans;
        }

        public override string ToString()
        {
            var provider = new System.Globalization.NumberFormatInfo() { NumberDecimalSeparator = "." };
            return timestamp.ToString("yyyyMMdd") + ";" +
                timestamp.ToString("HHmmss") + ";" +
                open.ToString(provider) + ";" +
                high.ToString(provider) + ";" +
                low.ToString(provider) + ";" +
                close.ToString(provider);
        }
    }
}
