using ChipMasters.Games.Appraisers;
using ChipMasters.Games.BetHandlers;
using ChipMasters.Games.Sessions;
using ChipMasters.Items;
using ChipMasters.Menu.Avatars;
using ChipMasters.User;
using Fakes.ChipMasters.Games.Appraisers;
using Fakes.ChipMasters.Games.Matches;
using Fakes.ChipMasters.Games.Matches.Providers;
using Fakes.ChipMasters.Users;

namespace Tests.ChipMasters.User
{
    [TestClass]
    public class TestRUser
    {
        [TestMethod]
        public void Bankrupted()
        {
            IUser user = new RUser();
            Assert.AreEqual(SIWalletExtensions.DEFAULT_CHIPS, user.Wallet.Chips);

            user.Wallet.Chips = 1;
            Assert.AreEqual(1, user.Wallet.Chips);

            user.Wallet.Chips = 0;
            Assert.AreEqual(0, user.Wallet.Chips);

            user.Wallet.Chips = -1;
            Assert.AreEqual(0, user.Wallet.Chips);
        } // end Bankrupted()

        [TestMethod]
        public void ConstructedBankrupt()
        {
            IUser user = new RUser(0, null, new RMetrics(), new RInventory(), new RAssetSelection(), Array.Empty<IAchievement>(), new RUserSettings(), new RAvatar());
            Assert.AreEqual(0, user.Wallet.Chips);
        } // end ConstructedBankrupt()


        [TestMethod]
        [DataRow(100, 20, 0, EHandState.Neutral, 0, EHandState.Neutral, 100)] // tiees
        [DataRow(100, 20, 23, EHandState.Neutral, 23, EHandState.Neutral, 100)]
        [DataRow(100, 20, 23, EHandState.Blackjack, 24, EHandState.Blackjack, 100)]

        [DataRow(100, 20, 239, EHandState.Neutral, 24, EHandState.Neutral, 120)] // player numeric win
        [DataRow(100, 20, 239, EHandState.Neutral, 2400, EHandState.Neutral, 80)] // dealer numberic win

        [DataRow(100, 20, 239, EHandState.Bust, 24, EHandState.Neutral, 80)] // player bust
        [DataRow(100, 20, 23, EHandState.Neutral, 24, EHandState.Bust, 120)] // dealer bust

        [DataRow(100, 20, 239, EHandState.Blackjack, 24, EHandState.Neutral, 140)] // player blackjack win
        [DataRow(100, 20, 239, EHandState.Neutral, 24, EHandState.Blackjack, 80)] // dealer blackjack win
        [DataRow(20, 20, 239, EHandState.Neutral, 24, EHandState.Blackjack, 0)] // dealer blackjack win
        public void MatchConclusion(int chips, int bet,
            int pHandVal, EHandState pHandState,
            int dHandVal, EHandState dHandState,
            int expectation)
        {
            // Arrange 
            IUser user = new RUser();
            user.Wallet.Chips = chips;
            VHandAppraisal p = new(new int[] { pHandVal }, pHandState);
            VHandAppraisal d = new(new int[] { dHandVal }, dHandState);
            FakeMatch m = new(bet: bet, appraiser: new FakeOrdinalAppraiser(p, d, p, d));
            user.Session = new RStandardSession(bet, new FakeMatchProvider((x) => m), new RStandardBetHandler(user.Wallet, new RInfiniteWallet()), new FakeMetrics());

            //Act
            m.Conclude();

            // Assert
            Assert.AreEqual(expectation, user.Wallet.Chips);
        } // end MatchConclusion()

        [TestMethod]
        public void ChipChangeRecorded()
        {
            IUser u = new RUser();
            Assert.AreEqual(1, u.Metrics.ChipHistory.Count);
            Assert.AreEqual(u.Wallet.Chips, u.Metrics.ChipHistory[0].Count);

            u.Wallet.Chips = 1000;

            Assert.AreEqual(2, u.Metrics.ChipHistory.Count);
            Assert.AreEqual(1000, u.Metrics.ChipHistory[^1].Count);
        } // end ChipChangeRecorded()

    } // end class
} // end namespace