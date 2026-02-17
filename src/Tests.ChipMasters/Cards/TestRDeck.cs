using ChipMasters.Cards;
using ChipMasters.Registers;
using System.Collections.Immutable;

namespace Tests.ChipMasters.Cards
{
    /// <summary>
    /// Unit tests for the Deck class using MSTest.
    /// </summary>
    public static class TestRDeck
    {
        private static readonly IImmutableList<ICard> CARD_LIST = SCardTypes.REGISTER.Values.Select((x) => x.Instantiate()).ToImmutableList();

        [TestClass]
        public class Valid
        {
            /// <summary>
            /// Ensures that calling Restore() initializes a deck with exactly 52 cards.
            /// </summary>
            [TestMethod]
            public void Restore()
            {
                // Arrange: Create a new deck instance
                IDeck deck = new RDeck(CARD_LIST);

                deck.Shuffle(); // shuffle the deck
                deck.Draw();
                deck.Restore(); // restore deck to initial state;

                Assert.IsTrue(deck.SequenceEqual(deck.Prototype)); // verify deck is equal to prototypical state
            } // end Restore()

            /// <summary>
            /// Ensures that drawing a card reduces the deck size by one.
            /// </summary>
            [TestMethod]
            public void Draw()
            {
                // Arrange: Create a deck and store its initial count
                var deck = new RDeck(CARD_LIST);
                int initialCount = deck.Count;

                // Act: Draw one card
                ICard first = deck[0];
                ICard second = deck[1];
                ICard drawn = deck.Draw();

                // Assert: The deck size should decrease by 1
                Assert.AreEqual(initialCount - 1, deck.Count);

                Assert.AreEqual(first, drawn); // card drawn should've been from the front
                Assert.AreEqual(second, deck[0]); // card positions should be shifted.
            } // end draw()

            /// <summary>
            /// Ensures that shuffling the deck changes the order of cards.
            /// </summary>
            [TestMethod]
            public void Shuffle()
            {
                // Arrange: Create a new deck and store its original order
                IDeck deck = new RDeck(CARD_LIST);
                deck.Restore();
                IList<ICard> originalOrder = deck.ToList();

                // Act: Shuffle the deck
                deck.Shuffle();
                IList<ICard> shuffledOrder = deck.ToList();

                // Assert: The shuffled order should not be identical to the original order
                Assert.IsFalse(originalOrder.SequenceEqual(shuffledOrder), "Deck order should change after shuffle.");
            } // end Shuffle()

            /// <summary>
            /// Ensures the deck can be manipulated in various ways.
            /// </summary>
            [TestMethod]
            public void ManipulateDeck()
            {
                ICard card = CARD_LIST[0];
                IDeck deck = new RDeck(); // empty deck

                // Add & Contains
                Assert.AreEqual(0, deck.Count);
                deck.Add(card);
                Assert.AreEqual(1, deck.Count);
                Assert.IsTrue(deck.Contains(card));

                // Remove
                bool removed = deck.Remove(card);
                Assert.IsTrue(removed);
                Assert.AreEqual(0, deck.Count);

                // Clear
                deck.Add(card);
                deck.Clear();
                Assert.AreEqual(0, deck.Count);

                // Insert & IndexOf
                deck.Insert(0, card);
                Assert.AreEqual(0, deck.IndexOf(card));
                Assert.AreEqual(card, deck[0]);

                // RemoveAt
                deck.RemoveAt(0);
                Assert.AreEqual(0, deck.Count);
            } // end ManipulateDeck()

            /// <summary>
            /// Ensures the deck can be copied.
            /// </summary>
            [TestMethod]
            public void CopyTo()
            {
                IDeck deck = new RDeck(CARD_LIST);
                ICard[] array = new ICard[deck.Count];
                deck.CopyTo(array, 0);
                Assert.AreEqual(deck.Count, array.Length);
                Assert.IsTrue(array.SequenceEqual(deck));
            } // end CopyTo()

            /// <summary>
            /// Ensures cards in the deck can get accessed.
            /// </summary>
            [TestMethod]
            public void GetAndSetCardInDeck()
            {
                ICard card1 = CARD_LIST[0];
                ICard card2 = CARD_LIST[1];
                IDeck deck = new RDeck(card1);
                Assert.AreEqual(card1, deck[0]);
                deck[0] = card2;
                Assert.AreEqual(card2, deck[0]);
            } // end GetAndSetCardInDeck()
        } // end inner class Valid

        [TestClass]
        public class Invalid
        {
            /// <summary>
            /// Ensures that drawing from an empty deck throws an InvalidOperationException.
            /// </summary>
            [TestMethod]
            [ExpectedException(typeof(InvalidOperationException))]
            public void Draw()
            {
                var deck = new RDeck(); // create empty deck
                deck.Draw(); //  Attempting to draw from an empty deck should throw an exception
            } // end Draw()
        } // end inner class Invalid

    } // end class
} // end namespace 