using ChipMasters.User;
using GSR.EnDecic;
using GSR.EnDecic.Jsonic;
using GSR.Jsonic;
using GSR.Jsonic.Formatting;

namespace Tests.ChipMasters.User
{
    public static class TestRDebtlessWallet
    {
        [TestClass]
        public class Valid
        {
            [TestMethod]
            [DataRow(10, 10)]
            [DataRow(0, 0)]
            [DataRow(-22, 0)]
            public void Ctor(int startingChips, int exChips)
            {
                IWallet w = new RDebtlessWallet(startingChips);
                Assert.AreEqual(exChips, w.Chips);
            } // end Ctor

            [TestMethod]
            [DataRow(10, 93, true, 93)]
            [DataRow(0, 4, true, 4)]
            [DataRow(74, -5, true, 0)]
            [DataRow(74, 74, false, 74)]
            [DataRow(0, 0, false, 0)]
            [DataRow(-2, -13, false, 0)]
            public void ChipsSetter(int startingChips, int setTo, bool exEventRan, int exChips)
            {
                IWallet w = new RDebtlessWallet(startingChips);
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
                [DataRow(0, $"{{\"{RDebtlessWallet.CHIPS_KEY}\":0}}")]
                [DataRow(4, $"{{\"{RDebtlessWallet.CHIPS_KEY}\":4}}")]
                [DataRow(int.MaxValue, $"{{\"{RDebtlessWallet.CHIPS_KEY}\":2147483647}}")]
                public void Encode(int initChips, string exJson)
                {
                    RDebtlessWallet data = new(initChips);
                    JsonElement stream = RDebtlessWallet.ENDEC.Encode(
                        JsonStreamCoder.INSTANCE,
                        data,
                        CoderSettings.DEFAULT);

                    Assert.AreEqual(exJson, stream.ToString(JsonFormatting.COMPRESSED));
                } // end Encode()

                [TestMethod]
                [DataRow($"{{\"{RDebtlessWallet.CHIPS_KEY}\": 0}}", 0)]
                [DataRow($"{{\"{RDebtlessWallet.CHIPS_KEY}\": 4}}", 4)]
                [DataRow($"{{\"{RDebtlessWallet.CHIPS_KEY}\": 2147483647}}", int.MaxValue)]
                public void Decode(string json, int exChipCount)
                {
                    RDebtlessWallet data = RDebtlessWallet.ENDEC.Decode(
                        JsonStreamCoder.INSTANCE,
                        JsonElement.ParseJson(json),
                        CoderSettings.DEFAULT);

                    Assert.AreEqual(exChipCount, data.Chips);
                } // end Decode()
            } // end inner inner class ENDEC.Valid

            [TestClass]
            public class Invalid
            {
                [TestMethod]
                [ExpectedException(typeof(ArgumentOutOfRangeException))]
                [DataRow($"{{\"{RDebtlessWallet.CHIPS_KEY}\": -4}}")]
                [DataRow($"{{\"{RDebtlessWallet.CHIPS_KEY}\": -1}}")]
                public void Decode(string json)
                {
                    RDebtlessWallet.ENDEC.Decode(
                        JsonStreamCoder.INSTANCE,
                        JsonElement.ParseJson(json),
                        CoderSettings.DEFAULT);
                } // end Decode()
            } // end inner inner class ENDEC.Invalid
        } // end inner class ENDEC
    } // end class
} // end namespace