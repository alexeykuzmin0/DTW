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
    }
}
