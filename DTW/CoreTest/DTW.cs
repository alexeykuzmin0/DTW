using System;
using System.Linq;
using System.Collections.Generic;
using Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CoreTest
{
    class Double : Core.IDistansable
    {
        double x;

        public Double(double x)
        {
            this.x = x;
        }

        public double DistanceTo(Core.IDistansable rhs)
        {
            Double r = (Double)rhs;
            return Math.Abs(x - r.x);
        }
    }

    [TestClass]
    public class DTW
    {
        [TestMethod]
        public void Constructor()
        {
            Core.DTW<Double> dtw = new Core.DTW<Double>(new List<Double>());
        }

        [TestMethod]
        public void Empty()
        {
            DTW<Double> dtw = new DTW<Double>(new List<Double>());
            Assert.AreEqual(0.0, dtw.GetResult());
        }

        [TestMethod]
        public void HalfEmpty()
        {
            DTW<Double> dtw = new DTW<Double>(Enumerable.Repeat(new Double(0.0), 1));
            Assert.AreEqual(double.PositiveInfinity, dtw.GetResult());
        }

        [TestMethod]
        public void ProcessOne()
        {
            DTW<Double> dtw = new DTW<Double>(Enumerable.Repeat(new Double(0.0), 1));
            dtw.Process(new Double(1));
            Assert.AreEqual(1.0, dtw.GetResult());
        }

        [TestMethod]
        public void ComplicatedPath()
        {
            List<Double> pattern = new List<Double>();
            pattern.Add(new Double(0));
            pattern.Add(new Double(100));
            DTW<Double> dtw = new DTW<Double>(pattern);
            dtw.Process(new Double(0));
            Assert.AreEqual(100.0, dtw.GetResult());
            dtw.Process(new Double(1));
            Assert.AreEqual(99.0, dtw.GetResult());
            dtw.Process(new Double(100));
            Assert.AreEqual(1.0, dtw.GetResult());
        }
    }
}
