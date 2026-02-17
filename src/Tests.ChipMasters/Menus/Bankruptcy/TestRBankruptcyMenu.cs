using ChipMasters.GodotWrappers;
using ChipMasters.Items;
using ChipMasters.Menu.Bankruptcy;
using ChipMasters.User;
using Fakes.ChipMasters.GodotWrappers;
using Fakes.ChipMasters.Items;
using Fakes.ChipMasters.Menus.Bankruptcy;
using Fakes.ChipMasters.Users;

namespace Tests.ChipMasters.Menus.Bankruptcy
{
    public static class TestRBankruptcyMenu
    {
        [TestClass]
        public class Valid
        {
            [DataRow(0, true, false, true, true)]  // No items: Payout granted, menu visible, info hidden, continue visible
            [DataRow(1, true, true, false, false)] // Has item: Menu visible, info visible, payout hidden, continue hidden
            [DataRow(5, true, true, false, false)]
            [TestMethod]
            public void BankruptcyPayoutAndButtons(
                int itemCount,
                bool expectedMenuVisible,
                bool expectedInfoVisible,
                bool expectedPayoutVisible,
                bool expectedContinueVisible)
            {
                IUser u = new FakeUser();
                IBankruptcyMenu m = new FakeBankruptcyMenu();
                IBaseButton b = new FakeButton();
                ILabel l1 = new FakeLabel();
                ILabel l2 = new FakeLabel();
                IConfirmationDialog cd = new FakeConfirmationDialog();
                IBankruptcyMenu bm = new RBankruptcyMenu(u, m, new FakeControl(), new FakeContainer(), b, l1, l2, cd);

                for (int i = 0; i < itemCount; i++)
                    u.Inventory.Add(new FakeItem(EItemCategory.CardSkin, $"test_{i}", 100));

                bm.Open();

                Assert.AreEqual(expectedMenuVisible, m.Visible);
                Assert.AreEqual(expectedInfoVisible, l1.Visible);
                Assert.AreEqual(expectedPayoutVisible, l2.Visible);
                Assert.AreEqual(expectedContinueVisible, b.Visible);

                if (itemCount == 0)
                {
                    int chipCount = u.Wallet.Chips;
                    u.Wallet.GiveBankruptcyPayout();
                    Assert.AreEqual(u.Wallet.Chips, chipCount + SIWalletExtensions.DEFAULT_CHIPS);
                }

                bm.Close();
                Assert.AreEqual(!expectedMenuVisible, m.Visible);
            } // end BankruptcyPayoutAndButtons()

            [TestMethod]
            public void BankruptcyConfirmationBox()
            {
                IUser u = new FakeUser();
                IBankruptcyMenu m = new FakeBankruptcyMenu();
                IBaseButton b = new FakeButton();
                ILabel l = new FakeLabel();
                IConfirmationDialog cd = new FakeConfirmationDialog();
                IBankruptcyMenu bm = new RBankruptcyMenu(u, m, new FakeControl(), new FakeContainer(), b, l, l, cd);

                bm.ShowBankruptcyBox();

                Assert.AreEqual(true, cd.Visible);
            } // end BankruptcyConfirmationBox()
        } // end inner class Valid
    } // end class
} // end namespace
