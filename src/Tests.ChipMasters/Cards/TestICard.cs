using ChipMasters.Cards;
using GSR.EnDecic;
using GSR.EnDecic.Jsonic;
using GSR.Jsonic.Formatting;

namespace Tests.ChipMasters.Cards
{
    public static class TestICard
    {
        public static class ENDEC
        {
            [TestClass]
            public class Valid
            {
                [TestMethod]
                [DataRow("\"ace_of_spades\"", ECardRank.Ace, ECardSuit.Spades, false)]
                [DataRow("{\"type\":\"ace_of_spades\",\"data\":{\"veiled\":true}}", ECardRank.Ace, ECardSuit.Spades, true)]
                public void Encode(string exJson, ECardRank rank, ECardSuit suit, bool veiled)
                {
                    ICard card = new RCard(rank, suit, veiled: veiled);
                    string json = ICard.ENDEC
                        .Encode(JsonStreamCoder.INSTANCE, card, CoderSettings.DEFAULT)
                        .ToString(JsonFormatting.COMPRESSED);

                    Assert.AreEqual(exJson, json);
                } // end Encode()

            } // end inner class Valid
        } // end inner class ENDEC
    } // end class
} // end namespace