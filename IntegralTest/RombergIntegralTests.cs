using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Integral.Tests {
	[TestClass()]
    public class RombergIntegralTests {
        [TestMethod()]
        public void IntegrateTest() {
            static double f(double x) => Math.Sqrt(1 - x * x);

            double v = RombergIntegral.Integrate(f, 0, Math.Sqrt(2) / 2);

            Assert.AreEqual(v, (Math.PI + 2) / 8, 1e-15);
        }
    }
}