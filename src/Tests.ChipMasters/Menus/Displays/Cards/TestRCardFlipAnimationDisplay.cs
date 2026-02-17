using ChipMasters.Cards;
using ChipMasters.Menu.Displays;
using ChipMasters.Menu.Displays.Cards;
using Fakes.ChipMasters.Cards;
using Fakes.ChipMasters.GodotWrappers;
using Fakes.ChipMasters.Users;

namespace Tests.ChipMasters.Menus.Displays.Cards
{
    [TestClass]
    public class TestRCardFlipAnimationDisplay
    {
        [TestMethod]
        [DataRow(true, false, FakeAnimationPlayer.START_TIME, true, false, FakeAnimationPlayer.START_TIME)] // start as unveiled, and don't flip
        [DataRow(false, false, FakeAnimationPlayer.END_TIME, false, false, FakeAnimationPlayer.END_TIME)] // start as veiled, and don't flip
        [DataRow(true, false, FakeAnimationPlayer.START_TIME, false, true, FakeAnimationPlayer.START_TIME)] // start as unveiled, and then flip
        [DataRow(false, false, FakeAnimationPlayer.END_TIME, true, true, FakeAnimationPlayer.END_TIME)] // start as veiled, and then flip
        public void Set(
            bool veiled1, bool exAnimPlaying1, double exAnimTime1,
            bool veiled2, bool exAnimPlaying2, double exAnimTime2)
        {
            FakeAssetSelection a = new();
            FakeAnimationPlayer ap = new();
            Assert.AreEqual(FakeAnimationPlayer.NULL_TIME, ap._Time_);

            IDisplay<ICard> cd = new RCardFlipAnimationDisplay(a, ap);
            Assert.AreEqual(FakeAnimationPlayer.NULL_TIME, ap._Time_); // no card set, shouldn't start playing.

            FakeCard c = new(veiled: veiled1);
            cd.Display = c;
            Assert.AreEqual(exAnimPlaying1, ap._IsPlaying_); // verify set init value sets
            Assert.AreEqual(exAnimTime1, ap._Time_);

            c.Veiled = veiled2;
            Assert.AreEqual(exAnimPlaying2, ap._IsPlaying_);
            Assert.AreEqual(exAnimTime2, ap._Time_);
        } // end Set()

        [TestMethod]
        public void Dispose()
        {
            FakeAssetSelection a = new();
            FakeAnimationPlayer ap = new();
            Assert.AreEqual(FakeAnimationPlayer.NULL_TIME, ap._Time_);

            IDisplay<ICard> cd = new RCardFlipAnimationDisplay(a, ap);
            Assert.AreEqual(FakeAnimationPlayer.NULL_TIME, ap._Time_); // no card set, shouldn't start playing.

            FakeCard c = new();
            cd.Display = c;
            Assert.AreEqual(FakeAnimationPlayer.END_TIME, ap._Time_); // setting should set the time. (default state veiled)

            cd.Dispose();

            c.Flip();
            Assert.AreEqual(FakeAnimationPlayer.END_TIME, ap._Time_); // nothing should happen since disposed

            a.CardFlipChanged();
            Assert.AreEqual(FakeAnimationPlayer.END_TIME, ap._Time_); // nothing should happen since disposed
        } // end Dispose()
    } // end class
} // end namespace