using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FinanceTest
{
    [TestClass]    
    public class DisjointMergeCandleTokenizer
    {
        const string SAMPLE1 =
            "<TICKER>;<PER>;<DATE>;<TIME>;<OPEN>;<HIGH>;<LOW>;<CLOSE>;<VOL>\n" +
            "SBER;1;20150105;100100;54.0300000;54.4000000;53.6100000;53.9900000;784480";
        const string SAMPLE2 =
            "<TICKER>;<PER>;<DATE>;<TIME>;<OPEN>;<HIGH>;<LOW>;<CLOSE>;<VOL>\n" +
            "SBER;1;20150105;100200;53.9900000;54.0500000;53.7200000;53.7200000;287400";
        const string SAMPLE3 =
            "<TICKER>;<PER>;<DATE>;<TIME>;<OPEN>;<HIGH>;<LOW>;<CLOSE>;<VOL>\n" +
            "SBER;2;20150105;100200;53.9900000;54.0500000;53.7200000;53.7200000;287400";
        const string SAMPLE4 =
            "<TICKER>;<PER>;<DATE>;<TIME>;<OPEN>;<HIGH>;<LOW>;<CLOSE>;<VOL>\n" +
            "SBPR;1;20150105;100200;53.9900000;54.0500000;53.7200000;53.7200000;287400";

        [TestMethod]
        public void Constructor()
        {
            var ct1 = new Finance.CandleTokenizer(
                new System.IO.StreamReader(CandleTokenizer.GenerateStream(SAMPLE1)));
            var ct2 = new Finance.CandleTokenizer(
                new System.IO.StreamReader(CandleTokenizer.GenerateStream(SAMPLE2)));
            var ct = new Finance.DisjointMergeCandleTokenizer(ct1, ct2);
        }

        [TestMethod]
        public void TickerAndPeriodOK()
        {
            var ct1 = new Finance.CandleTokenizer(
                new System.IO.StreamReader(CandleTokenizer.GenerateStream(SAMPLE1)));
            var ct2 = new Finance.CandleTokenizer(
                new System.IO.StreamReader(CandleTokenizer.GenerateStream(SAMPLE2)));
            var ct = new Finance.DisjointMergeCandleTokenizer(ct1, ct2);
            Assert.AreEqual("SBER", ct.GetTicker());
            Assert.AreEqual(TimeSpan.FromMinutes(1), ct.GetPeriod());
        }

        [TestMethod]
        [ExpectedException(typeof(System.FormatException))]
        public void TickerIncorrect()
        {
            var ct1 = new Finance.CandleTokenizer(
                new System.IO.StreamReader(CandleTokenizer.GenerateStream(SAMPLE1)));
            var ct2 = new Finance.CandleTokenizer(
                new System.IO.StreamReader(CandleTokenizer.GenerateStream(SAMPLE4)));
            var ct = new Finance.DisjointMergeCandleTokenizer(ct1, ct2);
        }

        [TestMethod]
        [ExpectedException(typeof(System.FormatException))]
        public void PeriodIncorrect()
        {
            var ct1 = new Finance.CandleTokenizer(
                new System.IO.StreamReader(CandleTokenizer.GenerateStream(SAMPLE1)));
            var ct2 = new Finance.CandleTokenizer(
                new System.IO.StreamReader(CandleTokenizer.GenerateStream(SAMPLE3)));
            var ct = new Finance.DisjointMergeCandleTokenizer(ct1, ct2);
        }

        [TestMethod]
        public void GetLength()
        {
            var ct1 = new Finance.CandleTokenizer(
                new System.IO.StreamReader(CandleTokenizer.GenerateStream(SAMPLE1)));
            var ct2 = new Finance.CandleTokenizer(
                new System.IO.StreamReader(CandleTokenizer.GenerateStream(SAMPLE2)));
            var ct = new Finance.DisjointMergeCandleTokenizer(ct1, ct2);
            Assert.AreEqual(2, ct.GetLength());
        }

        [TestMethod]
        public void Candles()
        {
            var ct1 = new Finance.CandleTokenizer(
                new System.IO.StreamReader(CandleTokenizer.GenerateStream(SAMPLE1)));
            var ct2 = new Finance.CandleTokenizer(
                new System.IO.StreamReader(CandleTokenizer.GenerateStream(SAMPLE2)));
            var ct = new Finance.DisjointMergeCandleTokenizer(ct1, ct2);
            var c1 = new Finance.Candle(new DateTime(2015, 1, 5, 10, 1, 0), 54.03, 54.4, 53.61, 53.99);
            var c2 = new Finance.Candle(new DateTime(2015, 1, 5, 10, 2, 0), 53.99, 54.05, 53.72, 53.72);
            Assert.AreEqual(c1, ct[0]);
            Assert.AreEqual(c2, ct[1]);
        }

        [TestMethod]
        public void Candles2()
        {
            var ct1 = new Finance.CandleTokenizer(
                new System.IO.StreamReader(CandleTokenizer.GenerateStream(SAMPLE1)));
            var ct2 = new Finance.CandleTokenizer(
                new System.IO.StreamReader(CandleTokenizer.GenerateStream(SAMPLE2)));
            var ct = new Finance.DisjointMergeCandleTokenizer(ct2, ct1);
            var c1 = new Finance.Candle(new DateTime(2015, 1, 5, 10, 1, 0), 54.03, 54.4, 53.61, 53.99);
            var c2 = new Finance.Candle(new DateTime(2015, 1, 5, 10, 2, 0), 53.99, 54.05, 53.72, 53.72);
            Assert.AreEqual(c1, ct[0]);
            Assert.AreEqual(c2, ct[1]);
        }
    }
}
