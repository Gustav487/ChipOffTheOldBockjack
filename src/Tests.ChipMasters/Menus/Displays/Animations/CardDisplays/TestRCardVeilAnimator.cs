using ChipMasters.Cards;
using ChipMasters.Menu.Displays;
using ChipMasters.Menu.Displays.Animations;
using ChipMasters.Menu.Displays.Animations.CardDisplay;
using Fakes.ChipMasters.Cards;
using Fakes.ChipMasters.Menus.Displays.Cards;

namespace Tests.ChipMasters.Menus.Displays.Animations.CardDisplays
{
    public class TestRCardVeilAnimator
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
                IAnimator<IDisplay<ICard>> adc = new RCardVeilAnimator();
                IAnimation a = adc.Animate(d);
                Assert.AreEqual(IAnimation.EMPTY, a);
                Assert.AreEqual(true, d.Display!.Veiled);
            } // end Animate()
        } // end inner class Valid
    } // end class
} // end namespace
