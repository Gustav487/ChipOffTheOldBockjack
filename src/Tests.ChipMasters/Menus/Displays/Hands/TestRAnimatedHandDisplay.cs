using ChipMasters.Cards;
using ChipMasters.Games.Hands;
using ChipMasters.GodotWrappers;
using ChipMasters.Menu.Displays.Animations;
using ChipMasters.Menu.Displays.Arrangers;
using ChipMasters.Menu.Displays.Cards;
using ChipMasters.Menu.Displays.Hand;
using ChipMasters.Menu.SubDisplays;
using ChipMasters.Util;
using Fakes.ChipMasters.Menus.Displays.Arrangers;
using Fakes.ChipMasters.Menus.Displays.Cards;
using Tests.ChipMasters.Menus.Displays.Animators.ControlCardDisplay;

namespace Tests.ChipMasters.Menus.Displays.Hands
{
    [TestClass]
    public class TestRAnimatedHandDisplay : TestRHandDisplay
    {
        protected override IHandDisplay Ctor(IPool<IControlCardDisplay> cardDisplayPool,
            IArranger<IControl> arranger,
            Action<INode> addChild, Action<INode> removeChild)
            => Ctor(cardDisplayPool, arranger, addChild, removeChild,
                new FakeControlCardDisplayAnimator());

        protected virtual IHandDisplay Ctor(IPool<IControlCardDisplay> cardDisplayPool,
            IArranger<IControl> arranger,
            Action<INode> addChild, Action<INode> removeChild,
            IAnimator<IControlCardDisplay> revealAnimator)
            => new RAnimatedHandDisplay(cardDisplayPool, arranger, addChild, removeChild,
                revealAnimator);



        [TestMethod]
        [DataRow(false, true)]
        [DataRow(true, true)]
        public void HandSetFlips(bool veiled, bool exFlip)
        {
            FakeControlCardDisplay ccd = new();
            Queue<IControlCardDisplay> q = new(new IControlCardDisplay[] { ccd });

            IPool<IControlCardDisplay> cdp = new RPool<IControlCardDisplay>(() => q.Dequeue());
            FakeControlCardDisplayAnimator ccda = new();
            IHandDisplay hd = Ctor(cdp, new FakeControlArranger(), (_) => { }, (_) => { }, ccda);

            Assert.IsNull(ccda._Animating_);

            RHand fh = new();
            ICard c1 = new RCard(ECardRank.Two, ECardSuit.Clubs, veiled: veiled);
            fh.AddCard(c1);
            hd.Hand = fh;

            Assert.AreEqual(exFlip, ccda._Animating_ == ccd);
        } // end HandSetFlips()

        [TestMethod]
        [DataRow(false, true)]
        [DataRow(true, true)]
        public void HandCardAddFlips(bool veiled, bool exFlip)
        {
            FakeControlCardDisplay ccd = new();
            Queue<IControlCardDisplay> q = new(new IControlCardDisplay[] { ccd });

            IPool<IControlCardDisplay> cdp = new RPool<IControlCardDisplay>(() => q.Dequeue());
            FakeControlCardDisplayAnimator ccda = new();
            IHandDisplay hd = Ctor(cdp, new FakeControlArranger(), (_) => { }, (_) => { }, ccda);

            Assert.IsNull(ccda._Animating_);

            RHand fh = new();
            hd.Hand = fh;

            Assert.IsNull(ccda._Animating_);

            ICard c1 = new RCard(ECardRank.Two, ECardSuit.Clubs, veiled: veiled);
            fh.AddCard(c1);

            Assert.AreEqual(exFlip, ccda._Animating_ == ccd);
        } // end HandCardAddFlips()

    } // end class
} // end namespace