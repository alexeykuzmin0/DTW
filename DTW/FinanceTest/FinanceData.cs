using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FinanceTest
{
    [TestClass]
    public class Candle
    {
        [TestMethod]
        public void Constructor()
        {
            var captions = new System.Collections.Generic.Dictionary<string, int>();
            captions["<DATE>"] = 0;
            captions["<TIME>"] = 1;
            captions["<OPEN>"] = 2;
            captions["<HIGH>"] = 3;
            captions["<LOW>"] = 4;
            captions["<CLOSE>"] = 5;
            string[] values = "20150105;100100;54.0300000;54.4000000;53.6100000;53.9900000;784480".Split(';');
            Finance.Candle c = new Finance.Candle(captions, values);
            Assert.AreEqual(new DateTime(2015, 1, 5, 10, 1, 0), c.timestamp);
            Assert.AreEqual(54.03, c.open);
            Assert.AreEqual(54.4, c.high);
            Assert.AreEqual(53.61, c.low);
            Assert.AreEqual(53.99, c.close);
        }

        [TestMethod]
        public void FieldByFieldConstructor()
        {
            Finance.Candle c = new Finance.Candle(new DateTime(2015, 1, 5, 10, 1, 0), 54.03, 54.4, 53.61, 53.99);
            Assert.AreEqual(new DateTime(2015, 1, 5, 10, 1, 0), c.timestamp);
            Assert.AreEqual(54.03, c.open);
            Assert.AreEqual(54.4, c.high);
            Assert.AreEqual(53.61, c.low);
            Assert.AreEqual(53.99, c.close);
        }

        [TestMethod]
        public void CopyConstructor()
        {
            var captions = new System.Collections.Generic.Dictionary<string, int>();
            captions["<DATE>"] = 0;
            captions["<TIME>"] = 1;
            captions["<OPEN>"] = 2;
            captions["<HIGH>"] = 3;
            captions["<LOW>"] = 4;
            captions["<CLOSE>"] = 5;
            string[] values = "20150105;100100;54.0300000;54.4000000;53.6100000;53.9900000;784480".Split(';');
            var b = new Finance.Candle(captions, values);
            var c = new Finance.Candle(b);
            Assert.AreEqual(new DateTime(2015, 1, 5, 10, 1, 0), c.timestamp);
            Assert.AreEqual(54.03, c.open);
            Assert.AreEqual(54.4, c.high);
            Assert.AreEqual(53.61, c.low);
            Assert.AreEqual(53.99, c.close);
        }

        [TestMethod]
        public void Equality()
        {
            var captions = new System.Collections.Generic.Dictionary<string, int>();
            captions["<DATE>"] = 0;
            captions["<TIME>"] = 1;
            captions["<OPEN>"] = 2;
            captions["<HIGH>"] = 3;
            captions["<LOW>"] = 4;
            captions["<CLOSE>"] = 5;
            string[] values = "20150105;100100;54.0300000;54.4000000;53.6100000;53.9900000;784480".Split(';');
            var b = new Finance.Candle(captions, values);
            var e = new Finance.Candle(b);
            var c0 = new Finance.Candle(b);
            c0.timestamp = c0.timestamp.AddDays(1);
            var c1 = new Finance.Candle(b);
            c1.open = 1;
            var c2 = new Finance.Candle(b);
            c2.high = 1;
            var c3 = new Finance.Candle(b);
            c3.low = 1;
            var c4 = new Finance.Candle(b);
            c4.close = 1;
            Assert.AreEqual(true, b == e);
            Assert.AreEqual(false, b == c0);
            Assert.AreEqual(false, b == c1);
            Assert.AreEqual(false, b == c2);
            Assert.AreEqual(false, b == c3);
            Assert.AreEqual(false, b == c4);
        }

        [TestMethod]
        public void Inequality()
        {
            var captions = new System.Collections.Generic.Dictionary<string, int>();
            captions["<DATE>"] = 0;
            captions["<TIME>"] = 1;
            captions["<OPEN>"] = 2;
            captions["<HIGH>"] = 3;
            captions["<LOW>"] = 4;
            captions["<CLOSE>"] = 5;
            string[] values = "20150105;100100;54.0300000;54.4000000;53.6100000;53.9900000;784480".Split(';');
            var b = new Finance.Candle(captions, values);
            var e = new Finance.Candle(b);
            var c0 = new Finance.Candle(b);
            c0.timestamp = c0.timestamp.AddDays(1);
            var c1 = new Finance.Candle(b);
            c1.open = 1;
            var c2 = new Finance.Candle(b);
            c2.high = 1;
            var c3 = new Finance.Candle(b);
            c3.low = 1;
            var c4 = new Finance.Candle(b);
            c4.close = 1;
            Assert.AreEqual(false, b != e);
            Assert.AreEqual(true, b != c0);
            Assert.AreEqual(true, b != c1);
            Assert.AreEqual(true, b != c2);
            Assert.AreEqual(true, b != c3);
            Assert.AreEqual(true, b != c4);
        }

        [TestMethod]
        public void Equals()
        {
            var captions = new System.Collections.Generic.Dictionary<string, int>();
            captions["<DATE>"] = 0;
            captions["<TIME>"] = 1;
            captions["<OPEN>"] = 2;
            captions["<HIGH>"] = 3;
            captions["<LOW>"] = 4;
            captions["<CLOSE>"] = 5;
            string[] values = "20150105;100100;54.0300000;54.4000000;53.6100000;53.9900000;784480".Split(';');
            var b = new Finance.Candle(captions, values);
            var e = new Finance.Candle(b);
            var c0 = new Finance.Candle(b);
            c0.timestamp = c0.timestamp.AddDays(1);
            var c1 = new Finance.Candle(b);
            c1.open = 1;
            var c2 = new Finance.Candle(b);
            c2.high = 1;
            var c3 = new Finance.Candle(b);
            c3.low = 1;
            var c4 = new Finance.Candle(b);
            c4.close = 1;
            Assert.AreEqual(true, b.Equals(e));
            Assert.AreEqual(false, b.Equals(c0));
            Assert.AreEqual(false, b.Equals(c1));
            Assert.AreEqual(false, b.Equals(c2));
            Assert.AreEqual(false, b.Equals(c3));
            Assert.AreEqual(false, b.Equals(c4));
            Assert.AreEqual(false, b.Equals(1));
            Assert.AreEqual(false, b.Equals(null));
        }

        [TestMethod]
        public new void GetHashCode()
        {
            DateTime timestamp = new DateTime(2015, 1, 5, 10, 1, 0);
            double open = 54.03;
            double high = 54.4;
            double low = 53.61;
            double close = 53.99;
            Finance.Candle c = new Finance.Candle(timestamp, open, high, low, close);
            int hash;
            unchecked
            {
                hash = ((((timestamp.GetHashCode()
                    * 239017) + open.GetHashCode())
                    * 239017 + high.GetHashCode())
                    * 239017 + low.GetHashCode())
                    * 239017 + close.GetHashCode();
            }
            Assert.AreEqual(hash, c.GetHashCode());
        }

        [TestMethod]
        public new void ToString()
        {
            DateTime timestamp = new DateTime(2015, 1, 5, 10, 1, 0);
            double open = 54.03;
            double high = 54.4;
            double low = 53.61;
            double close = 53.99;
            Finance.Candle c = new Finance.Candle(timestamp, open, high, low, close);
            Assert.AreEqual("20150105;100100;54.03;54.4;53.61;53.99", c.ToString());
        }
    }
}
