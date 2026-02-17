using ChipMasters.Cards;

namespace Tests.ChipMasters.Cards
{
    public static class TestRStandardCardType
    {
        [TestClass]
        public class Valid
        {
            [TestMethod]
            [DataRow(ECardRank.Seven, ECardSuit.Clubs)]
            [DataRow(ECardRank.Queen, ECardSuit.Spades)]
            public void IsTypeOfInstantiated(ECardRank rank, ECardSuit suit)
            {
                ICardType ct = new RStandardCardType(rank, suit);

                Assert.IsTrue(ct.IsTypeOf(ct.Instantiate()));
            } // end IsTypeOf()
        } // end Valid
    } // end class
} // end namespace