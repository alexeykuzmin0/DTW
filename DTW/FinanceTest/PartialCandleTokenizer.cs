using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FinanceTest
{
    [TestClass]
    public class PartialCandleTokenizer
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
            "<TICKER>;<PER>;<DATE>;<TIME>;<OPEN>;<HIGH>;<LOW>;<CLOSE>;<VOL>\n" +
            "SBER;1;20150105;100100;54.0300000;54.4000000;53.6100000;53.9900000;784480\n" +
            "SBER;1;20150105;100200;53.9900000;54.0500000;53.7200000;53.7200000;287400";

        [TestMethod]
        public void Constructor()
        {
            Finance.CandleTokenizer ct = new Finance.CandleTokenizer(
                new System.IO.StreamReader(GenerateStream(SAMPLE)));
            Finance.PartialCandleTokenizer pt = new Finance.PartialCandleTokenizer(ct, 1, 1);
        }

        [TestMethod]
        public void Length()
        {
            Finance.CandleTokenizer ct = new Finance.CandleTokenizer(
                new System.IO.StreamReader(GenerateStream(SAMPLE)));
            Finance.PartialCandleTokenizer pt = new Finance.PartialCandleTokenizer(ct, 1, 1);
            Assert.AreEqual(1, pt.GetLength());
        }

        [TestMethod]
        public void Candle()
        {
            Finance.CandleTokenizer ct = new Finance.CandleTokenizer(
                new System.IO.StreamReader(GenerateStream(SAMPLE)));
            Finance.PartialCandleTokenizer pt = new Finance.PartialCandleTokenizer(ct, 1, 1);
            Assert.AreEqual(ct[1], pt[0]);
        }
    }
}
