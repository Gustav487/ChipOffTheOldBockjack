using ChipMasters.GodotWrappers;
using ChipMasters.Menu.Displays;
using ChipMasters.Menu.Displays.WinRatios;
using ChipMasters.User;
using Fakes.ChipMasters.GodotWrappers;

namespace Tests.ChipMasters.Menus.Displays.WinRatios
{
    [TestClass]
    public class TestRWinRatioDisplay
    {
        [TestMethod]
        public void Ctor()
        {
            ILabel l = new FakeLabel();

            Assert.AreEqual("", l.Text);

            IDisplay<VWinRatio?> wRD = new RWinRatioDisplay(l);

            Assert.AreEqual(RWinRatioDisplay.UNDEFINED, l.Text);
        } // end Ctor()

        [TestMethod]
        [DataRow(null, null, null, 0, 1, 2, ALabelDisplay<VWinRatio>.UNDEFINED, "0:1:2")]
        [DataRow(null, null, null, null, null, null, ALabelDisplay<VWinRatio>.UNDEFINED, ALabelDisplay<VWinRatio>.UNDEFINED)]
        [DataRow(994, 12, 63, null, null, null, "994:12:63", ALabelDisplay<VWinRatio>.UNDEFINED)]
        [DataRow(994, 12, 63, 995, 12, 63, "994:12:63", "995:12:63")]
        [DataRow(994, 12, 63, 994, 12, 63, "994:12:63", "994:12:63")]
        public void SetWinRatio(
            int? wF, int? tF, int? lF,
            int? wT, int? tT, int? lT,
            string exPre, string exPost)
        {
#pragma warning disable CS8629
            VWinRatio? from = wF is null ? null : new VWinRatio((int)wF, (int)tF, (int)lF);
            VWinRatio? to = wT is null ? null : new VWinRatio((int)wT, (int)tT, (int)lT);
#pragma warning restore CS8629

            ILabel l = new FakeLabel();
            IDisplay<VWinRatio?> wRD = new RWinRatioDisplay(l);

            wRD.Display = from;

            Assert.AreEqual(exPre, l.Text);
            wRD.Display = to;
            Assert.AreEqual(exPost, l.Text);
        } // end SetWinRatio()
    } // end class
} // end namespace