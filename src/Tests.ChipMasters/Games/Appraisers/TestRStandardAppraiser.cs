using ChipMasters.Games.Appraisers;

namespace Tests.ChipMasters.Games.Appraisers
{
    public static class TestRStandardAppraiser
    {
        [TestClass]
        public class Valid
        {
            [TestMethod]
            [DataRow(EHandState.Neutral, 0, false)]
            [DataRow(EHandState.Neutral, 11, false, 11, "ace_of_spades", false)]
            [DataRow(EHandState.Neutral, 12, false, 1, 11, "ace_of_spades", false, "ace_of_spades", false)]
            [DataRow(EHandState.Blackjack, 21, false, 10, 11, "queen_of_hearts", false, "ace_of_spades", false)]
            [DataRow(EHandState.Neutral, 21, false, 10, 10, 1, "queen_of_hearts", false, "queen_of_hearts", false, "ace_of_spades", false)]
            [DataRow(EHandState.Neutral, 13, false, 2, 1, 9, 1, "two_of_clubs", false, "ace_of_diamonds", false, "nine_of_clubs", false, "ace_of_spades", false)]
            [DataRow(EHandState.Bust, 24, false, 7, 9, 8, "seven_of_clubs", false, "nine_of_diamonds", false, "eight_of_clubs", false)]

            [DataRow(EHandState.Neutral, 0, true)]
            [DataRow(EHandState.Neutral, 11, true, 11, "ace_of_spades", false)]
            [DataRow(EHandState.Neutral, 12, true, 1, 11, "ace_of_spades", false, "ace_of_spades", false)]
            [DataRow(EHandState.Blackjack, 21, true, 10, 11, "queen_of_hearts", false, "ace_of_spades", false)]
            [DataRow(EHandState.Neutral, 21, true, 10, 10, 1, "queen_of_hearts", false, "queen_of_hearts", false, "ace_of_spades", false)]
            [DataRow(EHandState.Neutral, 13, true, 2, 1, 9, 1, "two_of_clubs", false, "ace_of_diamonds", false, "nine_of_clubs", false, "ace_of_spades", false)]
            [DataRow(EHandState.Bust, 24, true, 7, 9, 8, "seven_of_clubs", false, "nine_of_diamonds", false, "eight_of_clubs", false)]

            [DataRow(EHandState.Unknown, 17, false, 9, 8, "seven_of_clubs", true, "nine_of_diamonds", false, "eight_of_clubs", false)]
            [DataRow(EHandState.Bust, 24, true, 7, 9, 8, "seven_of_clubs", true, "nine_of_diamonds", false, "eight_of_clubs", false)]
            public void AppraiseHand(EHandState exState, int exTotalValue, bool includeHidden, params object[] args)
                => SAppraiserTestUtil.TestAppraiseHand(new RStandardAppraiser(), exState, exTotalValue, includeHidden, args);
        } // end inner class Valid
    } // end class
} // end namespace