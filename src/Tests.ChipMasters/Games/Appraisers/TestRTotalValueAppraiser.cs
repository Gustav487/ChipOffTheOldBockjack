using ChipMasters.Cards;
using ChipMasters.Games.Appraisers;
using ChipMasters.Games.Hands;
using GSR.EnDecic;
using GSR.EnDecic.Jsonic;
using GSR.Jsonic;
using GSR.Jsonic.Formatting;

namespace Tests.ChipMasters.Games.Appraisers
{
    [TestClass]
    public class TestRTotalValueAppraiser
    {
        [TestClass]
        public class Valid
        {
            [TestMethod]
            [DataRow(21, EHandState.Neutral, 0, false)]
            [DataRow(43, EHandState.Neutral, 0, false)]
            [DataRow(1, EHandState.Neutral, 0, false)]
            [DataRow(0, EHandState.Blackjack, 0, false)]
            [DataRow(-1, EHandState.Bust, 0, false)]
            [DataRow(21, EHandState.Neutral, 11, false, 11, "ace_of_spades", false)]
            [DataRow(21, EHandState.Neutral, 12, false, 1, 11, "ace_of_spades", false, "ace_of_spades", false)]
            [DataRow(21, EHandState.Blackjack, 21, false, 10, 11, "queen_of_hearts", false, "ace_of_spades", false)]
            [DataRow(21, EHandState.Blackjack, 21, false, 10, 10, 1, "queen_of_hearts", false, "queen_of_hearts", false, "ace_of_spades", false)]
            [DataRow(21, EHandState.Neutral, 13, false, 2, 1, 9, 1, "two_of_clubs", false, "ace_of_diamonds", false, "nine_of_clubs", false, "ace_of_spades", false)]
            [DataRow(21, EHandState.Bust, 24, false, 7, 9, 8, "seven_of_clubs", false, "nine_of_diamonds", false, "eight_of_clubs", false)]

            [DataRow(21, EHandState.Neutral, 0, true)]
            [DataRow(21, EHandState.Neutral, 11, true, 11, "ace_of_spades", false)]
            [DataRow(21, EHandState.Neutral, 12, true, 1, 11, "ace_of_spades", false, "ace_of_spades", false)]
            [DataRow(21, EHandState.Blackjack, 21, true, 10, 11, "queen_of_hearts", false, "ace_of_spades", false)]
            [DataRow(21, EHandState.Blackjack, 21, true, 10, 10, 1, "queen_of_hearts", false, "queen_of_hearts", false, "ace_of_spades", false)]
            [DataRow(21, EHandState.Neutral, 13, true, 2, 1, 9, 1, "two_of_clubs", false, "ace_of_diamonds", false, "nine_of_clubs", false, "ace_of_spades", false)]
            [DataRow(21, EHandState.Bust, 24, true, 7, 9, 8, "seven_of_clubs", false, "nine_of_diamonds", false, "eight_of_clubs", false)]

            [DataRow(21, EHandState.Unknown, 17, false, 9, 8, "seven_of_clubs", true, "nine_of_diamonds", false, "eight_of_clubs", false)]
            [DataRow(21, EHandState.Bust, 24, true, 7, 9, 8, "seven_of_clubs", true, "nine_of_diamonds", false, "eight_of_clubs", false)]
            public void AppraiseHand(int maxValue, EHandState exState, int exTotalValue, bool includeHidden, params object[] args)
                => SAppraiserTestUtil.TestAppraiseHand(new RTotalValueAppraiser(maxValue), exState, exTotalValue, includeHidden, args);
        } // end inner class Valid

        public static class ENDEC
        {
            [TestClass]
            public class Valid
            {
                [TestMethod]
                [DataRow(1, $"{{\"{RTotalValueAppraiser.MAX_VALUE_KEY}\":1}}")]
                public void Encode(int max, string ex)
                {
                    JsonElement stream = RTotalValueAppraiser.ENDEC.Encode(
                        JsonStreamCoder.INSTANCE,
                        new RTotalValueAppraiser(max),
                        CoderSettings.DEFAULT);

                    Assert.AreEqual(ex, stream.ToString(JsonFormatting.COMPRESSED));
                } // end Encode

                [TestMethod]
                [DataRow($"{{\"{RTotalValueAppraiser.MAX_VALUE_KEY}\":23}}", 23)]
                [DataRow($"{{\"{RTotalValueAppraiser.MAX_VALUE_KEY}\":904}}", 904)]
                public void Decode(string json, int exMax)
                {
                    RTotalValueAppraiser data = RTotalValueAppraiser.ENDEC.Decode(
                        JsonStreamCoder.INSTANCE,
                        JsonElement.ParseJson(json),
                        CoderSettings.DEFAULT);

                    int tens = exMax / 10;
                    int r = exMax % 10;
                    IHand h = new RHand();

                    for (int i = 0; i < tens; i++)
                        h.AddCard(new RCard(ECardRank.Ten, ECardSuit.Clubs, veiled: false));
                    if (r > 0)
                        h.AddCard(new RCard((ECardRank)r, ECardSuit.Spades));

                    Assert.AreEqual(EHandState.Blackjack, data.AppraiseHand(h, true).State);

                    h.AddCard(new RCard(ECardRank.Ace, ECardSuit.Hearts));
                    Assert.AreEqual(EHandState.Bust, data.AppraiseHand(h, true).State);
                } // end Encode
            } // end ENDEC.Valid()

        } // end ENDEC
    } // end class
} // end namespace