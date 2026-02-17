using ChipMasters.Menu.Displays.Animations;

namespace Tests.ChipMasters.Menus.Displays.Animations
{
    public class TestRDelayAnimation
    {
        [TestClass]
        public class Valid
        {
            [TestMethod]
            public async Task Finish()
            {
                IAnimation a = new RDelayAnimation(100);
                await Task.Delay(200); // Allow enough time for animation to finish
                Assert.AreEqual(true, a.IsFinished);
            } // end Finish()
        } // end inner class Valid
    } // end class
} // end namespace
