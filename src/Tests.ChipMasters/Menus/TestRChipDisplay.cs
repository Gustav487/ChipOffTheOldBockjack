using ChipMasters.Menu.Displays.Chips;
using ChipMasters.Menu.Displays.Wallets;
using ChipMasters.User;
using Fakes.ChipMasters.Menus.Displays.Chips;

namespace Tests.ChipMasters.Menus
{
    public static class TestRChipDisplay
    {
        [TestClass]
        public class Valid
        {

            [TestMethod]
            [DataRow(0, 3, 4,
                null, 0, 3, 3)]
            [DataRow(11, 11, 99,
                null, 11, 11, 11)]
            public void Dispose(int initChipCount, int chipCount2, int chipCount3,
                int? exInitD, int? exPostCtorD, int? exD2, int? exD3)
            {
                IWallet w = new RWallet(initChipCount);
                IChipDisplay cd = new FakeChipDisplay();

                Assert.AreEqual(exInitD, cd.Chips);
                RWalletDisplay wd = new(w, cd);
                Assert.AreEqual(exPostCtorD, cd.Chips);

                w.Chips = chipCount2;
                Assert.AreEqual(exD2, cd.Chips);

                wd.Dispose();

                w.Chips = chipCount3;
                Assert.AreEqual(exD3, cd.Chips);
            } // end Dispose()

        } // end inner class Valid
    } // end class
} // end namespace