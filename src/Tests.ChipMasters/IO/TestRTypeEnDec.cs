using ChipMasters.Cards;
using ChipMasters.IO;
using GSR.EnDecic;
using GSR.EnDecic.Jsonic;
using GSR.Jsonic;

namespace Tests.ChipMasters.IO
{
    public static class TestRTypeEnDec
    {
        [TestClass]
        public class Valid
        {
            [TestMethod]
            [DataRow(0, "System.Int32")]
            [DataRow("", "System.String")]
            public void EncodePrimative(object data, string expectation)
            {
                Assert.AreEqual(expectation, SIOUtil.TYPE_ENDEC
                    .Encode(JsonStreamCoder.INSTANCE, data.GetType(), CoderSettings.DEFAULT).AsString().Value);
            } // end EncodePrimative()

            [TestMethod]
            public void EncodeCustom()
            {
                Assert.AreEqual("ChipMasters.Cards.RDeck", SIOUtil.TYPE_ENDEC
                    .Encode(JsonStreamCoder.INSTANCE, typeof(RDeck), CoderSettings.DEFAULT).AsString().Value);
            } // end EncodePrimative()

            [TestMethod]
            [DataRow(0, "System.Int32")]
            [DataRow("", "System.String")]
            public void DecodePrimative(object expectation, string stream)
            {
                Assert.AreEqual(expectation.GetType(), SIOUtil.TYPE_ENDEC
                    .Decode(JsonStreamCoder.INSTANCE, new JsonString(stream), CoderSettings.DEFAULT));
            } // end DecodePrimative()

            [TestMethod]
            public void DecodeCustom()
            {
                Assert.AreEqual(typeof(RDeck), SIOUtil.TYPE_ENDEC
                    .Decode(JsonStreamCoder.INSTANCE, new JsonString("ChipMasters.Cards.RDeck"), CoderSettings.DEFAULT));
            } // end DecodeCustom()
        } // end inner class Valid
    } // end class
} // end namespace