using ChipMasters.IO;
using GSR.EnDecic;
using GSR.EnDecic.Jsonic;
using GSR.Jsonic;

namespace Tests.ChipMasters.IO
{
    public static class TestRConstantEnDec
    {
        [TestClass]
        public class Valid
        {
            [TestMethod]
            [DataRow(9, 9, CoderSettings.DEFAULT, "null")]
            [DataRow(20202002, 20202002, CoderSettings.ALL_OPTIONALS, "null")]
            [DataRow(-1, -1, CoderSettings.SIGNIFICANT_ONLY | CoderSettings.TOLERANT, "null")]
            [DataRow(null, null, CoderSettings.SIGNIFICANT_ONLY | CoderSettings.TOLERANT, "null")]
            public void Encode(int? constant, int? data, CoderSettings settings, string expectation)
            {
                Assert.AreEqual(
                    expectation,
                    new RConstantEnDec<int?>(constant)
                        .Encode(JsonStreamCoder.INSTANCE, data, settings)
                        .ToString());
            } // end Encode()

            [TestMethod]
            [DataRow(9, "null", CoderSettings.DEFAULT, 9)]
            [DataRow(20202002, "null", CoderSettings.ALL_OPTIONALS, 20202002)]
            [DataRow(-1, "null", CoderSettings.SIGNIFICANT_ONLY | CoderSettings.TOLERANT, -1)]
            [DataRow(null, "null", CoderSettings.SIGNIFICANT_ONLY | CoderSettings.TOLERANT, null)]
            public void Decode(int? constant, string stream, CoderSettings settings, int? expectation)
            {
                Assert.AreEqual(
                    expectation,
                    new RConstantEnDec<int?>(constant)
                        .Decode(JsonStreamCoder.INSTANCE, JsonElement.ParseJson(stream), settings));
            } // end Decode()
        } // end inner class Valid

        [TestClass]
        public class Invalid
        {
            [TestMethod]
            [ExpectedException(typeof(InvalidOperationException))]
            [DataRow(9, 8, CoderSettings.DEFAULT)]
            [DataRow(20202002, 0, CoderSettings.ALL_OPTIONALS)]
            [DataRow(1, -1, CoderSettings.SIGNIFICANT_ONLY | CoderSettings.TOLERANT)]
            [DataRow(null, -1, CoderSettings.SIGNIFICANT_ONLY | CoderSettings.TOLERANT)]
            [DataRow(1, null, CoderSettings.SIGNIFICANT_ONLY | CoderSettings.TOLERANT)]
            public void Encode(int? constant, int? data, CoderSettings settings)
            {
                new RConstantEnDec<int?>(constant).Encode(JsonStreamCoder.INSTANCE, data, settings);
            } // end Encode()

            [TestMethod]
            [ExpectedException(typeof(InvalidOperationException))]
            [DataRow(9, "1", CoderSettings.DEFAULT)]
            [DataRow(20202002, "{}", CoderSettings.ALL_OPTIONALS)]
            [DataRow(-1, "[]", CoderSettings.SIGNIFICANT_ONLY | CoderSettings.TOLERANT)]
            [DataRow(null, "{\"\":[],\"sdf\":999930249023e4}", CoderSettings.SIGNIFICANT_ONLY | CoderSettings.TOLERANT)]
            public void Decode(int? constant, string stream, CoderSettings settings)
            {
                new RConstantEnDec<int?>(constant)
                    .Decode(JsonStreamCoder.INSTANCE, JsonElement.ParseJson(stream), settings);
            } // end Decode()
        } // end inner class Invalid
    } // end class
} // end namespace