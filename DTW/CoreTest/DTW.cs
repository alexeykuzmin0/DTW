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
    }
}
