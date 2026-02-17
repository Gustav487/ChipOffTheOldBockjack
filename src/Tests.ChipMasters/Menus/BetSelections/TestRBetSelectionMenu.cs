using ChipMasters.Games.BetHandlers;
using ChipMasters.GodotWrappers;
using ChipMasters.Menu.BetSelections;
using ChipMasters.Menu.Displays.Chips;
using ChipMasters.User;
using Fakes.ChipMasters.GodotWrappers;
using Fakes.ChipMasters.Menus.Displays.Chips;
using Fakes.ChipMasters.Users;

namespace Tests.ChipMasters.Menus.BetSelections
{
    public static class TestRBetSelectionMenu
    {
        [TestClass]
        public class Valid
        {
            [TestMethod]
            [DataRow(0, 0, 0, 0, 0, 0, 0)] // set to 0 - 0
            [DataRow(12, 0, 0, 0, 12, 0, 0)]
            [DataRow(2000000000, 0, 0, 0, 2000000000, 0, 0)]

            [DataRow(2000000000, 0, 401, 0, 2000000000, 0, 401)] // set to non-zero max lower than actual value
            [DataRow(92, 0, 303, 0, 92, 0, 92)]

            [DataRow(92, 1, 303, 0, 92, 1, 92)] // set to min lower than held value, set to max higher than held value
            [DataRow(92, 1, 91, 0, 92, 1, 91)] // set to min lower than held value, set to max lower than held value

            [DataRow(3, 4, 303, 0, 3, null, null)] // set to min higher than held value, set to max higher than held value
            public void Range(int chipCount, int min, int max,
            int? expectedPreMin, int? expectedPreMax,
            int? expectedPostMin, int? expectedPostMax)
            {
                IUser p = new FakeUser(chipCount: chipCount);
                IChipDisplay minL = new FakeChipDisplay();
                IChipDisplay maxL = new FakeChipDisplay();
                IBetSelectionMenu b = new RBetSelectionMenu(new RWallet(chipCount), minL, maxL, new FakeTextEdit(), new FakeRange(), new FakeButton());

                Assert.AreEqual(expectedPreMin, minL.Chips);
                Assert.AreEqual(expectedPreMax, maxL.Chips);

                b.Range = new(min, max);

                Assert.AreEqual(expectedPostMin, minL.Chips);
                Assert.AreEqual(expectedPostMax, maxL.Chips);
            } // end Range()

            [TestMethod]
            // to same value
            [DataRow(0, 0, 0, 0, 0, 0, 0, 0)] // same as range
            [DataRow(4, 0, 4, 4, 0, 4, 0, 4)] // same as max
            [DataRow(23, 0, 12, 23, 0, 12, 0, 12)] // range narrower than chip count
            [DataRow(25, 0, 66, 25, 0, 25, 0, 25)] // range wider than chip count
            [DataRow(25, 14, 66, 25, 14, 25, 14, 25)]
            // within valid range
            [DataRow(3, 1, 5, 2, 1, 3, 1, 2)]
            // jumping about max
            [DataRow(3, 1, 5, 7, 1, 3, 1, 5)]
            // reamain about max
            [DataRow(44, 1, 5, 7, 1, 5, 1, 5)]
            // falling below min
            [DataRow(3, 1, 5, 0, 1, 3, null, null)]
            // remain below min
            [DataRow(56, 128, 256, 72, null, null, null, null)]
            public void PlayerChipsChange(
                int preChipCount, int min, int max, int postChipCount,
                int? expectedPreMin, int? expectedPreMax,
                int? expectedPostMin, int? expectedPostMax)
            {
                IWallet w = new RWallet(preChipCount);
                IChipDisplay minL = new FakeChipDisplay();
                IChipDisplay maxL = new FakeChipDisplay();
                IBetSelectionMenu b = new RBetSelectionMenu(w, minL, maxL, new FakeTextEdit(), new FakeRange(), new FakeButton());
                b.Range = new(min, max);

                Assert.AreEqual(expectedPreMin, minL.Chips);
                Assert.AreEqual(expectedPreMax, maxL.Chips);

                w.Chips = postChipCount;

                Assert.AreEqual(expectedPostMin, minL.Chips);
                Assert.AreEqual(expectedPostMax, maxL.Chips);
            } // end PlayerChipsChange()


            [TestMethod]
            [DataRow(0, 0, 0, "0", 0, "0", 0d, 0, "0", 0d)]
            [DataRow(1, 0, 0, "0", 0, "0", 0d, 0, "0", 0d)]

            [DataRow(1, 0, 4, "2", 0, "0", 0d, 1, "1", 1d)] // text outside true range inside max range
            [DataRow(1, 1, 4, "2", 1, "1", 1d, 1, "1", 1d)]
            [DataRow(2, 1, 4, "3", 1, "1", 1d, 2, "2", 2d)]

            [DataRow(900, 1, 4, "3", 1, "1", 1d, 3, "3", 3d)]

            [DataRow(900, 1, 4, ".3", 1, "1", 1d, 1, "1", 1d)] // Not recognized number
            [DataRow(900, 1, 4, "23.3", 1, "1", 1d, 1, "1", 1d)]
            [DataRow(900, 1, 4, "k+12", 1, "1", 1d, 1, "1", 1d)]
            [DataRow(900, 1, 4, "-0.3", 1, "1", 1d, 1, "1", 1d)]
            [DataRow(900, 1, 4, "234k", 1, "1", 1d, 1, "1", 1d)]
            [DataRow(900, 1, 4, "_", 1, "1", 1d, 1, "1", 1d)]
            [DataRow(900, 0, 4, "90c", 0, "0", 0d, 0, "0", 0d)]
            public void BetTextChanged(int preChipCount, int min, int max,
                string text,
                int expectedPreSelection, string expectedPreText, double expectedPreSlider,
                int expectedPostSelection, string expectedPostText, double expectedPostSlider)
            {
                ITextEdit tE = new FakeTextEdit();
                IRange r = new FakeRange();
                IBetSelectionMenu b = new RBetSelectionMenu(
                    new RWallet(preChipCount), new FakeChipDisplay(), new FakeChipDisplay(),
                    tE, r, new FakeButton());
                b.Range = new(min, max);

                Assert.AreEqual(expectedPreSelection, b.Selected);
                Assert.AreEqual(expectedPreText, tE.Text);
                Assert.AreEqual(expectedPreSlider, r.Value);

                tE.Text = text;

                Assert.AreEqual(expectedPostSelection, b.Selected);
                Assert.AreEqual(expectedPostText, tE.Text);
                Assert.AreEqual(expectedPostSlider, r.Value);
            } // end BetTextChanged()


            [TestMethod]
            // inplace
            [DataRow(0, 0, 0, 0, 0, "0", 0d, 0, "0", 0d)]
            // inside value range
            [DataRow(77, 10, 100, 12, 10, "10", 10d, 12, "12", 12d)]
            [DataRow(12, 0, 14, 12, 0, "0", 0d, 12, "12", 12d)]
            // atside min value
            [DataRow(77, 0, 100, 0, 0, "0", 0d, 0, "0", 0d)]
            [DataRow(8, 8, 14, 8, 8, "8", 8d, 8, "8", 8d)]
            // atside max value
            [DataRow(12321, 10, 100, 100, 10, "10", 10d, 100, "100", 100d)]
            [DataRow(14, 0, 14, 14, 0, "0", 0d, 14, "14", 14d)]

            public void BetSliderChanged(int preChipCount, int min, int max,
                int value,
                int expectedPreSelection, string expectedPreText, double expectedPreSlider,
                int expectedPostSelection, string expectedPostText, double expectedPostSlider)
            {
                ITextEdit tE = new FakeTextEdit();
                IRange r = new FakeRange();
                IBetSelectionMenu b = new RBetSelectionMenu(
                    new FakeUser(chipCount: preChipCount).Wallet, new FakeChipDisplay(), new FakeChipDisplay(),
                    tE, r, new FakeButton());
                b.Range = new(min, max);

                Assert.AreEqual(expectedPreSelection, b.Selected);
                Assert.AreEqual(expectedPreText, tE.Text);
                Assert.AreEqual(expectedPreSlider, r.Value);

                r.Value = value;

                Assert.AreEqual(expectedPostSelection, b.Selected);
                Assert.AreEqual(expectedPostText, tE.Text);
                Assert.AreEqual(expectedPostSlider, r.Value);
            } // end BetTextChanged()

            [TestMethod]
            public void Dispose()
            {
                IWallet w = new RWallet(0);
                ITextEdit tE = new FakeTextEdit();
                IRange r = new FakeRange();
                IBetSelectionMenu b = new RBetSelectionMenu(w, new FakeChipDisplay(), new FakeChipDisplay(),
                    tE, r, new FakeButton());

                Assert.AreEqual(0d, r.MaxValue);

                w.Chips = 10;
                Assert.AreEqual(10d, r.MaxValue);

                b.Dispose();

                w.Chips = 100;
                Assert.AreEqual(10d, r.MaxValue); // post dispose update shouldn't occur.
            } // end Dispose()

            [TestMethod]
            public void SubmitTriggersClose()
            {
                IWallet w = new RWallet(0);
                ITextEdit tE = new FakeTextEdit();
                IRange r = new FakeRange();
                FakeButton submitBtn = new FakeButton();
                IBetSelectionMenu b = new RBetSelectionMenu(w, new FakeChipDisplay(), new FakeChipDisplay(),
                    tE, r, submitButton: submitBtn);

                int submitCount = 0;

                b.Open((bet) => submitCount += 1);

                Assert.AreEqual(0, submitCount);

                submitBtn.Press();

                Assert.AreEqual(1, submitCount);

                submitBtn.Press();

                Assert.AreEqual(1, submitCount); // detect close using side effect that callback should not be attached.
            } // end SubmitTriggersClose()

            [TestMethod]
            [DataRow(0, 0, 0, 0, "0", 0d)] // on min/max/mid/wallet
            [DataRow(0, 21, 32, 0, "0", 0d)] // on min
            [DataRow(4, 21, 9, 4, "4", 4d)]
            [DataRow(4, 21, 9, 5, "5", 5d)] // on mid
            [DataRow(4, 13, 9903409, 13, "13", 13d)] // on max
            [DataRow(0, 130, 7, 7, "7", 7d)] // on wallet
            public void Selected(int minBet, int maxBet, int playerChip, int selection,
                string exText, double exSlid)
            {
                ITextEdit tE = new FakeTextEdit();
                IRange r = new FakeRange();
                IBetSelectionMenu b = new RBetSelectionMenu(new RWallet(playerChip), new FakeChipDisplay(), new FakeChipDisplay(),
                    betEdit: tE, betSlider: r, submitButton: new FakeButton());
                b.Range = new VBetRange(min: minBet, max: maxBet);

                b.Selected = selection;

                Assert.AreEqual(exText, tE.Text);
                Assert.AreEqual(exSlid, r.Value);
            } // end Selected()
        } // end inner class Valid

        [TestClass]
        public class Invalid
        {
            [TestMethod]
            [ExpectedException(typeof(ArgumentOutOfRangeException))]
            [DataRow(0, 0, 0, 1)] // exceeds
            [DataRow(0, 20, 1000, 21)] // exceeds max range
            [DataRow(0, 20, 1000, -1)] // exceeds min range
            [DataRow(8, 20, 1000, -1)]
            [DataRow(8, 20, 1000, 6)]
            [DataRow(8, 20, 19, 20)] // exceeds the wallet based max. 
            public void Selected(int minBet, int maxBet, int playerChip, int selection)
            {
                IBetSelectionMenu b = new RBetSelectionMenu(new RWallet(playerChip), new FakeChipDisplay(), new FakeChipDisplay(),
                    new FakeTextEdit(), new FakeRange(), submitButton: new FakeButton());
                b.Range = new VBetRange(min: minBet, max: maxBet);

                b.Selected = selection;
            } // end Selected()
        } // end inner class Invalid
    } // end class
} // end namespace