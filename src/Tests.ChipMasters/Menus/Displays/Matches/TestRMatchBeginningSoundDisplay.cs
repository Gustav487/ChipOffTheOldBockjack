using ChipMasters.Games.Matches;
using ChipMasters.GodotWrappers;
using ChipMasters.Menu.Displays;
using ChipMasters.Menu.Displays.Matches;
using Fakes.ChipMasters.Games.Matches;
using Fakes.ChipMasters.GodotWrappers;

namespace Tests.ChipMasters.Menus.Displays.Matches
{
    [TestClass]
    public class TestRMatchBeginningSoundDisplay
    {
        [TestMethod]
        [DataRow(false, true, true)]
        [DataRow(true, false, false)]
        [DataRow(null, false, false)]
        public void SetDisplay(bool? concluded, bool exA1, bool exA2)
        {
            IAudioStreamPlayer a1 = new FakeAudioStreamPlayer();
            IAudioStreamPlayer a2 = new FakeAudioStreamPlayer();
            Assert.IsFalse(a1.Playing);
            Assert.IsFalse(a2.Playing);

            IDisplay<IMatch> d = new RMatchBeginningSoundDisplay(a1, a2);
            Assert.IsFalse(a1.Playing);
            Assert.IsFalse(a2.Playing);

            if (concluded is not null)
                d.Display = new FakeMatch(isConcluded: concluded.Value);
            else
                d.Display = null;

            Assert.AreEqual(exA1, a1.Playing);
            Assert.AreEqual(exA2, a2.Playing);
        } // end SetDisplay()

        [TestMethod]
        public void Dispose()
        {
            IAudioStreamPlayer a1 = new FakeAudioStreamPlayer();
            IAudioStreamPlayer a2 = new FakeAudioStreamPlayer();
            IDisplay<IMatch> d = new RMatchBeginningSoundDisplay(a1, a2);

            d.Dispose(); // nothing really happens, nothing needs to
        } // end Dispose()
    } // end class
} // end namespace