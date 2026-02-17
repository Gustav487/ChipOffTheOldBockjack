using ChipMasters.Cards;
using ChipMasters.Menu.Displays;
using ChipMasters.Menu.Displays.Animations;
using ChipMasters.Menu.Displays.Animations.CardDisplay;
using Fakes.ChipMasters.Menus.Displays.Cards;

namespace Tests.ChipMasters.Menus.Displays.Animators.ControlCardDisplay
{
    [TestClass]
    public class TestRCardFlipAnimator
    {
        [TestMethod]
        [DataRow(false, false, true)]
        [DataRow(true, false, true)]
        public void Animate(bool veiled, bool? exFlipped1, bool? exFlipped2)
        {
            FakeControlCardDisplay ccd = new() { Display = new RCard(ECardRank.Eight, ECardSuit.Spades, veiled: veiled) };
            IAnimator<IDisplay<ICard>> cda = new RCardFlipAnimator();

            Assert.AreEqual(exFlipped1, ccd._CardFliped_);

            IAnimation a = cda.Animate(ccd);

            Assert.AreEqual(exFlipped2, ccd._CardFliped_);
            Assert.IsTrue(a.IsFinished);
        } // end Animate()
    } // end class
} // end namespace