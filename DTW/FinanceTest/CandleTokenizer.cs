using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FinanceTest
{
    [TestClass]
    public class CandleTokenizer
    {
        public static System.IO.Stream GenerateStream(string s)
        {
            System.IO.MemoryStream stream = new System.IO.MemoryStream();
            System.IO.StreamWriter writer = new System.IO.StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

        const string SAMPLE =
            "<DATE>;<TIME>;<OPEN>;<HIGH>;<LOW>;<CLOSE>;<VOL>\n" +
            "20150105;100100;54.0300000;54.4000000;53.6100000;53.9900000;784480\n" +
            "20150105;100200;53.9900000;54.0500000;53.7200000;53.7200000;287400";

        [TestMethod]
        public void Constructor()
        {
            Finance.CandleTokenizer ct = new Finance.CandleTokenizer(
                new System.IO.StreamReader(GenerateStream(SAMPLE)));
        }

        [TestMethod]
        public void GetLength()
        {
            Finance.CandleTokenizer ct = new Finance.CandleTokenizer(
                new System.IO.StreamReader(GenerateStream(SAMPLE)));
            Assert.AreEqual(2, ct.GetLength());
        }

        [TestMethod]
        public void Candles()
        {
            Finance.CandleTokenizer ct = new Finance.CandleTokenizer(
                new System.IO.StreamReader(GenerateStream(SAMPLE)));
            var c1 = new Finance.Candle(new DateTime(2015, 1, 5, 10, 1, 0), 54.03, 54.4, 53.61, 53.99);
            var c2 = new Finance.Candle(new DateTime(2015, 1, 5, 10, 2, 0), 53.99, 54.05, 53.72, 53.72);
            Assert.AreEqual(c1, ct[0]);
            Assert.AreEqual(c2, ct[1]);
        }

        [TestMethod]
        public void Save()
        {
            Finance.CandleTokenizer ct = new Finance.CandleTokenizer(
                new System.IO.StreamReader(GenerateStream(SAMPLE)));
            var ms = new System.IO.MemoryStream();
            ct.Save(new System.IO.StreamWriter(ms));
            ms.Seek(0, System.IO.SeekOrigin.Begin);
            var sr = new System.IO.StreamReader(ms);
            var line1 = sr.ReadLine();
            var line2 = sr.ReadLine();
            var line3 = sr.ReadLine();
            Assert.AreEqual(true, sr.EndOfStream);
            Assert.AreEqual("<DATE>;<TIME>;<OPEN>;<HIGH>;<LOW>;<CLOSE>", line1);
            Assert.AreEqual("20150105;100100;54.03;54.4;53.61;53.99", line2);
            Assert.AreEqual("20150105;100200;53.99;54.05;53.72;53.72", line3);
        }
    }
}
