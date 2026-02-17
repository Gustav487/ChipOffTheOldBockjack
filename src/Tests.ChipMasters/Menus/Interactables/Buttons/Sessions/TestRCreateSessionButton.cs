using ChipMasters.Cards;
using ChipMasters.Games.BetHandlers;
using ChipMasters.Menu.Interactables.Buttons.Sessions;
using ChipMasters.User;
using Fakes.ChipMasters.Cards;
using Fakes.ChipMasters.Games.Sessions.Providers;
using Fakes.ChipMasters.Menus.BetSelections;
using Fakes.ChipMasters.Users;

namespace Tests.ChipMasters.Menus.Interactables.Buttons.Sessions
{
    public static class TestRCreateSessionButton
    {
        private static readonly IDeck FAKE_DECK = new FakeDeck(
            new FakeCard(ECardRank.Two, ECardSuit.Clubs),
            new FakeCard(ECardRank.Two, ECardSuit.Clubs),
            new FakeCard(ECardRank.Two, ECardSuit.Clubs),
            new FakeCard(ECardRank.Two, ECardSuit.Clubs)
            );

        [TestClass]
        public class Valid
        {
            [TestMethod]
            [DataRow(0, 0)]
            [DataRow(0, 1231)]
            [DataRow(442, 12310)]
            public void DetachesAfterBetSelection(int minBet, int maxBet)
            {
                IUser f = new FakeUser();
                FakeBetSelectionMenu bsm = new FakeBetSelectionMenu();
                RCreateSessionButton cbm = new(
                    f,
                    new FakeSessionProvider(),
                    new VBetRange(minBet, maxBet),
                    bsm);

                int matchesCreatedCount = 0;                         // setup, and validate setup
                cbm.OnSessionCreated += () => matchesCreatedCount += 1;
                Assert.AreEqual(false, bsm.Visible);
                Assert.AreEqual(0, matchesCreatedCount);
                Assert.AreEqual(null, f.Session);

                bsm.Submit();                  // bet selection submitted without awaiting
                Assert.AreEqual(false, bsm.Visible);
                Assert.AreEqual(0, matchesCreatedCount);
                Assert.AreEqual(null, f.Session);

                cbm.Press();            // await bet selection
                Assert.AreEqual(true, bsm.Visible);
                Assert.AreEqual(0, matchesCreatedCount);
                Assert.AreEqual(new VBetRange(minBet, maxBet), bsm.Range);
                bsm.Submit();           // submit bet selection and confirm was processed
                Assert.AreEqual(false, bsm.Visible);
                Assert.AreEqual(1, matchesCreatedCount);
                Assert.AreNotEqual(null, f.Session);

                bsm.Submit();                   // submit bet selection again and confirm nothing happens to creation menu
                Assert.AreEqual(false, bsm.Visible);
                Assert.AreEqual(1, matchesCreatedCount);
                Assert.AreNotEqual(null, f.Session);
            } // end DetachesAfterBetSelection()
        } // end inner class Valid
    } // end class
} // end namespace