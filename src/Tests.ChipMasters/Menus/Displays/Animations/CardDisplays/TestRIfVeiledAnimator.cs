using ChipMasters.Cards;
using ChipMasters.Menu.Displays;
using ChipMasters.Menu.Displays.Animations;
using ChipMasters.Menu.Displays.Animations.CardDisplay;
using Fakes.ChipMasters.Cards;
using Fakes.ChipMasters.Menus.Displays.Cards;

namespace Tests.ChipMasters.Menus.Displays.Animations.CardDisplays
{
    public class TestRIfVeiledAnimator
    {
        [TestClass]
        public class Valid
        {
            [TestMethod]
            public void Animate()
            {
                ICard c = new FakeCard(ECardRank.Ace, ECardSuit.Spades, false);
                IDisplay<ICard> d = new FakeControlCardDisplay();
                d.Display = c;
                IAnimator<IDisplay<ICard>> adc1 = new RCardVeilAnimator();
                IAnimator<IDisplay<ICard>> adc2 = new RIfVeiledAnimator(adc1, false);
                IAnimation a = adc2.Animate(d);
                Assert.AreEqual(IAnimation.EMPTY, a);
                Assert.AreEqual(false, d.Display.Veiled);

                c.Veiled = true;
                a = adc2.Animate(d);
                Assert.AreEqual(IAnimation.EMPTY, a);
                Assert.AreEqual(true, d.Display.Veiled);
            } // end Animate()
        } // end inner class Valid
    } // end class
} // end namespace
