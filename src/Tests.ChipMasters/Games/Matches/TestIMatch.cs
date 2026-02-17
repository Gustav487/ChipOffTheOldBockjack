using ChipMasters.Games.Matches;
using GSR.EnDecic;

namespace Tests.ChipMasters.Games.Matches
{
    public class TestIMatch
    {
        public static class EnDec
        {
            public static readonly IEnDec<IMatch> ENDEC = IMatch.ENDEC;

            [TestClass]
            public class Valid
            {
                /*                [TestMethod]
                                public void Encode1()
                                    => Assert.AreEqual(
                                        "{" +
                                            "\"deck\":{\"prototype\":[],\"state\":[]}," +
                                            "\"dealer_hand\":[\"three_of_hearts\",{\"type\":\"nine_of_clubs\",\"data\":{\"veiled\":true}}]," +
                                            "\"player_hand\":[\"four_of_hearts\",\"six_of_spades\"]," +
                                            "\"bet\":99," +
                                            $"\"concluded\":false" +
                                        "}",
                                        ENDEC
                                        .Encode(JsonStreamCoder.INSTANCE, new RMatch(new RDeck(
                                            new ICard[] { },
                                            new ICard[]
                                            {
                                                SCardTypes.REGISTER["four_of_hearts"].Instantiate(),
                                                SCardTypes.REGISTER["three_of_hearts"].Instantiate(),
                                                SCardTypes.REGISTER["six_of_spades"].Instantiate(),
                                                SCardTypes.REGISTER["nine_of_clubs"].Instantiate()
                                            }), 99), CoderSettings.DEFAULT)
                                        .ToString(JsonFormatting.COMPRESSED));

                                [TestMethod]
                                public void Decode1()
                                {
                                    IMatch match = ENDEC
                                        .Decode(JsonStreamCoder.INSTANCE,
                                            JsonElement.ParseJson(
                                                "{" +
                                                    "\"deck\":{\"prototype\":[],\"state\":[]}," +
                                                    "\"dealer_hand\":[\"three_of_hearts\",\"nine_of_clubs\"]," +
                                                    "\"player_hand\":[\"four_of_hearts\",\"six_of_spades\"]," +
                                                    "\"bet\":99," +
                                                    $"\"concluded\":false" +
                                                "}"),
                                            CoderSettings.DEFAULT);

                                    Assert.AreEqual(99, match.Bet);
                                    Assert.AreEqual(2, match.DealerHand.Count);
                                    Assert.AreEqual(SCardTypes.REGISTER["three_of_hearts"].Instantiate(), match.DealerHand[0]);
                                    Assert.AreEqual(SCardTypes.REGISTER["nine_of_clubs"].Instantiate(), match.DealerHand[1]);
                                    Assert.AreEqual(2, match.PlayerHand.Count);
                                    Assert.AreEqual(SCardTypes.REGISTER["four_of_hearts"].Instantiate(), match.PlayerHand[0]);
                                    Assert.AreEqual(SCardTypes.REGISTER["six_of_spades"].Instantiate(), match.PlayerHand[1]);
                                } // end Decode1()*/

            } // end inner class Valid
        } // end inner class EnDec
    } // end class
} // end namespace