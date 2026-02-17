using ChipMasters.GodotWrappers;
using ChipMasters.Menu.Displays.Animations;
using ChipMasters.Menu.Displays.Animations.Controls;
using Fakes.ChipMasters.GodotWrappers;

namespace Tests.ChipMasters.Menus.Displays.Animations.Controls
{
    public class TestRVisibilityAnimator
    {
        [TestClass]
        public class Valid
        {
            [TestMethod]
            [DataRow(true)]
            [DataRow(false)]
            public void Animate(bool setTo)
            {
                IControl c = new FakeControl();
                IAnimator<IControl> ac = new RVisibilityAnimator(setTo);
                IAnimation a = ac.Animate(c);
                Assert.AreEqual(IAnimation.EMPTY, a);
                Assert.AreEqual(setTo, c.Visible);
            } // end Animate()
        } // end inner class Valid
    } // end class
} // end namespace
