namespace Tests.ChipMasters.User
{
    public static class TestIUser
    {
        /*        public static class ENDEC
                {
                    private static readonly IEnDec<IUser> _ENDEC =
                        IUser.ENDEC(
                            IMatch.ENDEC(
                                IDeck.ENDEC(
                                    ICard.ENDEC(FakeSCardTypes.REGISTER)),
                                IHand.ENDEC(
                                    ICard.ENDEC(FakeSCardTypes.REGISTER))));
                    [TestClass]
                    public class Valid
                    {
                        [TestMethod]
                        [DataRow(0, "{\"chips\":50,\"match\":null,\"metrics\":{\"history\":[],\"chip_history\":[{\"timestamp\":0,\"chip_count\":50}],\"ratio\":{\"wins\":0,\"ties\":0,\"losses\":0}},\"inventory\":{\"items\":[]}}")]
                        [DataRow(7, "{\"chips\":7,\"match\":null,\"metrics\":{\"history\":[],\"chip_history\":[{\"timestamp\":0,\"chip_count\":7}],\"ratio\":{\"wins\":0,\"ties\":0,\"losses\":0}},\"inventory\":{\"items\":[]}}")]
                        public void Encode1(int chips, string expectation)
                        {
                            JsonElement stream = _ENDEC.Encode(
                                JsonStreamCoder.INSTANCE,
                                new RUser(chips, null, new RMetrics(() => DateTimeOffset.FromUnixTimeSeconds(0).UtcDateTime), new RInventory()),
                                CoderSettings.DEFAULT);
                            Assert.AreEqual(expectation, stream.ToString(JsonFormatting.COMPRESSED));
                        } // end Encode1()

                        [TestMethod]
                        [DataRow(50, "{\"chips\":50,\"match\":null,\"metrics\":{\"history\":[],\"chip_history\":[],\"ratio\":{\"wins\":0,\"ties\":0,\"losses\":0}},\"inventory\":{\"items\":[]}}")]
                        [DataRow(7, "{\"chips\":7,\"match\":null,\"metrics\":{\"history\":[],\"chip_history\":[{\"timestamp\":*,\"chip_count\":7}],\"ratio\":{\"wins\":0,\"ties\":0,\"losses\":0}},\"inventory\":{\"items\":[]}}")]
                        public void Decode1(int exChips, string json)
                        {
                            IUser user = _ENDEC.Decode(
                                JsonStreamCoder.INSTANCE,
                                JsonElement.ParseJson(json),
                                CoderSettings.DEFAULT);
                            Assert.AreEqual(exChips, user.Wallet.Chips);
                        }
                        // end Decode1()

                        [TestMethod]
                        public void EncodeConcludedMatch()
                        {
                            JsonElement stream = _ENDEC.Encode(
                                JsonStreamCoder.INSTANCE,
                                new RUser(0, new FakeSession(new FakeMatch(isConcluded: true)), new FakeMetrics(), new RInventory()),
                                CoderSettings.DEFAULT);

                            string actualJson = stream.ToString(JsonFormatting.COMPRESSED);

                            // Use regex to extract timestamp dynamically
                            string timestampPattern = "\"timestamp\":(\\d+)";
                            Match match = Regex.Match(actualJson, timestampPattern);
                            string extractedTimestamp = match.Success ? match.Groups[1].Value : "*";

                            //{ "chips":50,"match":{ "deck":{ "prototype":[],"state":[]},"dealer_hand":[],"player_hand":[],"bet":0,"concluded":true},"metrics":{ "history":[],"chip_history":[{ "timestamp":*,"chip_count":50}],"ratio":{ "wins":0,"ties":0,"losses":0} },"inventory":{ "items":[]} }
                            //{ "chips":50,"match":{ "deck":{ "prototype":[],"state":[]},"dealer_hand":[],"player_hand":[],"bet":0,"concluded":true},"metrics":{ "history":[],"chip_history":[],"ratio":{ "wins":0,"ties":0,"losses":0} },"inventory":{ "items":[]} }
                            // Construct expected JSON with the extracted timestamp
                            string expectedJson = $"{{\"chips\":50,\"match\":{{\"deck\":{{\"prototype\":[],\"state\":[]}}," +
                                $"\"dealer_hand\":[],\"player_hand\":[],\"bet\":0,\"concluded\":true}}," +
                                $"\"metrics\":{{\"history\":[],\"chip_history\":[]," +
                                $"\"ratio\":{{\"wins\":0,\"ties\":0,\"losses\":0}}}}," +
                                $"\"inventory\":{{\"items\":[]}}}}";

                            Assert.AreEqual(expectedJson, actualJson);
                        } // end EncodeConcludedMatch()

                        [TestMethod]
                        public void EncodeOngoingMatch()
                        {
                            IMatch match = new RMatch(
                                new RDeck(
                                    new ICard[] { },
                                    new ICard[]
                                    {
                        FakeSCardTypes.REGISTER["four_of_hearts"].Instantiate(),
                        FakeSCardTypes.REGISTER["three_of_hearts"].Instantiate(),
                        FakeSCardTypes.REGISTER["six_of_spades"].Instantiate(),
                        FakeSCardTypes.REGISTER["nine_of_clubs"].Instantiate()
                                    }),
                                99);

                            JsonElement stream = _ENDEC.Encode(
                                JsonStreamCoder.INSTANCE,
                                new RUser(0, new FakeSession(match), new RMetrics(), new RInventory()),
                                CoderSettings.DEFAULT);

                            string actualJson = stream.ToString(JsonFormatting.COMPRESSED);

                            // Use Regex to replace the timestamp value with a wildcard
                            string timestampPattern = "\"timestamp\":\\d+";  // Matches `"timestamp":123456789`
                            string sanitizedActualJson = Regex.Replace(actualJson, timestampPattern, "\"timestamp\":*");

                            string expectedJson = "{" +
                                "\"chips\":50," +
                                "\"match\":{" +
                                    "\"deck\":{" +
                                    "\"prototype\":[],\"state\":[]}," +
                                    "\"dealer_hand\":[\"three_of_hearts\",{\"type\":\"nine_of_clubs\",\"data\":{\"veiled\":true}}]," +
                                    "\"player_hand\":[\"four_of_hearts\",\"six_of_spades\"]," +
                                    "\"bet\":99," +
                                    "\"concluded\":false}," +
                                "\"metrics\":{\"history\":[],\"chip_history\":[{\"timestamp\":*,\"chip_count\":50}],\"ratio\":{\"wins\":0,\"ties\":0,\"losses\":0}},\"inventory\":{\"items\":[]}}";

                            Assert.AreEqual(expectedJson, sanitizedActualJson);
                        } // end EncodeOngoingMatch()

                        // history and etc

                    } // end inner inner class Valid
                } // end inner class ENDEC*/
    } // end class
} // end namespace