using ChipMasters.Items;
using ChipMasters.Menu.StoreMenu;
using Fakes.ChipMasters.Menus.StoreMenu;

namespace Tests.ChipMasters.Menus.StoreMenu
{
    public static class TestRStoreMenu
    {
        [TestClass]
        public class Valid
        {
            [DataRow(0, EItemCategory.CardSkin)]
            [DataRow(1, EItemCategory.GameBackground)]
            [TestMethod]
            public void ReturnCorrectCategoryForTab(int categoryValue, EItemCategory? expectedCategory)
            {
                IStoreMenu s = new RStoreMenu(new FakeStoreMenu());
                EItemCategory c = s.GetCategoryForTab(categoryValue);
                Assert.AreEqual(expectedCategory, c);
            } // end ReturnCorrectCategoryForTab()
        } // end inner class Valid
    } // end class
} // end namespace
