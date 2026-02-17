using ChipMasters.Cards;
using ChipMasters.Games.Hands;
using ChipMasters.Registers;
using GSR.EnDecic.Jsonic;
using GSR.Jsonic;
using GSR.Jsonic.Formatting;

namespace Tests.ChipMasters.Games.Hands
{
    public class TestIHand
    {
        public static class EnDec
        {
            [TestClass]
            public class Valid
            {
                [TestMethod]
                public void Encode1()
                    => Assert.AreEqual(
                        "[]",
                        IHand.ENDEC
                        .Encode(JsonStreamCoder.INSTANCE, new RHand(), GSR.EnDecic.CoderSettings.DEFAULT)
                        .ToString(JsonFormatting.COMPRESSED));

                [TestMethod]
                public void Decode1()
                {
                    IHand hand = IHand.ENDEC
                        .Decode(JsonStreamCoder.INSTANCE,
                            JsonElement.ParseJson("[]"),
                            GSR.EnDecic.CoderSettings.DEFAULT);

                    Assert.AreEqual(0, hand.Count);
                } // end Decode1()

                [TestMethod]
                public void Encode2()
                    => Assert.AreEqual(
                        "[\"queen_of_hearts\",\"ace_of_diamonds\"]",
                        IHand.ENDEC
                        .Encode(JsonStreamCoder.INSTANCE, new RHand(
                            new ICard[] {
                            SCardTypes.REGISTER["queen_of_hearts"].Instantiate(),
                            SCardTypes.REGISTER["ace_of_diamonds"].Instantiate()}
                            ), GSR.EnDecic.CoderSettings.DEFAULT)
                        .ToString(JsonFormatting.COMPRESSED));

                [TestMethod]
                public void Decode2()
                {
                    IHand hand = IHand.ENDEC
                        .Decode(JsonStreamCoder.INSTANCE,
                            JsonElement.ParseJson("[\"queen_of_hearts\", \"ace_of_diamonds\"]"),
                            GSR.EnDecic.CoderSettings.DEFAULT);

                    Assert.AreEqual(2, hand.Count);
                    Assert.AreEqual(SCardTypes.REGISTER["queen_of_hearts"].Instantiate(), hand[0]);
                    Assert.AreEqual(SCardTypes.REGISTER["ace_of_diamonds"].Instantiate(), hand[1]);
                } // end Decode2()
            } // end inner class Valid

            [TestClass]
            public class Invalid
            {
                [TestMethod]
                [ExpectedException(typeof(InvalidJsonCastException))]
                [DataRow("{}")]
                public void Decode1(string json)
                    => IHand.ENDEC
                    .Decode(JsonStreamCoder.INSTANCE, JsonElement.ParseJson(json), GSR.EnDecic.CoderSettings.DEFAULT);

                [TestMethod]
                [ExpectedException(typeof(AggregateException))]
                [DataRow("[0]")]
                [DataRow("[true]")]
                [DataRow("[null]")]
                [DataRow("[\"\", null]")]
                public void Decode2(string json)
                    => IHand.ENDEC
                    .Decode(JsonStreamCoder.INSTANCE, JsonElement.ParseJson(json), GSR.EnDecic.CoderSettings.DEFAULT);
            } // end inner class Invalid
        } // end inner class EnDec
    } // end class
} // end namespace