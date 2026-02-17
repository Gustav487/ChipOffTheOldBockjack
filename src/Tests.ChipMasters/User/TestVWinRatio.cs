using ChipMasters.User;
using GSR.EnDecic;
using GSR.EnDecic.Jsonic;
using GSR.Jsonic;
using GSR.Jsonic.Formatting;

namespace Tests.ChipMasters.User
{
    public static class TestVWinRatio
    {
        [TestClass]
        public class Valid
        {
            [TestMethod]
            [DataRow(0, 0, 0, 0, 0, 0)]
            [DataRow(1, 0, 0, 1, 0, 0)]
            [DataRow(0, 1, 0, 0, 1, 0)]
            [DataRow(0, 0, 1, 0, 0, 1)]
            [DataRow(234, 8, 1055, 234, 8, 1055)]
            public void Ctor(int w, int t, int l, int exW, int exT, int exL)
            {
                VWinRatio vwr = new(w: w, t: t, l: l);
                Assert.AreEqual(exW, vwr.Wins);
                Assert.AreEqual(exT, vwr.Ties);
                Assert.AreEqual(exL, vwr.Losses);
            } // end Ctor()

            [TestMethod]
            [DataRow(0, 0, 0, "0:0:0")]
            [DataRow(1, 0, 0, "1:0:0")]
            [DataRow(0, 1, 0, "0:1:0")]
            [DataRow(0, 0, 1, "0:0:1")]
            [DataRow(234, 8, 1055, "234:8:1055")]
            public void ToString(int w, int t, int l, string ex)
            {
                VWinRatio vwr = new(w: w, t: t, l: l);
                Assert.AreEqual(ex, vwr.ToString());
            } // end ToString()
        } // end class TestVWinRatio.Valid

        [TestClass]
        public class Invalid
        {
            [TestMethod]
            [ExpectedException(typeof(ArgumentOutOfRangeException))]
            [DataRow(-1, 0, 0)]
            [DataRow(0, -1, 0)]
            [DataRow(0, 0, -1)]
            [DataRow(-234, -8, -1055)]
            public void Ctor(int w, int t, int l)
            {
                new VWinRatio(w: w, t: t, l: l);
            } // end Ctor()
        } // end class TestVWinRatio.Invalid

        public static class ENDEC
        {
            [TestClass]
            public class Valid
            {
                [TestMethod]
                [DataRow(0, 0, 0, $"{{\"{VWinRatio.WINS_KEY}\":0,\"{VWinRatio.TIES_KEY}\":0,\"{VWinRatio.LOSSES_KEY}\":0}}")]
                [DataRow(0, 1, 0, $"{{\"{VWinRatio.WINS_KEY}\":0,\"{VWinRatio.TIES_KEY}\":1,\"{VWinRatio.LOSSES_KEY}\":0}}")]
                [DataRow(341, 1, 8739, $"{{\"{VWinRatio.WINS_KEY}\":341,\"{VWinRatio.TIES_KEY}\":1,\"{VWinRatio.LOSSES_KEY}\":8739}}")]
                public void Encode(int w, int t, int l, string exJson)
                {
                    JsonElement json = VWinRatio.ENDEC.Encode(
                        JsonStreamCoder.INSTANCE,
                        new VWinRatio(w: w, t: t, l: l),
                        CoderSettings.DEFAULT);

                    Assert.AreEqual(exJson, json.ToString(JsonFormatting.COMPRESSED));
                } // end Decode()

                [TestMethod]
                [DataRow($"{{\"{VWinRatio.WINS_KEY}\":0,\"{VWinRatio.TIES_KEY}\":0,\"{VWinRatio.LOSSES_KEY}\":0}}", 0, 0, 0)]
                [DataRow($"{{\"{VWinRatio.WINS_KEY}\":0,\"{VWinRatio.TIES_KEY}\":1,\"{VWinRatio.LOSSES_KEY}\":0}}", 0, 1, 0)]
                [DataRow($"{{\"{VWinRatio.TIES_KEY}\":0,\"{VWinRatio.WINS_KEY}\":1,\"{VWinRatio.LOSSES_KEY}\":0}}", 1, 0, 0)]
                [DataRow($"{{\"{VWinRatio.WINS_KEY}\":341,\"{VWinRatio.TIES_KEY}\":1,\"{VWinRatio.LOSSES_KEY}\":8739}}", 341, 1, 8739)]
                public void Decode(string json, int exW, int exT, int exL)
                {
                    VWinRatio vwr = VWinRatio.ENDEC.Decode(
                        JsonStreamCoder.INSTANCE,
                        JsonElement.ParseJson(json),
                        CoderSettings.DEFAULT);

                    Assert.AreEqual(exW, vwr.Wins);
                    Assert.AreEqual(exT, vwr.Ties);
                    Assert.AreEqual(exL, vwr.Losses);
                } // end Decode()
            } // end class TestVWinRatio.ENDEC.Valid

            [TestClass]
            public class Invalid
            {
                [TestMethod]
                [ExpectedException(typeof(ArgumentOutOfRangeException))]
                [DataRow($"{{\"{VWinRatio.WINS_KEY}\":-1,\"{VWinRatio.TIES_KEY}\":0,\"{VWinRatio.LOSSES_KEY}\":0}}")]
                [DataRow($"{{\"{VWinRatio.WINS_KEY}\":0,\"{VWinRatio.TIES_KEY}\":-1,\"{VWinRatio.LOSSES_KEY}\":0}}")]
                [DataRow($"{{\"{VWinRatio.WINS_KEY}\":0,\"{VWinRatio.TIES_KEY}\":0,\"{VWinRatio.LOSSES_KEY}\":-1}}")]
                [DataRow($"{{\"{VWinRatio.WINS_KEY}\":-341,\"{VWinRatio.TIES_KEY}\":0,\"{VWinRatio.LOSSES_KEY}\":-8739}}")]
                public void Decode(string json)
                {
                    VWinRatio.ENDEC.Decode(
                        JsonStreamCoder.INSTANCE,
                        JsonElement.ParseJson(json),
                        CoderSettings.DEFAULT);
                } // end Decode()
            } // end class TestVWinRatio.ENDEC.Invalid
        } // end inner class TestVWinRatio.ENDEC

    } // end class
} // end namespace