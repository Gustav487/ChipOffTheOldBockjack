using ChipMasters.IO;
using GSR.EnDecic;
using GSR.EnDecic.Jsonic;
using GSR.Jsonic;

namespace Tests.ChipMasters.IO
{
    public static class TestREnumEnDec
    {
        [TestClass]
        public class Valid
        {
            [TestMethod]
            [DataRow(ETestEnum.A, CoderSettings.SIGNIFICANT_ONLY, "\"A\"")]
            [DataRow(ETestEnum.Two, CoderSettings.TOLERANT, "\"Two\"")]
            [DataRow(ETestEnum.Also, CoderSettings.DEFAULT, "\"Also\"")]
            public void Encode(ETestEnum data, CoderSettings settings, string expectation)
                => Assert.AreEqual(expectation, new REnumEnDec<ETestEnum>()
                    .Encode(JsonStreamCoder.INSTANCE, data, settings).ToString());

            [TestMethod]
            [DataRow("\"A\"", CoderSettings.SIGNIFICANT_ONLY, ETestEnum.A)]
            [DataRow("\"Two\"", CoderSettings.TOLERANT, ETestEnum.Two)]
            [DataRow("\"Also\"", CoderSettings.DEFAULT, ETestEnum.Also)]
            [DataRow("\"2\"", CoderSettings.DEFAULT, ETestEnum.Also)]
            public void Decode(string json, CoderSettings settings, ETestEnum expectation)
                => Assert.AreEqual(expectation, new REnumEnDec<ETestEnum>()
                    .Decode(JsonStreamCoder.INSTANCE, JsonElement.ParseJson(json), settings));
        } // end inner class Valid

        [TestClass]
        public class Invalid
        {
            [TestMethod]
            [ExpectedException(typeof(ArgumentException))]
            [DataRow((ETestEnum)234902, CoderSettings.SIGNIFICANT_ONLY)]
            [DataRow((ETestEnum)(-12), CoderSettings.TOLERANT)]
            [DataRow((ETestEnum)10, CoderSettings.DEFAULT)]
            public void Encode(ETestEnum data, CoderSettings settings)
                => new REnumEnDec<ETestEnum>().Encode(JsonStreamCoder.INSTANCE, data, settings);

            [TestMethod]
            [ExpectedException(typeof(ArgumentException))]
            [DataRow("\"a\"", CoderSettings.SIGNIFICANT_ONLY)]
            [DataRow("\"Three\"", CoderSettings.TOLERANT)]
            [DataRow("\"22\"", CoderSettings.DEFAULT)]
            public void Decode(string json, CoderSettings settings)
                => new REnumEnDec<ETestEnum>().Decode(JsonStreamCoder.INSTANCE, JsonElement.ParseJson(json), settings);

            [TestMethod]
            [ExpectedException(typeof(InvalidJsonCastException))]
            [DataRow("2", CoderSettings.DEFAULT)]
            public void Decode2(string json, CoderSettings settings)
                => new REnumEnDec<ETestEnum>().Decode(JsonStreamCoder.INSTANCE, JsonElement.ParseJson(json), settings);
        } // end inner class Invalid


        public enum ETestEnum
        {
            A = 0,
            Two = 3,
            Also = 2,
        } // end inner enum ETestEnum
    } // end class
} // end namespace