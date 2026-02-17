using ChipMasters.Cards;

namespace Tests.ChipMasters.Cards
{
    public static class TestRCard
    {
        [TestClass]
        public class Valid
        {
            [TestMethod]
            [DataRow(ECardRank.Four, ECardSuit.Hearts, ECardRank.Five, ECardSuit.Diamonds, false)] // different different
            [DataRow(ECardRank.Nine, ECardSuit.Clubs, ECardRank.Nine, ECardSuit.Spades, false)] // same different
            [DataRow(ECardRank.Six, ECardSuit.Hearts, ECardRank.Queen, ECardSuit.Hearts, false)] // different same
            [DataRow(ECardRank.King, ECardSuit.Spades, ECardRank.King, ECardSuit.Spades, true)] // same same
            public void Equals(ECardRank rank1, ECardSuit suit1, ECardRank rank2, ECardSuit suit2, bool expectation)
            {
                RCard a = new RCard(rank1, suit1);
                RCard b = new RCard(rank2, suit2);

                Assert.AreEqual(expectation, a.Equals(b));
                Assert.AreEqual(expectation, b.Equals(a));
                Assert.AreEqual(expectation, a == b);
                Assert.AreEqual(expectation, b == a);
                Assert.AreEqual(!expectation, a != b);
                Assert.AreEqual(!expectation, b != a);

                ICard a2 = a;
                ICard b2 = b;
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