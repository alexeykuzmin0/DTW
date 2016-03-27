using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GUIComponentsTest
{
    [TestClass]
    public class CandleStickChart
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
            var csc = new GUIComponents.CandleStickChart();
            Assert.AreEqual("No data loaded", csc.GraphPane.Title.Text);
            Assert.AreEqual("", csc.GraphPane.XAxis.Title.Text);
            Assert.AreEqual("", csc.GraphPane.YAxis.Title.Text);
        }

        [TestMethod]
        public void SetCandles()
        {
            var tokenizer = new Finance.CandleTokenizer(
                new System.IO.StreamReader(GenerateStream(SAMPLE)));
            var csc = new GUIComponents.CandleStickChart();
            csc.SetCandles(tokenizer);
            Assert.AreEqual(1, csc.GraphPane.CurveList.Count);
            Assert.AreEqual(2, csc.GraphPane.CurveList[0].NPts);
            Assert.AreEqual(0, csc.GraphPane.XAxis.Scale.Min);
            Assert.AreEqual(1.2, csc.GraphPane.XAxis.Scale.Max);
            Assert.AreEqual("SBER 1 minute", csc.GraphPane.Title.Text);
        }

        [TestMethod]
        public void SetCandlesNull()
        {
            var csc = new GUIComponents.CandleStickChart();
            csc.SetCandles(null);
            Assert.AreEqual(0, csc.GraphPane.CurveList.Count);
            Assert.AreEqual("No data loaded", csc.GraphPane.Title.Text);
        }
    }
}
