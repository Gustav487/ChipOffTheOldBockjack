using ChipMasters.User;
using GSR.EnDecic;
using GSR.EnDecic.Jsonic;
using GSR.Jsonic;
using GSR.Jsonic.Formatting;

namespace Tests.ChipMasters.User
{
    public static class TestRWallet
    {
        [TestClass]
        public class Valid
        {
            [TestMethod]
            [DataRow(10, 10)]
            [DataRow(0, 0)]
            [DataRow(-22, -22)]
            public void Ctor(int startingChips, int exChips)
            {
                IWallet w = new RWallet(startingChips);
                Assert.AreEqual(exChips, w.Chips);
            } // end Ctor

            [TestMethod]
            [DataRow(10, 93, true, 93)]
            [DataRow(0, 4, true, 4)]
            [DataRow(-9000000, 4, true, 4)]
            [DataRow(74, -5, true, -5)]
            [DataRow(74, 74, false, 74)]
            [DataRow(0, 0, false, 0)]
            [DataRow(-2, -13, true, -13)]
            [DataRow(-03, -03, false, -03)]
            public void ChipsSetter(int startingChips, int setTo, bool exEventRan, int exChips)
            {
                IWallet w = new RWallet(startingChips);
                bool ran = false;
                w.OnChipsChanged += () => ran = true;
                w.Chips = setTo;

                Assert.AreEqual(exEventRan, ran);
                Assert.AreEqual(exChips, w.Chips);
            } // end Ctor
        } // end inner class Valid()

        public static class ENDEC
        {
            [TestClass]
            public class Valid
            {
                [TestMethod]
                [DataRow(-441, $"{{\"{RWallet.CHIPS_KEY}\":-441}}")]
                [DataRow(0, $"{{\"{RWallet.CHIPS_KEY}\":0}}")]
                [DataRow(4, $"{{\"{RWallet.CHIPS_KEY}\":4}}")]
                [DataRow(int.MaxValue, $"{{\"{RWallet.CHIPS_KEY}\":2147483647}}")]
                public void Encode(int initChips, string exJson)
                {
                    RWallet data = new(initChips);
                    JsonElement stream = RWallet.ENDEC.Encode(
                        JsonStreamCoder.INSTANCE,
                        data,
                        CoderSettings.DEFAULT);

                    Assert.AreEqual(exJson, stream.ToString(JsonFormatting.COMPRESSED));
                } // end Encode()

                [TestMethod]
                [DataRow($"{{\"{RWallet.CHIPS_KEY}\": -2147483648}}", -2147483648)]
                [DataRow($"{{\"{RWallet.CHIPS_KEY}\": -1}}", -1)]
                [DataRow($"{{\"{RWallet.CHIPS_KEY}\": 0}}", 0)]
                [DataRow($"{{\"{RWallet.CHIPS_KEY}\": 4}}", 4)]
                [DataRow($"{{\"{RWallet.CHIPS_KEY}\": 2147483647}}", int.MaxValue)]
                public void Decode(string json, int exChipCount)
                {
                    RWallet data = RWallet.ENDEC.Decode(
                        JsonStreamCoder.INSTANCE,
                        JsonElement.ParseJson(json),
                        CoderSettings.DEFAULT);

                    Assert.AreEqual(exChipCount, data.Chips);
                } // end Decode()
            } // end inner inner class ENDEC.Valid
        } // end inner class ENDEC
    } // end class
} // end namespace