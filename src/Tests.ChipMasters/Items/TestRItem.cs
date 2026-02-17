using ChipMasters.Items;

namespace Tests.ChipMasters.Items
{
    public static class TestRItem
    {
        [TestClass]
        public class Valid
        {
            [TestMethod]
            [DataRow(EItemCategory.GameBackground, "Bronze Sleeve", 100, EItemCategory.GameBackground, "Silver Sleeve", 250, false)] // same different different
            [DataRow(EItemCategory.Null, "Bronze Sleeve", 100, EItemCategory.Null, "Bronze Sleeve", 100, true)] // same same same
            public void Equals(EItemCategory type1, string name1, int price1, EItemCategory type2, string name2, int price2, bool expectation)
            {
                RItem a = new RItem(type1, name1, price1);
                RItem b = new RItem(type2, name2, price2);

                Assert.AreEqual(expectation, a.Equals(b));
                Assert.AreEqual(expectation, b.Equals(a));
                Assert.AreEqual(expectation, a == b);
                Assert.AreEqual(expectation, b == a);
                Assert.AreEqual(!expectation, a != b);
                Assert.AreEqual(!expectation, b != a);

                IItem a2 = a;
                IItem b2 = b;
                Assert.AreEqual(expectation, a2.Equals(b2));
                Assert.AreEqual(expectation, b2.Equals(a2));
                Assert.AreEqual(false, a2 == b2);
                Assert.AreEqual(false, b2 == a2);
                Assert.AreEqual(true, a2 != b2);
                Assert.AreEqual(true, b2 != a2);
            } // end Equals()
        } // end Valid
    } // end class
} // end namespace