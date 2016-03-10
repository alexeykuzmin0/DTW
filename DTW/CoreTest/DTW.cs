using System;
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
            Core.DTW<Double> dtw = new Core.DTW<Double>();
        }
    }
}
