using ChipMasters.Items;

namespace Tests.ChipMasters.Items
{
    public static class TestSEItemCategoryExtensions
    {
        [TestClass]
        public class Valid
        {
            [TestMethod]
            [DataRow(EItemCategory.Null, "Null")]
            [DataRow(EItemCategory.CardSkin, "Card Skin")]
            [DataRow(EItemCategory.GameBackground, "Game Background")]
            [DataRow((EItemCategory)9090, "9090")]
            public void DisplayText(EItemCategory e, string ex)
            {
                Assert.AreEqual(ex, e.DisplayText());
            } // end DisplayText()
        } // end inner class Valid()
    } // end class
} // end namespace