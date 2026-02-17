using ChipMasters.GodotWrappers;
using ChipMasters.Menu.Displays.Chips;
using Fakes.ChipMasters.GodotWrappers;

namespace Tests.ChipMasters.Menus.Displays.Chips
{
    public static class TestRChipDisplay
    {
        [TestClass]
        public class Valid
        {
            [TestMethod]
            [DataRow(null, "N/A")]
            [DataRow(0, "0")]
            [DataRow(0, "0", true)]
            [DataRow(1, "1")]
            [DataRow(-3, "-3")]

            [DataRow(92, "92")]
            [DataRow(-36, "-36")]

            [DataRow(401, "401")]
            [DataRow(-653, "-653")]
            [DataRow(999, "999")]
            [DataRow(999, "+999", true)]
            [DataRow(-999, "-999")]

            [DataRow(1000, "1000")]
            [DataRow(-1000, "-1000")]

            [DataRow(154990, "154990")]
            [DataRow(-998301, "-998301")]

            [DataRow(-1030000, "-1030000")]
            [DataRow(3_499_000, "3499000")]
            [DataRow(-5_830_000, "-5830000")]

            [DataRow(int.MaxValue, "2147483647")]
            [DataRow(int.MaxValue, "+2147483647", true)]
            [DataRow(int.MinValue, "-2147483648")]
            public void Display(int? chips, string expectation, bool explicitSign = false)
            {
                ILabel label = new FakeLabel();
                IChipDisplay cd = new RChipDisplay(label);

                cd.Chips = chips;
                cd.ExplicitSign = explicitSign;
                Assert.AreEqual(expectation, label.Text);
            } // end Display()
        } // end inner class Valid
    } // end class
} // end namespace