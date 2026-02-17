namespace Tests.ChipMasters.Util
{
    [TestClass]
    public class TestMathUtil
    {
        [TestMethod]
        [DataRow(0f, 0f, null, true)]
        [DataRow(0f, 0f, 1e-9f, true)]
        [DataRow(0f, 0f, 1f, true)]
        [DataRow(0.1000003f, 0.1f, null, true)]
        [DataRow(0.1000003f, 0.1f, .000001f, true)]
        [DataRow(0.1000003f, 0.1f, 1f, true)]

        [DataRow(1.1000003f, 0.1f, null, false)]
        [DataRow(23.1f, 23f, null, false)]
        [DataRow(-9.0004f, -9.0003f, null, false)]
        [DataRow(0.1000003f, 0.1f, .0000001f, false)]
        public void ApproximatelyEquals(float a, float b, float? epsilon, bool ex)
        {
            if (epsilon is null)
                Assert.AreEqual(ex, MathUtil.ApproximatelyEquals(a, b));
            else
                Assert.AreEqual(ex, MathUtil.ApproximatelyEquals(a, b, (float)epsilon));
        } // ApproximatelyEquals()

        [TestMethod]
        [DataRow(0f, 1f, null, true)]
        [DataRow(-1f, 0f, null, true)]
        [DataRow(.4f, .5f, null, true)]
        [DataRow(.0004f, .0005f, null, true)]
        [DataRow(.00004f, .00005f, .000000001f, true)]
        [DataRow(.0000004f, .0000005f, .000000001f, true)]
        [DataRow(.0004f, 99.0005f, 99.0001f, true)]

        [DataRow(0f, 0f, null, false)]
        [DataRow(1f, 0f, null, false)]
        [DataRow(.0003f, 0f, null, false)]
        [DataRow(.00004f, .00005f, null, false)]
        [DataRow(.00000003f, 0f, null, false)]
        [DataRow(.0000004f, .0000005f, null, false)]
        [DataRow(.0000004f, .0000005f, 99.0001f, false)]
        [DataRow(.0000004f, 98f, 99.0001f, false)]
        public void ApproximatelyLessThan(float a, float b, float? epsilon, bool ex)
        {
            if (epsilon is null)
                Assert.AreEqual(ex, MathUtil.ApproximatelyLessThan(a, b));
            else
                Assert.AreEqual(ex, MathUtil.ApproximatelyLessThan(a, b, (float)epsilon));
        } // ApproximatelyLessThan()
    } // end class
} // end namespace