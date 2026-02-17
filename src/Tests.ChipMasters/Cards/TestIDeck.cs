using ChipMasters.Cards;
using ChipMasters.Registers;
using Fakes.ChipMasters.Cards;
using GSR.EnDecic.Jsonic;
using GSR.Jsonic;
using GSR.Jsonic.Formatting;

namespace Tests.ChipMasters.Cards
{
    public static class TestIDeck
    {
        public static class ENDEC
        {
            [TestClass]
            public class Valid
            {
                [TestMethod]
                public void Encode1()
                    => Assert.AreEqual(
                        "{\"prototype\":[],\"state\":[]}",
                        IDeck.ENDEC
                        .Encode(JsonStreamCoder.INSTANCE, new RDeck(), GSR.EnDecic.CoderSettings.DEFAULT)
                        .ToString(JsonFormatting.COMPRESSED));

                [TestMethod]
                public void Encode1_2()
                    => Assert.AreEqual(
                        "{\"prototype\":[],\"state\":[]}",
                        IDeck.ENDEC
                        .Encode(JsonStreamCoder.INSTANCE, new FakeDeck(), GSR.EnDecic.CoderSettings.DEFAULT)
                        .ToString(JsonFormatting.COMPRESSED));

                [TestMethod]
                public void Decode1()
                {
                    IDeck deck = IDeck.ENDEC
                        .Decode(JsonStreamCoder.INSTANCE,
                            JsonElement.ParseJson(
                                "{\"prototype\":[],\"state\":[]}"),
                            GSR.EnDecic.CoderSettings.DEFAULT);

                    Assert.AreEqual(typeof(RDeck), deck.GetType());
                    Assert.AreEqual(0, deck.Prototype.Count);
                    Assert.AreEqual(0, deck.Count);
                } // end Decode1()

                [TestMethod]
                public void Encode2()
                    => Assert.AreEqual(
                        "{\"prototype\":[\"queen_of_hearts\",\"ace_of_diamonds\"]," +
                        "\"state\":[]}",
                        IDeck.ENDEC
                        .Encode(JsonStreamCoder.INSTANCE, new RDeck(
                            new ICard[] {
                            SCardTypes.REGISTER["queen_of_hearts"].Instantiate(),
                            SCardTypes.REGISTER["ace_of_diamonds"].Instantiate()},
                            new ICard[] { }
                            ), GSR.EnDecic.CoderSettings.DEFAULT)
                        .ToString(JsonFormatting.COMPRESSED));

                [TestMethod]
                public void Decode2()
                {
                    IDeck deck = IDeck.ENDEC
                        .Decode(JsonStreamCoder.INSTANCE,
                            JsonElement.ParseJson(
                            "{\"prototype\":[\"queen_of_hearts\", \"ace_of_diamonds\"]," +
                            "\"state\":[]}"),
                            GSR.EnDecic.CoderSettings.DEFAULT);

                    Assert.AreEqual(2, deck.Prototype.Count);
                    Assert.AreEqual(SCardTypes.REGISTER["queen_of_hearts"].Instantiate(), deck.Prototype[0]);
                    Assert.AreEqual(SCardTypes.REGISTER["ace_of_diamonds"].Instantiate(), deck.Prototype[1]);
                    Assert.AreEqual(0, deck.Count);
                } // end Decode2()

                [TestMethod]
                public void Encode3()
                    => Assert.AreEqual(
                        "{\"prototype\":[\"queen_of_hearts\",\"ace_of_diamonds\"]," +
                        "\"state\":[\"queen_of_hearts\",\"ace_of_diamonds\"]}",
                        IDeck.ENDEC
                        .Encode(JsonStreamCoder.INSTANCE, new RDeck(
                            SCardTypes.REGISTER["queen_of_hearts"].Instantiate(),
                            SCardTypes.REGISTER["ace_of_diamonds"].Instantiate()
                            ), GSR.EnDecic.CoderSettings.DEFAULT)
                        .ToString(JsonFormatting.COMPRESSED));

                [TestMethod]
                public void Decode3()
                {
                    IDeck deck = IDeck.ENDEC
                    .Decode(JsonStreamCoder.INSTANCE,
                    JsonElement.ParseJson(
                        "{\"prototype\":[\"queen_of_hearts\",\"ace_of_diamonds\"]," +
                        "\"state\":[\"queen_of_hearts\",\"ace_of_diamonds\"]}"),
                    GSR.EnDecic.CoderSettings.DEFAULT);

                    Assert.AreEqual(2, deck.Prototype.Count);
                    Assert.AreEqual(SCardTypes.REGISTER["queen_of_hearts"].Instantiate(), deck.Prototype[0]);
                    Assert.AreEqual(SCardTypes.REGISTER["ace_of_diamonds"].Instantiate(), deck.Prototype[1]);
                    Assert.AreEqual(2, deck.Count);
                    Assert.AreEqual(SCardTypes.REGISTER["queen_of_hearts"].Instantiate(), deck[0]);
                    Assert.AreEqual(SCardTypes.REGISTER["ace_of_diamonds"].Instantiate(), deck[1]);
                } // end Decode3()

                [TestMethod]
                public void Encode4()
                    => Assert.AreEqual(
                        "{\"prototype\":[\"queen_of_hearts\",\"ace_of_diamonds\"]," +
                        "\"state\":[\"two_of_clubs\"]}",
                        IDeck.ENDEC
                        .Encode(JsonStreamCoder.INSTANCE, new RDeck(
                            new ICard[] {
                                SCardTypes.REGISTER["queen_of_hearts"].Instantiate(),
                                SCardTypes.REGISTER["ace_of_diamonds"].Instantiate()
                            },
                            new ICard[] {
                                SCardTypes.REGISTER["two_of_clubs"].Instantiate()
                            }
                            ), GSR.EnDecic.CoderSettings.DEFAULT)
                        .ToString(JsonFormatting.COMPRESSED));

                [TestMethod]
                public void Decode4()
                {
                    IDeck deck = IDeck.ENDEC
                    .Decode(JsonStreamCoder.INSTANCE,
                    JsonElement.ParseJson(
                        "{\"prototype\":[\"queen_of_hearts\",\"ace_of_diamonds\"]," +
                        "\"state\":[{\"type\":\"two_of_clubs\",\"data\":{\"veiled\":false}}]}"),
                    GSR.EnDecic.CoderSettings.DEFAULT);

                    Assert.AreEqual(2, deck.Prototype.Count);
                    Assert.AreEqual(SCardTypes.REGISTER["queen_of_hearts"].Instantiate(), deck.Prototype[0]);
                    Assert.AreEqual(SCardTypes.REGISTER["ace_of_diamonds"].Instantiate(), deck.Prototype[1]);
                    Assert.AreEqual(1, deck.Count);
                    Assert.AreEqual(SCardTypes.REGISTER["two_of_clubs"].Instantiate(), deck[0]);
                } // end Decode4()
            } // end inner class Valid

            [TestClass]
            public class Invalid
            {
                [TestMethod]
                [ExpectedException(typeof(ArgumentException))]
                [DataRow("{\"type\":\"ChipMasters.Cards.RDeck\"," +
                        "\"data\":{" +
                            "\"prototype\":[\"queen_of_hearts\",\"ace_of_diamonds\"]," +
                            "\"state\":[{\"type\":\"two_of_clubs\",\"data\":null}]}}")] // typed form
                [DataRow("{\"type\":\"ChipMasters.Cards.RDeck\", \"data\":{}}")] // malformed data
                [DataRow("{\"type\":\"ChipMasters.Cards.RDeck\", \"data\": {}, \"Extra\":-3942}")] // too many fields
                [DataRow("{\"data\":{}}")] // missing type
                [DataRow("{\"type\":\"ChipMasters.Cards.RDeck\"}")] // missing data
                public void Decode(string json)
                    => IDeck.ENDEC
                    .Decode(JsonStreamCoder.INSTANCE, JsonElement.ParseJson(json), GSR.EnDecic.CoderSettings.DEFAULT);
            } // end inner class Invalid
        } // end inner class ENDEC
    } // end class
} // end namespace