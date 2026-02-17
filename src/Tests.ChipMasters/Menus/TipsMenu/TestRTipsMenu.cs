using ChipMasters.Cards;
using ChipMasters.Games.Appraisers;
using ChipMasters.Games.BetHandlers;
using ChipMasters.Games.Sessions;
using ChipMasters.GodotWrappers;
using ChipMasters.Menu.TipsMenu;
using ChipMasters.User;
using Fakes.ChipMasters.Cards;
using Fakes.ChipMasters.Games.Appraisers;
using Fakes.ChipMasters.Games.Matches;
using Fakes.ChipMasters.Games.Matches.Providers;
using Fakes.ChipMasters.GodotWrappers;
using Fakes.ChipMasters.Users;

namespace Tests.ChipMasters.Menus.TipsMenu
{
    public static class TestRTipsMenu
    {
        [TestClass]
        public class Valid
        {
            [TestMethod]
            [DataRow(8, 15, 10, true, false)]  // 8 -> 10, same text
            [DataRow(8, 15, 11, true, false)]  // 8 -> 11, same text
            [DataRow(11, 10, 12, true, true)]  // 11 -> 12, changed text
            [DataRow(12, 7, 16, true, false)]  // 12 -> 16, same text
            [DataRow(16, 6, 17, true, true)]   // 16 -> 17, changed text
            [DataRow(17, 8, 20, true, false)]  // 17 -> 20, same text
            [DataRow(20, 5, 21, true, true)]   // 20 -> 21, changed text
            [DataRow(21, 5, 22, true, true)]   // 21 -> 22, changed text
            public void TipsLabelChanged(
                int playerTotal1, int dealerTotal, int playerTotal2,
                bool? expectedLabelChange1, bool? expectedLabelChange2)
            {
                IUser u = new RUser();
                ILabel l = new FakeLabel();
                ITipsMenu t = new RTipsMenu(u, new FakeControl(), new FakeCheckBox(), l, new FakeButton());

                l.Text = "Test";
                string startingLabelText = l.Text;
                t.ShowTip(false, playerTotal1, dealerTotal);

                Assert.AreEqual(expectedLabelChange1, startingLabelText != l.Text);

                string secondLabelText = l.Text;
                t.ShowTip(false, playerTotal2, dealerTotal);

                Assert.AreEqual(expectedLabelChange2, secondLabelText != l.Text);
            } // end TipsLabelChanged()

            [TestMethod]
            [DataRow(21, 10, false, "You have the best possible hand! Press the Stand button.")]
            [DataRow(21, 10, true, "Congratulations! You got a Blackjack!")]
            public void BlackjackScenario(int playerTotal, int dealerTotal, bool isMatchConcluded, string expectedLabel)
            {
                IUser u = new RUser();
                ILabel l = new FakeLabel();
                ITipsMenu t = new RTipsMenu(u, new FakeControl(), new FakeCheckBox(), l, new FakeButton());

                t.ShowTip(isMatchConcluded, playerTotal, dealerTotal);

                Assert.AreEqual(expectedLabel, l.Text);
            } // end BlackjackScenario()

            [TestMethod]
            [DataRow(22, "Oh no, looks like you have bust. Try not to go over 21.")]
            [DataRow(16854, "Oh no, looks like you have bust. Try not to go over 21.")]
            public void BustScenario(int playerTotal, string expectedLabel)
            {
                IUser u = new RUser();
                ILabel l = new FakeLabel();
                ITipsMenu t = new RTipsMenu(u, new FakeControl(), new FakeCheckBox(), l, new FakeButton());

                t.ShowTip(true, playerTotal, 10);

                Assert.AreEqual(expectedLabel, l.Text);
            } // end BustScenario()

            [TestMethod]
            public void OpenWithToggleNotPressed()
            {
                IControl c = new FakeControl();
                ICheckBox cb = new FakeCheckBox { ButtonPressed = false };
                ITipsMenu t = new RTipsMenu(new FakeUser(), c, cb, new FakeLabel(), new FakeButton());

                t.Open(); // check box is not pressed, shouldn't open menu

                Assert.IsFalse(c.Visible);
            } // end OpenWithToggleNotPressed()

            [TestMethod]
            public void Dispose()
            {
                IUser u = new RUser();
                VHandAppraisal p = new(new int[] { 10 }, EHandState.Neutral);
                VHandAppraisal d = new(new int[] { 14 }, EHandState.Neutral);
                FakeMatch m = new FakeMatch(appraiser: new FakeOrdinalAppraiser(p, d));
                u.Session = new RStandardSession(50, new FakeMatchProvider((x) => m), new RStandardBetHandler(u.Wallet, new RInfiniteWallet()), new FakeMetrics());

                ILabel l = new FakeLabel();
                ICheckBox c = new FakeCheckBox();
                ITipsMenu t = new RTipsMenu(u, new FakeControl(), c, l, new FakeButton());

                c.ButtonPressed = true;
                t.Open();

                Assert.AreEqual("Press the Hit button to draw another card.", l.Text);

                t.Dispose();

                u.Session.Match.PlayerHand.AddCard(new FakeCard(ECardRank.Ace, ECardSuit.Clubs)); // label should update if subscribed to OnCardAdded
                Assert.AreEqual("Press the Hit button to draw another card.", l.Text); // label does not update
            } // end Dispose()

        } // end inner class Invalid
    } // end class
} // end namespace