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
    }
}
