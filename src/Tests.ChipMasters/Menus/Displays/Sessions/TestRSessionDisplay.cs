using ChipMasters.Games.Matches;
using ChipMasters.Games.Sessions;
using ChipMasters.Menu.Displays;
using ChipMasters.Menu.Displays.ISessions;
using Fakes.ChipMasters.Games.Matches;
using Fakes.ChipMasters.Games.Sessions;
using Fakes.ChipMasters.Menus.Displays;

namespace Tests.ChipMasters.Menus.Displays.Sessions
{
    [TestClass]
    public class TestRSessionDisplay
    {
        [TestMethod]
        public void Dispose()
        {
            IDisplay<IMatch> md = new FakeDisplay<IMatch>();
            IDisplay<ISession> sd = new RSessionDisplay(md);

            FakeSession s = new(new FakeMatch(), new FakeMatch());
            sd.Display = s;

            Assert.AreEqual(s.Match, md.Display);

            sd.Dispose();
            s.PlayAgain(0);

            Assert.AreNotEqual(s.Match, md.Display);
        } // end Dispose()

        [TestMethod]
        public void Set()
        {
            IDisplay<IMatch> md = new FakeDisplay<IMatch>();
            IDisplay<ISession> sd = new RSessionDisplay(md);

            Assert.IsNull(md.Display); // initial

            IMatch m1 = new FakeMatch();
            IMatch m2 = new FakeMatch();
            IMatch m3 = new FakeMatch();
            FakeSession s = new(m1, m2, m3);
            sd.Display = s;
            Assert.AreEqual(m1, md.Display); // setting the session initializes match from session

            s.PlayAgain(0);
            Assert.AreEqual(m2, md.Display); // event causes match display update.

            sd.Display = null;
            Assert.IsNull(md.Display); // removing session remove match from being displayed.

            s.PlayAgain(0);
            Assert.IsNull(md.Display); // session match change isn't being listened to after removal
        } // end Set()
    } // end class
} // end namespace