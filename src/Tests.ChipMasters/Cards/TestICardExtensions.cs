using ChipMasters.Cards;

namespace Tests.ChipMasters.Cards
{
    public class TestICardExtensions
    {
        [TestClass]
        public class Valid
        {
            [TestMethod]
            [DataRow(ECardRank.Four, ECardSuit.Hearts)]
            [DataRow(ECardRank.Nine, ECardSuit.Clubs)]
            [DataRow(ECardRank.Six, ECardSuit.Hearts)]
            [DataRow(ECardRank.King, ECardSuit.Spades)]
            public void Clone(ECardRank rank, ECardSuit suit)
            {
                ICard c1 = new RCard(rank, suit);
                ICard c2 = c1.Clone();
                Assert.AreEqual(c1, c2);
            } // end Clone()
        } // end inner class Valid
    } // end class
} // end namespace
