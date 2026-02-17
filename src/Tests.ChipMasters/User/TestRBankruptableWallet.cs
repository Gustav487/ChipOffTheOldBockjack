using ChipMasters.User;

namespace Tests.ChipMasters.User
{
    public static class TestRBankruptableWallet
    {
        [TestClass]
        public class Valid
        {
            [TestMethod]
            [DataRow(10, 023492, 10)]
            [DataRow(0, 1, 1)]
            [DataRow(-22, 43, 43)]
            public void Ctor(int startingChips, int bankrupcyPay, int exChips)
            {
                IWallet w = new RBankruptableWallet(startingChips, bankrupcyPay);
                Assert.AreEqual(exChips, w.Chips);
            } // end Ctor


            [TestMethod]
            [DataRow(10, 023492, 93, true, 93)]
            [DataRow(0, 1, 4, true, 4)]
            [DataRow(74, 43, -5, true, 43)]
            [DataRow(74, 43, 74, false, 74)]
            [DataRow(43, 43, -9, false, 43)]
            public void ChipsSetter(int startingChips, int bankrupcyPay, int setTo, bool exEventRan, int exChips)
            {
                IWallet w = new RBankruptableWallet(startingChips, bankrupcyPay);
                bool ran = false;
                w.OnChipsChanged += () => ran = true;
                w.Chips = setTo;

                Assert.AreEqual(exEventRan, ran);
                Assert.AreEqual(exChips, w.Chips);
            } // end Ctor

        } // end inner class Valid()

        [TestClass]
        public class Invalid
        {
            [TestMethod]
            [ExpectedException(typeof(ArgumentOutOfRangeException))]
            [DataRow(10, 0)]
            [DataRow(0, -1)]
            public void Ctor(int startingChips, int bankrupcyPay)
            {
                new RBankruptableWallet(startingChips, bankrupcyPay);
            } // end Ctor
        } // end inner class Invalid()
        /*
                public static class ENDEC
                {
                    [TestClass]
                    public class Valid
                    {
                        [TestMethod]
                        [DataRow(0, $"{{\"{RBankruptableWallet.CHIPS_KEY}\":0}}")]
                        [DataRow(4, $"{{\"{RBankruptableWallet.CHIPS_KEY}\":4}}")]
                        [DataRow(int.MaxValue, $"{{\"{RDebtlessWallet.CHIPS_KEY}\":2147483647}}")]
                        public void Encode(int initChips, string exJson)
                        {
                            RBankruptableWallet data = new(initChips);
                            JsonElement stream = RDebtlessWallet.ENDEC.Encode(
                                JsonStreamCoder.INSTANCE,
                                data,
                                CoderSettings.DEFAULT);

                            Assert.AreEqual(exJson, stream.ToString(JsonFormatting.COMPRESSED));
                        } // end Encode()

                        [TestMethod]
                        [DataRow($"{{\"{RBankruptableWallet.CHIPS_KEY}\": 0}}", 0)]
                        [DataRow($"{{\"{RBankruptableWallet.CHIPS_KEY}\": 4}}", 4)]
                        [DataRow($"{{\"{RBankruptableWallet.CHIPS_KEY}\": 2147483647}}", int.MaxValue)]
                        public void Decode(string json, int exChipCount)
                        {
                            RBankruptableWallet data = RDebtlessWallet.ENDEC.Decode(
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
                        [DataRow($"{{\"{RBankruptableWallet.CHIPS_KEY}\":-4,\"{RBankruptableWallet.BANKRUPTCY_PAY_KEY}\":1}}")]
                        [DataRow($"{{\"{RBankruptableWallet.CHIPS_KEY}\": -1}}")]
                        public void Decode(string json)
                        {
                            RBankruptableWallet.ENDEC.Decode(
                                JsonStreamCoder.INSTANCE,
                                JsonElement.ParseJson(json),
                                CoderSettings.DEFAULT);
                        } // end Decode()
                    } // end inner inner class ENDEC.Invalid
                } // end inner class ENDEC*/
    } // end class
} // end namespace