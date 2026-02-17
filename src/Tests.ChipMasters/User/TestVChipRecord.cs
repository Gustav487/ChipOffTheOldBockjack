using ChipMasters.User;
using GSR.EnDecic;
using GSR.EnDecic.Jsonic;
using GSR.Jsonic;
using GSR.Jsonic.Formatting;

namespace Tests.ChipMasters.User
{
    public static class TestVChipRecord
    {
        public static class ENDEC
        {
            [TestClass]
            public class Valid
            {
                [TestMethod]
                [DataRow(1, 2, 3, 4, 5, 6, 899, null, "{\"timestamp\":-62132712894,\"count\":899,\"delta\":null}")]
                [DataRow(1970, 2, 3, 4, 5, 6, 899, 4095867, "{\"timestamp\":2883906,\"count\":899,\"delta\":4095867}")]
                public void Encode(
                    int year, int month, int day, int hour, int minute, int second,
                    int count,
                    int? delta,
                    string ex)
                {
                    JsonElement stream = VChipRecord.ENDEC.Encode(
                        JsonStreamCoder.INSTANCE,
                        new VChipRecord(
                            new DateTime(year, month, day, hour, minute, second),
                            count,
                            delta),
                        CoderSettings.DEFAULT);

                    Assert.AreEqual(ex, stream.ToString(JsonFormatting.COMPRESSED));
                } // end Encode()

                [TestMethod]
                [DataRow("{\"timestamp\":-62132712894,\"count\":899,\"delta\":null}", 1, 2, 3, 4, 5, 6, 899, null)]
                [DataRow("{\"timestamp\":2883906,\"count\":899,\"delta\":4095867}", 1970, 2, 3, 4, 5, 6, 899, 4095867)]
                public void Decode(
                    string json,
                    int exYear, int exMonth, int exDay, int exHour, int exMinute, int exSecond,
                    int exCount,
                    int? exDelta)
                {
                    VChipRecord data = VChipRecord.ENDEC.Decode(
                        JsonStreamCoder.INSTANCE,
                        JsonElement.ParseJson(json),
                        CoderSettings.DEFAULT);

                    Assert.AreEqual(
                        new DateTime(exYear, exMonth, exDay, exHour, exMinute, exSecond),
                        data.Timestamp);
                    Assert.AreEqual(exCount, data.Count);
                    Assert.AreEqual(exDelta, data.Delta);
                } // end Encode()

            } // end inner class TestVChipRecord.ENDEC.Valid
        } // end inner class TestVChipRecord.ENDEC
    } // end class
} // end namespace