using ChipMasters.Games.Matches;
using ChipMasters.Menu.Displays;
using ChipMasters.Menu.Displays.Chips;
using ChipMasters.Menu.Displays.Hand;
using ChipMasters.Menu.Displays.Hand.Appraisal;
using ChipMasters.Menu.Displays.Matches;
using Fakes.ChipMasters.Games.Appraisers;
using Fakes.ChipMasters.Games.Matches;
using Fakes.ChipMasters.GodotWrappers;
using Fakes.ChipMasters.Menus.Displays.Chips;
using Fakes.ChipMasters.Menus.Displays.Hands;
using Fakes.ChipMasters.Menus.Displays.Hands.Appraisals;

namespace Tests.ChipMasters.Menus.Displays.Matches
{
    [TestClass]
    public sealed class TestRMatchDisplay
    {
        [TestMethod]
        public void Hit()
        {
            FakeButton hb = new();
            IDisplay<IMatch> md = new RMatchDisplay(
                new FakeHandDisplay(), new FakeHandDisplay(),
                new FakeHandAppraisalDisplay(), new FakeHandAppraisalDisplay(),
                new FakeChipDisplay(),
                new FakeButton(), hitButton: hb);

            hb.Press(); // nothing should happen - hard to assert anything here

            FakeMatch fm = new(appraiser: new FakeOrdinalAppraiser());
            md.Display = fm;
            hb.Press();

            Assert.AreEqual(1, fm._HitPresses_);
            Assert.AreEqual(0, fm._StandPresses_);
            hb.Press();

            Assert.AreEqual(2, fm._HitPresses_);
            Assert.AreEqual(0, fm._StandPresses_);
            md.Display = null;
            hb.Press(); // no further changes should occur,  match should no longer be affected by the display

            Assert.AreEqual(2, fm._HitPresses_);
            Assert.AreEqual(0, fm._StandPresses_);
        } // end Hit()

        [TestMethod]
        public void Stand()
        {
            FakeButton sb = new();
            IDisplay<IMatch> md = new RMatchDisplay(
                new FakeHandDisplay(), new FakeHandDisplay(),
                new FakeHandAppraisalDisplay(), new FakeHandAppraisalDisplay(),
                new FakeChipDisplay(),
                standButton: sb, new FakeButton());

            sb.Press(); // nothing should happen - hard to assert anything here

            FakeMatch fm = new(appraiser: new FakeOrdinalAppraiser());
            md.Display = fm;
            sb.Press();

            Assert.AreEqual(0, fm._HitPresses_);
            Assert.AreEqual(1, fm._StandPresses_);
            sb.Press();

            Assert.AreEqual(0, fm._HitPresses_);
            Assert.AreEqual(2, fm._StandPresses_);
            md.Display = null;
            sb.Press(); // no further changes should occur, match should no longer be affected by the display

            Assert.AreEqual(0, fm._HitPresses_);
            Assert.AreEqual(2, fm._StandPresses_);
        } // end Stand()

        [TestMethod]
        public void Dispose()
        {
            FakeButton hb = new();
            FakeButton sb = new();
            IDisplay<IMatch> md = new RMatchDisplay(
                new FakeHandDisplay(), new FakeHandDisplay(),
                new FakeHandAppraisalDisplay(), new FakeHandAppraisalDisplay(),
                new FakeChipDisplay(),
                standButton: sb, hitButton: hb);

            hb.Press(); // nothing should happen - hard to assert anything here
            sb.Press();

            FakeMatch fm = new(appraiser: new FakeOrdinalAppraiser());
            md.Display = fm;
            hb.Press();
            sb.Press();

            Assert.AreEqual(1, fm._HitPresses_);
            Assert.AreEqual(1, fm._StandPresses_);
            hb.Press();
            sb.Press();

            Assert.AreEqual(2, fm._HitPresses_);
            Assert.AreEqual(2, fm._StandPresses_);
            md.Dispose();
            hb.Press(); // further changes should actually occur, desubscribing from buttons could cause an exception if they were disposed of first.
            sb.Press();

            Assert.AreEqual(3, fm._HitPresses_);
            Assert.AreEqual(3, fm._StandPresses_);
        } // end Dispose()

        [TestMethod]
        public void Display()
        {
            IHandDisplay dhd = new FakeHandDisplay();
            IHandDisplay phd = new FakeHandDisplay();
            IHandAppraisalDisplay dhapd = new FakeHandAppraisalDisplay();
            IHandAppraisalDisplay phapd = new FakeHandAppraisalDisplay();
            IChipDisplay bd = new FakeChipDisplay();

            FakeButton hb = new();
            FakeButton sb = new();
            IDisplay<IMatch> md = new RMatchDisplay(
                dealerHandDisplay: dhd, playerHandDisplay: phd,
                dealerHandAppraisalDisplay: dhapd, playerHandAppraisalDisplay: phapd,
                betDisplay: bd,
                standButton: sb, hitButton: hb);

            // initila

            Assert.IsNull(dhd.Hand);
            Assert.IsNull(phd.Hand);
            Assert.IsNull(dhapd.Hand);
            Assert.IsNull(dhapd.Appraiser);
            Assert.IsNull(phapd.Hand);
            Assert.IsNull(phapd.Appraiser);
            Assert.IsNull(bd.Chips);

            FakeMatch fm = new(appraiser: new FakeOrdinalAppraiser()); // change to match
            md.Display = fm;

            Assert.AreEqual(fm.DealerHand, dhd.Hand);
            Assert.AreEqual(fm.PlayerHand, phd.Hand);
            Assert.AreEqual(fm.DealerHand, dhapd.Hand);
            Assert.AreEqual(fm.Appraiser, dhapd.Appraiser);
            Assert.AreEqual(fm.PlayerHand, phapd.Hand);
            Assert.AreEqual(fm.Appraiser, phapd.Appraiser);
            Assert.AreEqual(fm.Bet, bd.Chips);

            md.Display = null; // change match back to null

            Assert.IsNull(dhd.Hand);
            Assert.IsNull(phd.Hand);
            Assert.IsNull(dhapd.Hand);
            Assert.IsNull(dhapd.Appraiser);
            Assert.IsNull(phapd.Hand);
            Assert.IsNull(phapd.Appraiser);
            Assert.IsNull(bd.Chips);

            FakeMatch fm2 = new(appraiser: new FakeOrdinalAppraiser()); // change from unnatural null to a match
            md.Display = fm2;

            Assert.AreEqual(fm2.DealerHand, dhd.Hand);
            Assert.AreEqual(fm2.PlayerHand, phd.Hand);
            Assert.AreEqual(fm2.DealerHand, dhapd.Hand);
            Assert.AreEqual(fm2.Appraiser, dhapd.Appraiser);
            Assert.AreEqual(fm2.PlayerHand, phapd.Hand);
            Assert.AreEqual(fm2.Appraiser, phapd.Appraiser);
            Assert.AreEqual(fm2.Bet, bd.Chips);

            FakeMatch fm3 = new(appraiser: new FakeOrdinalAppraiser()); // change from match to a different match
            md.Display = fm3;

            Assert.AreEqual(fm3.DealerHand, dhd.Hand);
            Assert.AreEqual(fm3.PlayerHand, phd.Hand);
            Assert.AreEqual(fm3.DealerHand, dhapd.Hand);
            Assert.AreEqual(fm3.Appraiser, dhapd.Appraiser);
            Assert.AreEqual(fm3.PlayerHand, phapd.Hand);
            Assert.AreEqual(fm3.Appraiser, phapd.Appraiser);
            Assert.AreEqual(fm3.Bet, bd.Chips);
        } // end Display()

        [TestMethod]
        public void ButtonDisability()
        {
            FakeButton hb = new();
            FakeButton sb = new();
            IDisplay<IMatch> md = new RMatchDisplay(
                new FakeHandDisplay(), new FakeHandDisplay(),
                new FakeHandAppraisalDisplay(), new FakeHandAppraisalDisplay(),
                new FakeChipDisplay(),
                standButton: sb, hitButton: hb);

            Assert.IsTrue(sb.Disabled); // disabled by default
            Assert.IsTrue(hb.Disabled);

            FakeMatch fm = new(isConcluded: true, appraiser: new FakeOrdinalAppraiser());
            md.Display = fm;

            Assert.IsTrue(sb.Disabled); // setting to concluded match keeps them disabled
            Assert.IsTrue(hb.Disabled);

            FakeMatch fm2 = new(isConcluded: false, appraiser: new FakeOrdinalAppraiser());
            md.Display = fm2;

            Assert.IsFalse(sb.Disabled); // setting to live match enables
            Assert.IsFalse(hb.Disabled);

            FakeMatch fm3 = new(isConcluded: true, appraiser: new FakeOrdinalAppraiser());
            md.Display = fm3;

            Assert.IsTrue(sb.Disabled); // setting to concluded disables buttons
            Assert.IsTrue(hb.Disabled);

            md.Display = null;

            Assert.IsTrue(sb.Disabled); // null keeps disabled
            Assert.IsTrue(hb.Disabled);

            FakeMatch fm4 = new(isConcluded: false, appraiser: new FakeOrdinalAppraiser());
            md.Display = fm4;

            Assert.IsFalse(sb.Disabled); // restore for the next case
            Assert.IsFalse(hb.Disabled);

            md.Display = null;

            Assert.IsTrue(sb.Disabled); // to null from enabled disables
            Assert.IsTrue(hb.Disabled);

            FakeMatch fm5 = new(isConcluded: false, appraiser: new FakeOrdinalAppraiser());
            md.Display = fm5;

            Assert.IsFalse(sb.Disabled); // restore for the next case
            Assert.IsFalse(hb.Disabled);

            fm5.Conclude();

            Assert.IsTrue(sb.Disabled); // conclude event disables
            Assert.IsTrue(hb.Disabled);
        } // end ButtonDisability()
    } // end class
} // end namespace