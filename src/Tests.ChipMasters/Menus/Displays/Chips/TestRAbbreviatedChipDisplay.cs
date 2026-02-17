using ChipMasters.GodotWrappers;
using ChipMasters.Menu.Displays.Chips;
using Fakes.ChipMasters.GodotWrappers;

namespace Tests.ChipMasters.Menus.SubDisplays.Chips
{
    public static class TestRAbbreviatedChipDisplay
    {
        [TestClass]
        public class Valid
        {
            [TestMethod]
            [DataRow(null, RAbbreviatedChipDisplay.UNDEFINED, RAbbreviatedChipDisplay.UNDEFINED)]
            [DataRow(null, RAbbreviatedChipDisplay.UNDEFINED, RAbbreviatedChipDisplay.UNDEFINED, true)]
            [DataRow(0, "0", "0")]
            [DataRow(0, "0", "0", true)]
            [DataRow(1, "1", "1")]
            [DataRow(1, "+1", "+1", true)]
            [DataRow(-3, "-3", "-3")]

            [DataRow(92, "92", "92")]
            [DataRow(-36, "-36", "-36")]

            [DataRow(401, "401", "401")]
            [DataRow(-653, "-653", "-653")]
            [DataRow(999, "999", "999")]
            [DataRow(-999, "-999", "-999")]

            [DataRow(1000, "1k", "1000")]
            [DataRow(-1000, "-1k", "-1000")]
            [DataRow(1099, "1k", "1099")]
            [DataRow(-1030, "-1k", "-1030")]
            [DataRow(1499, "1.4k", "1499")]
            [DataRow(-1830, "-1.8k", "-1830")]
            [DataRow(9999, "9.9k", "9999")]
            [DataRow(-9999, "-9.9k", "-9999")]

            [DataRow(14990, "14k", "14990")]
            [DataRow(-18301, "-18k", "-18301")]

            [DataRow(154990, "154k", "154990")]
            [DataRow(-998301, "-998k", "-998301")]
            [DataRow(999000, "999k", "999000")]
            [DataRow(-999000, "-999k", "-999000")]
            [DataRow(999_999, "999k", "999999")]
            [DataRow(-999_999, "-999k", "-999999")]

            [DataRow(1000_000, "1m", "1000000")]
            [DataRow(-1000_000, "-1m", "-1000000")]
            [DataRow(1099000, "1m", "1099000")]
            [DataRow(-1030000, "-1m", "-1030000")]
            [DataRow(3_499_000, "3.4m", "3499000")]
            [DataRow(-5_830_000, "-5.8m", "-5830000")]
            [DataRow(9_999_999, "9.9m", "9999999")]
            [DataRow(9_999_999, "+9.9m", "+9999999", true)]
            [DataRow(-9_999_999, "-9.9m", "-9999999")]
            [DataRow(-9_999_999, "-9.9m", "-9999999", true)]

            [DataRow(30_499_000, "30m", "30499000")]
            [DataRow(-51_830_000, "-51m", "-51830000")]

            [DataRow(723_499_000, "723m", "723499000")]
            [DataRow(723_499_000, "+723m", "+723499000", true)]
            [DataRow(-598_830_000, "-598m", "-598830000")]
            [DataRow(999_499_001, "999m", "999499001")]
            [DataRow(-999_830_030, "-999m", "-999830030")]
            [DataRow(999_999_999, "999m", "999999999")]
            [DataRow(-999_999_999, "-999m", "-999999999")]

            [DataRow(1_000_000_000, "1b", "1000000000")]
            [DataRow(-1_000_000_000, "-1b", "-1000000000")]
            [DataRow(2_050_000_000, "2b", "2050000000")]
            [DataRow(-2_098_900_000, "-2b", "-2098900000")]
            [DataRow(2_100_000_000, "2.1b", "2100000000")]
            [DataRow(-2_100_000_000, "-2.1b", "-2100000000")]

            [DataRow(int.MaxValue, "2.1b", "2147483647")]
            [DataRow(int.MaxValue, "+2.1b", "+2147483647", true)]
            [DataRow(int.MinValue, "-2.1b", "-2147483648")]
            [DataRow(int.MinValue, "-2.1b", "-2147483648", true)]
            public void Display(int? chips, string exText, string exToolTip, bool explicitSign = false)
            {
                ILabel label = new FakeLabel();
                Assert.AreEqual(Godot.Control.MouseFilterEnum.Ignore, label.MouseFilter);

                IChipDisplay cd = new RAbbreviatedChipDisplay(label);
                Assert.AreEqual(Godot.Control.MouseFilterEnum.Pass, label.MouseFilter);

                cd.Chips = chips;
                cd.ExplicitSign = explicitSign;
                Assert.AreEqual(exText, label.Text);
                Assert.AreEqual(exToolTip, label.TooltipText);
            } // end Display()
        } // end inner class Valid
    } // end class
} // end namespace