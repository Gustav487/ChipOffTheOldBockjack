using ChipMasters.Menu.Displays.Animations;
using Fakes.ChipMasters.Menus.Displays.Animations;

namespace Tests.ChipMasters.Menus.Displays.Animations
{
    public class TestRSequenceAnimator
    {
        [TestClass]
        public class Valid
        {
            [TestMethod]
            public void Animate()
            {
                object inst = new();
                FakeAnimator<object> a1 = new(instConclude: false);
                FakeAnimator<object> a2 = new(instConclude: false);
                IAnimator<object> anim = new RSequenceAnimator<object>(new IAnimator<object>[] { a1, a2 });

                IAnimation a = anim.Animate(inst);
                Assert.IsInstanceOfType(a, typeof(RSequenceAnimation<object>));

                a1._RetList_[0].Conclude();
                Assert.IsTrue(a1._RetList_[0].IsFinished);
                Assert.IsFalse(a.IsFinished);

                a2._RetList_[0].Conclude();
                Assert.IsTrue(a2._RetList_[0].IsFinished);
                Assert.IsTrue(a.IsFinished);
            } // end Animate()
        } // end inner class Valid
    } // end class
} // end namespace
