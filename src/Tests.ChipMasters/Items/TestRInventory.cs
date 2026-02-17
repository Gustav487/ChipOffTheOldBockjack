using ChipMasters.Items;
using Fakes.ChipMasters.Items;

namespace Tests.ChipMasters.Items
{
    public static class TestRInventory
    {
        [TestClass]
        public class Valid
        {
            [TestMethod]
            public void IncludesDefaultItemsOnCreation()
            {
                IInventory inventory = new RInventory();
                Assert.IsTrue(inventory.Items.Count > 0);

                foreach (IItem item in inventory.Items)
                    Assert.IsTrue(item.Price == 0);
            } // end IncludesDefaultItemsOnCreation()

            [TestMethod]
            public void AddsItems()
            {
                IInventory inventory = new RInventory();
                IItem item = new FakeItem(EItemCategory.CardSkin, "TestItem", 100);
                inventory.Add(item);
                Assert.IsTrue(inventory.Items.Contains(item));
            } // end AddsItems()

            [TestMethod]
            public void RemovesItems()
            {
                IInventory inventory = new RInventory();
                IItem item = new FakeItem(EItemCategory.CardSkin, "TestItem", 100);
                inventory.Remove(item);
                Assert.IsFalse(inventory.Items.Contains(item));
            } // end RemovesItems()

            [TestMethod]
            public void DoesNotAddDuplicateItems()
            {
                IInventory inventory = new RInventory();
                IItem item = new FakeItem(EItemCategory.CardSkin, "TestItem", 100);
                inventory.Add(item);
                inventory.Add(item);
                Assert.AreEqual(1, inventory.Items.Count(i => i.Name == "TestItem"));
            } // end DoesNotAddDuplicateItems()
        } // end inner class Valid
    } // end class
} // end namespace
