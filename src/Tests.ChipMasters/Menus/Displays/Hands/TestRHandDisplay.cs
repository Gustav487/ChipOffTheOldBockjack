using ChipMasters.Cards;
using ChipMasters.Games.Hands;
using ChipMasters.GodotWrappers;
using ChipMasters.Menu.Displays.Arrangers;
using ChipMasters.Menu.Displays.Cards;
using ChipMasters.Menu.Displays.Hand;
using ChipMasters.Menu.SubDisplays;
using ChipMasters.Util;
using Fakes.ChipMasters.Cards;
using Fakes.ChipMasters.Menus.Displays.Arrangers;
using Fakes.ChipMasters.Menus.Displays.Cards;

namespace Tests.ChipMasters.Menus.Displays.Hands
{
    [TestClass]
    public class TestRHandDisplay
    {
        protected virtual IHandDisplay Ctor(IPool<IControlCardDisplay> cardDisplayPool,
            IArranger<IControl> arranger,
            Action<INode> addChild, Action<INode> removeChild)
            => new RHandDisplay(cardDisplayPool, arranger, addChild, removeChild);



        [TestMethod]
        public void Dispose()
        {
            IPool<IControlCardDisplay> cdp = new RPool<IControlCardDisplay>(() => throw new NotImplementedException());
            IArranger<IControl> arranger = new FakeControlArranger();
            int addCount = 0;
            int removeCount = 0;
            Action<INode> addChild = (_) => addCount++;
            Action<INode> removeChild = (_) => removeCount++;
            IHandDisplay hd = Ctor(cdp, arranger, addChild, removeChild);

            RHand fh = new();
            hd.Hand = fh;
            Assert.AreEqual(0, addCount);
            Assert.AreEqual(0, removeCount);

            hd.Dispose();

            fh.AddCard(new FakeCard());
            Assert.AreEqual(0, addCount);
            Assert.AreEqual(0, removeCount);
        } // end Dispose()

        [TestMethod]
        public void Hand0Add1()
        {
            IControlCardDisplay ccd1 = new FakeControlCardDisplay();
            Queue<IControlCardDisplay> q = new(new IControlCardDisplay[] { ccd1 });

            IPool<IControlCardDisplay> cdp = new RPool<IControlCardDisplay>(() => q.Dequeue());
            IArranger<IControl> arranger = new FakeControlArranger();
            int addCount = 0;
            int removeCount = 0;
            Action<INode> addChild = (_) => addCount++;
            Action<INode> removeChild = (_) => removeCount++;
            IHandDisplay hd = Ctor(cdp, arranger, addChild, removeChild);

            AssertCCD(ccd1); // expect default state

            RHand fh = new();
            hd.Hand = fh;
            Assert.AreEqual(0, addCount);
            Assert.AreEqual(0, removeCount);
            Assert.AreEqual(1, q.Count);
            AssertCCD(ccd1); // expect default state

            ICard c1 = new FakeCard();
            fh.AddCard(c1);

            Assert.AreEqual(1, addCount);
            Assert.AreEqual(0, removeCount);
            Assert.AreEqual(0, q.Count);
            AssertCCD(ccd1, 1f, c1, true); // expect arranged and made visible
        } // end Hand0Add1()

        [TestMethod]
        public void Hand1Add1()
        {
            IControlCardDisplay ccd1 = new FakeControlCardDisplay();
            IControlCardDisplay ccd2 = new FakeControlCardDisplay();
            Queue<IControlCardDisplay> q = new(new IControlCardDisplay[] { ccd1, ccd2 });

            IPool<IControlCardDisplay> cdp = new RPool<IControlCardDisplay>(() => q.Dequeue());
            IArranger<IControl> arranger = new FakeControlArranger();
            int addCount = 0;
            int removeCount = 0;
            Action<INode> addChild = (_) => addCount++;
            Action<INode> removeChild = (_) => removeCount++;
            IHandDisplay hd = Ctor(cdp, arranger, addChild, removeChild);

            AssertCCD(ccd1); // expect default state
            AssertCCD(ccd2);

            RHand fh = new();
            ICard c1 = new FakeCard();
            fh.AddCard(c1);

            hd.Hand = fh;
            Assert.AreEqual(1, addCount);
            Assert.AreEqual(0, removeCount);
            Assert.AreEqual(1, q.Count);
            AssertCCD(ccd1, 1f, c1, true); // expect arranged and made visible
            AssertCCD(ccd2); // expect still in default state

            ICard c2 = new FakeCard();
            fh.AddCard(c2);

            Assert.AreEqual(2, addCount); // 1 on the initial set, then 2 from the rebuild on add
            Assert.AreEqual(0, removeCount);
            Assert.AreEqual(0, q.Count);
            AssertCCD(ccd1, 1f, c1, true); // display rebuilt but should still be identical
            AssertCCD(ccd2, 2f, c2, true); // should now be used also
        } // end Hand1Add1()

        [TestMethod]
        public void Hand2To1()
        {
            IControlCardDisplay ccd1 = new FakeControlCardDisplay();
            IControlCardDisplay ccd2 = new FakeControlCardDisplay();
            Queue<IControlCardDisplay> q = new(new IControlCardDisplay[] { ccd1, ccd2 });

            IPool<IControlCardDisplay> cdp = new RPool<IControlCardDisplay>(() => q.Dequeue());
            IArranger<IControl> arranger = new FakeControlArranger();
            int addCount = 0;
            int removeCount = 0;
            Action<INode> addChild = (_) => addCount++;
            Action<INode> removeChild = (_) => removeCount++;
            IHandDisplay hd = Ctor(cdp, arranger, addChild, removeChild);

            AssertCCD(ccd1); // expect default state
            AssertCCD(ccd2);

            RHand fh = new();
            ICard c1 = new FakeCard();
            ICard c2 = new FakeCard();
            fh.AddCard(c1);
            fh.AddCard(c2);
            hd.Hand = fh;

            Assert.AreEqual(2, addCount);
            Assert.AreEqual(0, removeCount);
            Assert.AreEqual(0, q.Count);
            AssertCCD(ccd1, 1f, c1, true); // expect arranged and made visible
            AssertCCD(ccd2, 2f, c2, true);

            RHand h2 = new();
            ICard c3 = new FakeCard();
            h2.AddCard(c3);
            hd.Hand = h2;

            Assert.AreEqual(5, addCount); // 1 on the initial set, then 2 from the rebuild on add - remove now refreshes the display, triggers twice when new added.
            Assert.AreEqual(4, removeCount);
            Assert.AreEqual(0, q.Count);
            AssertCCD(ccd1, 1f, c3, true); // display rebuilt but should still be identical
            AssertCCD(ccd2, 2f, c2, false); // should now be invisibile indicating it's unused - other details are not reset
        } // end Hand2To1()





        private static void AssertCCD(IControlCardDisplay ccd, float offs = 0f, ICard? c = null, bool vis = false)
        {
            Assert.AreEqual(offs, ccd.AnchorTop);
            Assert.AreEqual(offs, ccd.AnchorBottom);
            Assert.AreEqual(offs, ccd.AnchorLeft);
            Assert.AreEqual(offs, ccd.AnchorRight);
            Assert.AreEqual(c, ccd.Display);
            Assert.AreEqual(vis, ccd.Visible);
        } // end AssertCCD()

    } // end class
} // end namespace