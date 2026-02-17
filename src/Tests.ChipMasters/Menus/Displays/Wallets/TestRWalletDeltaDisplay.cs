using ChipMasters.Menu.Displays.Chips;
using ChipMasters.Menu.Displays.Wallets;
using ChipMasters.User;
using Fakes.ChipMasters.Menus.Displays.Chips;

namespace Tests.ChipMasters.Menus.Displays.Wallets
{
    [TestClass]
    public class TestRWalletDeltaDisplay
    {
        [TestMethod]
        [DataRow(0, 1, 1)]
        [DataRow(300, 1, -299)]
        [DataRow(-4, 4, 8)]
        [DataRow(9, -23, -32)]
        [DataRow(0, 0, null)]
        [DataRow(-63, -63, null)]
        [DataRow(9, 9, null)]
        public void OnChipsChanged(int init, int change, int? ex)
        {
            IWallet w = new RWallet(startingChips: init);
            IChipDisplay c = new FakeChipDisplay();
            RWalletDeltaDisplay d = new(w, c);

            Assert.AreEqual(null, c.Chips);

            w.Chips = change;

            Assert.AreEqual(ex, c.Chips);
        } // end OnChipsChanged()

        [TestMethod]
        public void Dispose()
        {
            IWallet w = new RWallet(startingChips: 0);
            IChipDisplay c = new FakeChipDisplay();
            RWalletDeltaDisplay d = new(w, c);

            Assert.AreEqual(null, c.Chips);

            d.Dispose();
            w.Chips = 24243;

            Assert.AreEqual(null, c.Chips);
        } // end Dispose()
    } // end class
} // end namespace