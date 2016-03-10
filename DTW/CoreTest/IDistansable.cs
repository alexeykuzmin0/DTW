using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CoreTest
{
    class C : Core.IDistansable
    {
        public double DistanceTo(Core.IDistansable rhs)
        {
            return 0;
        }
    }

    [TestClass]
    public class IDistansable
    {
        [TestMethod]
        public void Constructor()
        {
            new C();
        }
    }
}
