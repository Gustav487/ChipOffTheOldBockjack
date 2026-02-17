using ChipMasters.Games.Appraisers;
using ChipMasters.Games.BetHandlers;
using ChipMasters.User;
using Fakes.ChipMasters.Games.Appraisers;
using Fakes.ChipMasters.Games.Matches;

namespace Tests.ChipMasters.Games.BetHandlers
{
    public static class TestRStandardBetHandler
    {
        [TestClass]
        public class Valid
        {
            [TestMethod]
            [DataRow(100, 0, 20, 0, EHandState.Neutral, 0, EHandState.Neutral, 100, 0)] // tiees
            [DataRow(100, 90, 20, 23, EHandState.Neutral, 23, EHandState.Neutral, 100, 90)]
            [DataRow(100, -80, 20, 23, EHandState.Blackjack, 24, EHandState.Blackjack, 100, -80)]

            [DataRow(100, 10, 20, 239, EHandState.Neutral, 24, EHandState.Neutral, 120, -10)] // player numeric win
            [DataRow(100, 1000, 20, 239, EHandState.Neutral, 2400, EHandState.Neutral, 80, 1020)] // dealer numberic win

            [DataRow(100, 0, 20, 239, EHandState.Bust, 24, EHandState.Neutral, 80, 20)] // player bust
            [DataRow(100, -10, 20, 23, EHandState.Neutral, 24, EHandState.Bust, 120, -30)] // dealer bust

            [DataRow(100, 120, 20, 239, EHandState.Blackjack, 24, EHandState.Neutral, 140, 80)] // player blackjack win
            [DataRow(100, 30, 20, 239, EHandState.Neutral, 24, EHandState.Blackjack, 80, 50)] // dealer blackjack win
            [DataRow(20, 87, 20, 239, EHandState.Neutral, 24, EHandState.Blackjack, 0, 107)] // dealer blackjack win triggers bankrupted
            public void Payout(int pChips, int dChips, int bet,
            int pHandVal, EHandState pHandState,
            int dHandVal, EHandState dHandState,
            int exPChips, int exDChips)
            {
                // Arrange 
                IWallet pW = new RWallet(pChips);
                IWallet dW = new RWallet(dChips);
                VHandAppraisal p = new(new int[] { pHandVal }, pHandState);
                VHandAppraisal d = new(new int[] { dHandVal }, dHandState);
                FakeMatch m = new(bet: bet, appraiser: new FakeOrdinalAppraiser(p, d));

                //Act
                new RStandardBetHandler(pW, dW).Payout(m);

                // Assert
                Assert.AreEqual(exPChips, pW.Chips);
                Assert.AreEqual(exDChips, dW.Chips);
            } // end MatchConclusion()

        } // end inner class Valid()
    } // end class
} // end namespace