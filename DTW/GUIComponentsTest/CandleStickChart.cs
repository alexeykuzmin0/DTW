using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GUIComponentsTest
{
    [TestClass]
    public class CandleStickChart
    {
        [TestMethod]
        public void Constructor()
        {
            var csc = new GUIComponents.CandleStickChart();
            Assert.AreEqual("No data loaded", csc.GraphPane.Title.Text);
            Assert.AreEqual("", csc.GraphPane.XAxis.Title.Text);
            Assert.AreEqual("", csc.GraphPane.YAxis.Title.Text);
        }
    }
}
