using ChipMasters.Cards;
using ChipMasters.Games.Hands;
using Fakes.ChipMasters.Cards;

namespace Tests.ChipMasters.Games.Hands
{
    public static class TestRHand
    {
        [TestClass]
        public class Valid
        {
            [TestMethod]
            public void OnCardAdded()
            {
                // Create a new RHand
                IHand hand = new RHand();
                bool eventTriggered = false;

                // Subscribe to the OnCardAdded event
                hand.OnCardAdded += (card) =>
                {
                    eventTriggered = true;
                };

                // Add a card to the hand, which should trigger the event
                var card = new FakeCard(ECardRank.Seven, ECardSuit.Hearts);
                hand.AddCard(card);

                // Verify that the event was triggered
                Assert.IsTrue(eventTriggered);
            } // end OnCardAdded()
        } // end inner class Valid

    } // end class
} // end namespace