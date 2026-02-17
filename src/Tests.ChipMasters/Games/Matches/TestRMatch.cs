using ChipMasters;
using ChipMasters.Cards;
using ChipMasters.Games.Appraisers;
using ChipMasters.Games.Matches;
using Fakes.ChipMasters.Cards;
using GSR.Utilic.Generic;

namespace Tests.ChipMasters.Games.Matches
{
    public class TestRMatch
    {
        [TestClass]
        public class Valid
        {
            // Test case 1: Simulating a game where the player wins
            [TestMethod]
            public void PlayerWins()
            {
                // Create a controlled deck that returns predefined cards
                var mockDeck = new FakeDeck(new ICard[]
                {
                new FakeCard(ECardRank.Seven, ECardSuit.Hearts),   // Player's first card
                new FakeCard(ECardRank.Five, ECardSuit.Spades),   // Dealer's first card
                new FakeCard(ECardRank.Six, ECardSuit.Diamonds), // Player's second card
                new FakeCard(ECardRank.Eight, ECardSuit.Clubs),    // Dealer's second card
                new FakeCard(ECardRank.Eight, ECardSuit.Hearts),   // Player hits and gets a 9
                new FakeCard(ECardRank.Ace, ECardSuit.Hearts),   // 14
                new FakeCard(ECardRank.Ace, ECardSuit.Hearts),   // 15
                new FakeCard(ECardRank.Ace, ECardSuit.Hearts),   // 16
                new FakeCard(ECardRank.Ace, ECardSuit.Hearts)   // 17
                });

                var match = new RMatch(mockDeck, mockDeck, 10);

                // Start the game and simulate player actions
                match.Hit();  // Player hits, adds another card (9)
                match.Stand(); // Player stands, dealer's turn

                // Verify the result
                VHandAppraisal p = match.AppraisePlayerHand(false);
                VHandAppraisal d = match.AppraiseDealerHand(false);

                Assert.AreEqual(EHandState.Neutral, p.State);
                Assert.AreEqual(EHandState.Neutral, d.State);
                Assert.AreEqual(true, p.TotalValue > d.TotalValue);

                match.DealerHand.ForEvery((x) => Assert.IsFalse(x.Veiled));
                match.PlayerHand.ForEvery((x) => Assert.IsFalse(x.Veiled));
            } // end PlayerWins()

            // Test case 2: Simulating a game where the player busts
            [TestMethod]
            public void PlayerBusts()
            {
                var mockDeck = new FakeDeck(new ICard[]
                {
                new FakeCard(ECardRank.Seven, ECardSuit.Hearts),   // Player's first card
                new FakeCard(ECardRank.Five, ECardSuit.Spades),   // Dealer's first card
                new FakeCard(ECardRank.Six, ECardSuit.Diamonds), // Player's second card
                new FakeCard(ECardRank.Eight, ECardSuit.Clubs),    // Dealer's second card
                new FakeCard(ECardRank.Nine, ECardSuit.Spades)    // Player hits and busts with a 9 (Total 22)
                });

                var match = new RMatch(mockDeck, mockDeck, 10);

                // Simulate actions
                match.Hit(); // Player hits and busts

                // Verify the result
                VHandAppraisal p = match.AppraisePlayerHand(false);
                VHandAppraisal d = match.AppraiseDealerHand(false);

                Assert.AreEqual(EHandState.Bust, p.State);
                Assert.AreEqual(EHandState.Neutral, d.State);

                match.DealerHand.ForEvery((x) => Assert.IsFalse(x.Veiled));
                match.PlayerHand.ForEvery((x) => Assert.IsFalse(x.Veiled));
            } // end PlayerBusts()

            // Test case 3: Simulating a game where the dealer busts
            [TestMethod]
            public void DealerBusts()
            {
                var mockDeck = new FakeDeck(new ICard[]
                {
                new FakeCard(ECardRank.Seven, ECardSuit.Hearts),   // Player's first card
                new FakeCard(ECardRank.Five, ECardSuit.Spades),   // Dealer's first card
                new FakeCard(ECardRank.Six, ECardSuit.Diamonds), // Player's second card
                new FakeCard(ECardRank.Eight, ECardSuit.Clubs),    // Dealer's second card
                new FakeCard(ECardRank.King, ECardSuit.Hearts),  // Dealer hits and gets K (Total 15)
                                                              //new Card("Clubs", "9", 9)     // Dealer busts with a 9 (Total 24)
                });

                var match = new RMatch(mockDeck, mockDeck, 10);

                match.Stand(); // Player stands, dealer's turn
                /*            match.Hit();   // Dealer hits, gets a King
                            match.Hit();   // Dealer busts with a 9*/

                // Verify the result
                VHandAppraisal p = match.AppraisePlayerHand(false);
                VHandAppraisal d = match.AppraiseDealerHand(false);

                Assert.AreEqual(EHandState.Neutral, p.State);
                Assert.AreEqual(EHandState.Bust, d.State);

                match.DealerHand.ForEvery((x) => Assert.IsFalse(x.Veiled));
                match.PlayerHand.ForEvery((x) => Assert.IsFalse(x.Veiled));
            } // end DealerBusts()

            // Test case 4: Simulating a tie scenario
            [TestMethod]
            public void Tie()
            {
                var mockDeck = new FakeDeck(new ICard[]
                {
                new FakeCard(ECardRank.Seven, ECardSuit.Hearts),   // Player's first card
                new FakeCard(ECardRank.Seven, ECardSuit.Spades),   // Dealer's first card
                new FakeCard(ECardRank.Queen, ECardSuit.Diamonds), // Player's second card
                new FakeCard(ECardRank.Jack, ECardSuit.Clubs)     // Dealer's second card
                });

                var match = new RMatch(mockDeck, mockDeck, 10);

                match.Stand(); // Both player and dealer have equal hand value

                // Verify the result
                VHandAppraisal p = match.AppraisePlayerHand(false);
                VHandAppraisal d = match.AppraiseDealerHand(false);

                Assert.AreEqual(EHandState.Neutral, p.State);
                Assert.AreEqual(EHandState.Neutral, d.State);
                Assert.AreEqual(true, p.TotalValue == d.TotalValue);

                match.DealerHand.ForEvery((x) => Assert.IsFalse(x.Veiled));
                match.PlayerHand.ForEvery((x) => Assert.IsFalse(x.Veiled));
            } // end Tie()

            // Test case 5: Simulating a blackjack scenario where the player wins with blackjack
            [TestMethod]
            public void PlayerBlackjackWins()
            {
                var mockDeck = new FakeDeck(new ICard[]
                {
                new FakeCard(ECardRank.Ace, ECardSuit.Hearts),  // Player's first card (Blackjack)
                new FakeCard(ECardRank.Five, ECardSuit.Spades),   // Dealer's first card
                new FakeCard(ECardRank.Ten, ECardSuit.Spades), // Player's second card (Blackjack)
                new FakeCard(ECardRank.Eight, ECardSuit.Diamonds),     // Dealer's second card
                new FakeCard(ECardRank.Two, ECardSuit.Diamonds),
                new FakeCard(ECardRank.Two, ECardSuit.Diamonds),
                new FakeCard(ECardRank.Two, ECardSuit.Diamonds),
                new FakeCard(ECardRank.Two, ECardSuit.Diamonds),
                new FakeCard(ECardRank.Two, ECardSuit.Diamonds),
                new FakeCard(ECardRank.Two, ECardSuit.Diamonds),
                new FakeCard(ECardRank.Two, ECardSuit.Diamonds),
                new FakeCard(ECardRank.Two, ECardSuit.Diamonds)
                });

                var match = new RMatch(mockDeck, mockDeck, 10);

                // Simulate actions
                // match.Stand(); // Player has Blackjack, dealer's turn

                // Verify the result
                VHandAppraisal p = match.AppraisePlayerHand(false);
                VHandAppraisal d = match.AppraiseDealerHand(false);

                Assert.AreEqual(EHandState.Blackjack, p.State);
                Assert.AreEqual(EHandState.Neutral, d.State);

                match.DealerHand.ForEvery((x) => Assert.IsFalse(x.Veiled));
                match.PlayerHand.ForEvery((x) => Assert.IsFalse(x.Veiled));
            } // end PlayerBlackjackWins()

            // Test case 6: Simulating a dealer blackjack
            [TestMethod]
            public void DealerBlackjack()
            {
                var mockDeck = new FakeDeck(new ICard[]
                {
                new FakeCard(ECardRank.Five, ECardSuit.Hearts),   // Player's first card
                new FakeCard(ECardRank.Ten, ECardSuit.Spades),   // Dealer's first card
                new FakeCard(ECardRank.Ten, ECardSuit.Spades), // Player's second card
                new FakeCard(ECardRank.Ace, ECardSuit.Clubs)    // Dealer's second card (Blackjack)
                });

                var match = new RMatch(mockDeck, mockDeck, 10);

                // match.Stand(); // Player stands, dealer's turn

                // Verify the result
                VHandAppraisal p = match.AppraisePlayerHand(false);
                VHandAppraisal d = match.AppraiseDealerHand(false);

                Assert.AreEqual(EHandState.Neutral, p.State);
                Assert.AreEqual(EHandState.Blackjack, d.State);

                match.DealerHand.ForEvery((x) => Assert.IsFalse(x.Veiled));
                match.PlayerHand.ForEvery((x) => Assert.IsFalse(x.Veiled));
            } // end DealerBlackjack()

            // Test case 7: Verify OnCardAdded event subscriber works correctly
            [TestMethod]
            public void OnCardAddedToPlayerHand()
            {
                // Set up a controlled deck with predefined cards
                var mockDeck = new FakeDeck(new ICard[]
                {
                    new FakeCard(ECardRank.Queen, ECardSuit.Hearts),
                    new FakeCard(ECardRank.Queen, ECardSuit.Spades),
                    new FakeCard(ECardRank.Queen, ECardSuit.Diamonds),
                    new FakeCard(ECardRank.Queen, ECardSuit.Clubs)
                });

                var match = new RMatch(mockDeck, mockDeck, 10);

                // Simulate adding a card to the player's hand
                match.PlayerHand.AddCard(new FakeCard(ECardRank.Two, ECardSuit.Clubs));  // Player adds a card

                // Assert that the match ended in the player's bust
                VHandAppraisal p = match.AppraisePlayerHand(false);
                VHandAppraisal d = match.AppraiseDealerHand(false);

                Assert.AreEqual(EHandState.Bust, p.State);
                Assert.AreEqual(EHandState.Neutral, d.State);

                match.DealerHand.ForEvery((x) => Assert.IsFalse(x.Veiled));
                match.PlayerHand.ForEvery((x) => Assert.IsFalse(x.Veiled));
            } // end OnCardAddedToPlayerHand()
        } // end inner class Valid

        [TestClass]
        public class Invalid
        {
            [TestMethod]
            [ExpectedException(typeof(RGameOverException))]
            public void HitAfterConclusion()
            {
                var mockDeck = new FakeDeck(new ICard[]
                {
                    new FakeCard(ECardRank.Five, ECardSuit.Hearts),   // Player's first card
                    new FakeCard(ECardRank.Ten, ECardSuit.Spades),   // Dealer's first card
                    new FakeCard(ECardRank.Ten, ECardSuit.Spades), // Player's second card
                    new FakeCard(ECardRank.Ace, ECardSuit.Clubs)    // Dealer's second card (Blackjack)
                });

                var match = new RMatch(mockDeck, mockDeck, 133330);

                match.Stand(); // Player stands, dealer's turn

                VHandAppraisal p = match.AppraisePlayerHand(false);
                VHandAppraisal d = match.AppraiseDealerHand(false);

                Assert.AreEqual(EHandState.Neutral, p.State);
                Assert.AreEqual(EHandState.Blackjack, d.State);

                match.DealerHand.ForEvery((x) => Assert.IsFalse(x.Veiled));
                match.PlayerHand.ForEvery((x) => Assert.IsFalse(x.Veiled));

                match.Hit();
            } // end HitAfterConclusion()
        } // end Invalid()
    } // end class
} // end namespace