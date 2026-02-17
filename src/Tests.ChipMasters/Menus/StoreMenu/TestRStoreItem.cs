using ChipMasters.GodotWrappers;
using ChipMasters.Items;
using ChipMasters.Menu.StoreMenu;
using ChipMasters.Registers;
using ChipMasters.User;
using Fakes.ChipMasters.GodotWrappers;
using Fakes.ChipMasters.Items;
using Fakes.ChipMasters.Menus.StoreMenu;
using Fakes.ChipMasters.Users;

namespace Tests.ChipMasters.Menus.StoreMenu
{
    public static class TestRStoreItem
    {
        [TestClass]
        public class Valid
        {
            [TestMethod]
            [DataRow(0, EItemCategory.CardSkin)]
            [DataRow(1, EItemCategory.GameBackground)]
            public void PurchaseItem(int tabIndex, EItemCategory itemCategory)
            {
                IUser u = new FakeUser();
                IItem i = new FakeItem(itemCategory, "TestItem", 100);
                IButton b = new FakeButton();
                IStoreItem si = new RStoreItem(tabIndex, u, i, new FakeStoreMenu(), new FakeLabel(), b, b, b, b, b, b);

                bool wasPurchaseEventRaised = false;
                si.OnPurchase += () => wasPurchaseEventRaised = true;

                si.Purchase(i);

                Assert.IsTrue(u.Inventory.Items.Contains(i));
                Assert.IsTrue(wasPurchaseEventRaised);
            } // end PurchaseItem()

            [TestMethod]
            [DataRow(0, EItemCategory.CardSkin)]
            [DataRow(1, EItemCategory.GameBackground)]
            public void SellItem(int tabIndex, EItemCategory itemCategory)
            {
                IUser u = new FakeUser();
                IItem i = SItems.REGISTER.Where(item => item.Value.Category == itemCategory).First().Value;
                ((IApplicableItem)i).Apply(u);
                u.Inventory.Add(i);

                IButton b = new FakeButton();
                IStoreItem si = new RStoreItem(tabIndex, u, i, new FakeStoreMenu(), new FakeLabel(), b, b, b, b, b, b);

                Assert.IsTrue(u.Inventory.Items.Contains(i));

                bool wasSellEventRaised = false;
                si.OnSell += () => wasSellEventRaised = true;

                si.Sell(i);

                Assert.IsFalse(u.Inventory.Items.Contains(i));
                Assert.IsTrue(wasSellEventRaised);
            } // end SellItem()

            [TestMethod]
            [DataRow(0, EItemCategory.CardSkin)]
            [DataRow(1, EItemCategory.GameBackground)]
            public void Cycle(int tabIndex, EItemCategory itemCategory)
            {
                IEnumerable<IItem> itemsInCategory = SItems.REGISTER.GetAllItems().Where(i => i.Category == itemCategory);
                IItem i1 = itemsInCategory.ElementAt(0)!;

                IUser u = new FakeUser();
                IButton b = new FakeButton();
                IStoreMenu sm = new FakeStoreMenu();
                sm = new RStoreMenu(sm);
                IStoreItem si = new RStoreItem(tabIndex, u, i1, sm, new FakeLabel(), b, b, b, b, b, b);

                bool wasCycleEventRaised = false;
                si.OnCycle += () => wasCycleEventRaised = true;

                sm.CurrentTab = tabIndex;
                si.Cycle(false); // Cycle to next item

                if (itemsInCategory.Count() <= 1)
                    return; // Stop tests if only one item in category

                IItem i2 = itemsInCategory.ElementAt(1)!;
                Assert.AreEqual(i2.Name, si.Item.Name);
                Assert.IsTrue(wasCycleEventRaised);

                si.Cycle(true); // Cycle to previous item

                Assert.AreEqual(i1.Name, si.Item.Name);

                // Cycle past last item back to first item
                // Cycle() is 0-indexed, Count() starts at 1
                for (int i = 0; i < itemsInCategory.Count(); i++)
                    si.Cycle(false);
                Assert.AreEqual(i1.Name, si.Item.Name);
            } // end Cycle()

            [TestMethod]
            [DataRow(0, EItemCategory.CardSkin)]
            [DataRow(1, EItemCategory.GameBackground)]
            public void Select(int tabIndex, EItemCategory itemCategory)
            {
                IUser u = new FakeUser();
                IItem i = SItems.REGISTER.Where(item => item.Value.Category == itemCategory).First().Value;
                IButton b = new FakeButton();
                IStoreItem si = new RStoreItem(tabIndex, u, i, new FakeStoreMenu(), new FakeLabel(), b, b, b, b, b, b);

                bool wasSelectEventRaised = false;
                si.OnSelect += () => wasSelectEventRaised = true;

                si.Select(i);

                Assert.IsTrue(((IApplicableItem)i).IsApplied(u));
                Assert.IsTrue(wasSelectEventRaised);
            } // end Select()

            [TestMethod]
            [DataRow(0, EItemCategory.CardSkin)]
            [DataRow(1, EItemCategory.GameBackground)]
            public void Refresh(int tabIndex, EItemCategory itemCategory)
            {
                IUser u = new FakeUser();
                IItem i = SItems.REGISTER.Where(item => item.Value.Category == itemCategory).First().Value;
                IButton b = new FakeButton();
                IStoreItem si = new RStoreItem(tabIndex, u, i, new FakeStoreMenu(), new FakeLabel(), b, b, b, b, b, b);

                Assert.AreEqual(i.Name, si.Item.Name);

                i = SItems.REGISTER.Where(item => item.Value.Category == itemCategory).Last().Value;
                si.Item = i; // Set store item to last item in category
                si.Refresh();
                Assert.AreEqual(i.Name, si.Item.Name);
            } // end Refresh()
        } // end inner class Valid
    } // end class
} // end namespace
