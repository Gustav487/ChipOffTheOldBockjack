using ChipMasters.Games.BetHandlers;

namespace Tests.ChipMasters.Games.BetHandlers
{
    public static class TestVBetRange
    {
        [TestClass]
        public class Valid
        {
            [TestMethod]
            [DataRow(0, 0)]
            [DataRow(1, 1)]
            [DataRow(1, 34902)]
            [DataRow(0, 88801)]
            public void Ctor(int min, int max)
            {
                VBetRange br = new(min, max);
                Assert.AreEqual(min, br.Min);
                Assert.AreEqual(max, br.Max);
            } // end Ctor              
        } // end inner class Invalid

        [TestClass]
        public class Invalid
        {
            [TestMethod]
            [ExpectedException(typeof(ArgumentException))]
            [DataRow(2, 1)]
            [DataRow(0, -1)]
            [DataRow(-4, 0)]
            [DataRow(-9, -1)]
            public void Ctor(int min, int max) => new VBetRange(min, max);
        } // end inner class Invalid
    } // end class
} // end namespace