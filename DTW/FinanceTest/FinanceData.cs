﻿using System;
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
    }
}
