using ChipMasters.Menu.Displays.Animations;
using Fakes.ChipMasters.Menus.Displays.Animations;

namespace Tests.ChipMasters.Menus.Displays.Animations
{
    [TestClass]
    public class TestRSequnceAnimation
    {
        // two, first inst conclude, second inst conclude
        // two, first inst conclude, second manual conclude
        // two, first manual conclude, second manual conclude
        // two, first manual conclude, second inst conclude

        [TestMethod]
        public void OneInst()
        {
            object inst = new();
            FakeAnimator<object> a1 = new(instConclude: true);

            IAnimation anim = new RSequenceAnimation<object>(inst, new IAnimator<object>[] { a1 });

            Assert.IsTrue(anim.IsFinished);
            Assert.AreEqual(1, a1._RetList_.Count);
        } // end OneInst()

        [TestMethod]
        public void OneManu()
        {
            object inst = new();
            FakeAnimator<object> a1 = new(instConclude: false);

            IAnimation anim = new RSequenceAnimation<object>(inst, new IAnimator<object>[] { a1 });

            Assert.IsFalse(anim.IsFinished);
            Assert.AreEqual(1, a1._RetList_.Count);

            a1._RetList_[0].Conclude();

            Assert.IsTrue(anim.IsFinished);
            Assert.AreEqual(1, a1._RetList_.Count);
        } // end OneManu()

        [TestMethod]
        public void TwoInstInst()
        {
            object inst = new();
            FakeAnimator<object> a1 = new(instConclude: true);
            FakeAnimator<object> a2 = new(instConclude: true);

            IAnimation anim = new RSequenceAnimation<object>(inst, new IAnimator<object>[] { a1, a2 });

            Assert.IsTrue(anim.IsFinished);
            Assert.AreEqual(1, a1._RetList_.Count);
            Assert.AreEqual(1, a2._RetList_.Count);
        } // end TwoInstInst()

        [TestMethod]
        public void TwoInstManu()
        {
            object inst = new();
            FakeAnimator<object> a1 = new(instConclude: true);
            FakeAnimator<object> a2 = new(instConclude: false);

            IAnimation anim = new RSequenceAnimation<object>(inst, new IAnimator<object>[] { a1, a2 });

            Assert.IsFalse(anim.IsFinished);
            Assert.AreEqual(1, a1._RetList_.Count);
            Assert.AreEqual(1, a2._RetList_.Count);

            a2._RetList_[0].Conclude();

            Assert.IsTrue(anim.IsFinished);
            Assert.AreEqual(1, a1._RetList_.Count);
            Assert.AreEqual(1, a2._RetList_.Count);
        } // end TwoInstManu()

        [TestMethod]
        public void TwoManuManu()
        {
            object inst = new();
            FakeAnimator<object> a1 = new(instConclude: false);
            FakeAnimator<object> a2 = new(instConclude: false);

            IAnimation anim = new RSequenceAnimation<object>(inst, new IAnimator<object>[] { a1, a2 });

            Assert.IsFalse(anim.IsFinished);
            Assert.AreEqual(1, a1._RetList_.Count);
            Assert.AreEqual(0, a2._RetList_.Count);

            a1._RetList_[0].Conclude();

            Assert.IsFalse(anim.IsFinished);
            Assert.AreEqual(1, a1._RetList_.Count);
            Assert.AreEqual(1, a2._RetList_.Count);

            a2._RetList_[0].Conclude();

            Assert.IsTrue(anim.IsFinished);
            Assert.AreEqual(1, a1._RetList_.Count);
            Assert.AreEqual(1, a2._RetList_.Count);
        } // end TwoInstManu()

        [TestMethod]
        public void TwoManuInst()
        {
            object inst = new();
            FakeAnimator<object> a1 = new(instConclude: false);
            FakeAnimator<object> a2 = new(instConclude: true);

            IAnimation anim = new RSequenceAnimation<object>(inst, new IAnimator<object>[] { a1, a2 });

            Assert.IsFalse(anim.IsFinished);
            Assert.AreEqual(1, a1._RetList_.Count);
            Assert.AreEqual(0, a2._RetList_.Count);

            a1._RetList_[0].Conclude();

            Assert.IsTrue(anim.IsFinished);
            Assert.AreEqual(1, a1._RetList_.Count);
            Assert.AreEqual(1, a2._RetList_.Count);
        } // end TwoInstManu()

    } // end class
} // end namespace