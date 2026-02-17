using ChipMasters.Cards;
using ChipMasters.Games.Matches;
using ChipMasters.Games.Matches.Providers;
using Fakes.ChipMasters.Cards;
using GSR.EnDecic;
using GSR.EnDecic.Jsonic;
using GSR.Jsonic;

namespace Tests.ChipMasters.Games.Matches.Providers
{
    public class TestRStandardMatchProvider
    {
        [TestClass]
        public class Valid
        {
            [TestMethod]
            public void Create()
            {
                ICard[] c = new ICard[]
                {
                    new FakeCard(ECardRank.Seven, ECardSuit.Hearts),   // Player's first card
                    new FakeCard(ECardRank.Five, ECardSuit.Spades),   // Dealer's first card
                    new FakeCard(ECardRank.Six, ECardSuit.Diamonds), // Player's second card
                    new FakeCard(ECardRank.Eight, ECardSuit.Clubs),    // Dealer's second card
                    new FakeCard(ECardRank.Eight, ECardSuit.Hearts),   // Player hits and gets a 9
                    new FakeCard(ECardRank.Ace, ECardSuit.Hearts),   // 14
                    new FakeCard(ECardRank.Ace, ECardSuit.Hearts),   // 15
                    new FakeCard(ECardRank.Ace, ECardSuit.Hearts),   // 16
                    new FakeCard(ECardRank.Ace, ECardSuit.Hearts)   // 17
                };

                var pd = new FakeDeck(c);
                var dd = new FakeDeck(c);

                IMatchProvider mp = new RStandardMatchProvider(pd, dd);
                IMatch m = mp.Create(50);

                Assert.AreEqual(true, pd.WasShuffled);
                Assert.AreEqual(true, dd.WasShuffled);
                Assert.IsInstanceOfType(m, typeof(RMatch));
                Assert.AreEqual(50, m.Bet);
            } // end Create()

            [TestMethod]
            public void Decode()
            {
                IMatchProvider provider = RStandardMatchProvider.ENDEC
                    .Decode(JsonStreamCoder.INSTANCE,
                        JsonElement.ParseJson(
                            "{" +
                                "\"player_deck\":{\"prototype\":[],\"state\":[]}," +
                                "\"dealer_deck\":{\"prototype\":[],\"state\":[]}" +
                            "}"),
                        CoderSettings.DEFAULT);

                Assert.IsNotNull(provider);
            } // end Decode()
        } // end inner class Valid
    } // end class
} // end namespace
