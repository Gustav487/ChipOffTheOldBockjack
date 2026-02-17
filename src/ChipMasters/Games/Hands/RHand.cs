using ChipMasters.Cards;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ChipMasters.Games.Hands
{
    /// <summary>
    /// Simple <see cref="IHand"/> implementation.
    /// </summary>
    public class RHand : IHand
    {
        /// <inheritdoc/>
        public int Count => _hand.Count;

        /// <inheritdoc/>
        public ICard this[int index] => _hand[index];

        /// <inheritdoc/>
        public event Action<ICard>? OnCardAdded;



        /// <inheritdoc/>
        [Obsolete]
        public IReadOnlyList<ICard> Cards => _hand;
        private readonly List<ICard> _hand = new List<ICard>();



        /// <inheritdoc/>
        public RHand() { } // end ctor

        /// <inheritdoc/>
        public RHand(IEnumerable<ICard> cards)
        {
            _hand = cards.AssertNotNull().Select((x) => x.AssertNotNull()).ToList();
        } // end ctor



        /// <inheritdoc/>
        public void AddCard(ICard card)
        {
            _hand.Add(card);
            OnCardAdded?.Invoke(card);
        } // end AddCard()



        /// <inheritdoc/>
        public IEnumerator<ICard> GetEnumerator() => _hand.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    } // end class
} // end namespace 
