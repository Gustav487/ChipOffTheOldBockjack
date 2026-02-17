using ChipMasters.Util;
using GSR.EnDecic;
using GSR.EnDecic.EnDecs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace ChipMasters.Cards
{
    /// <summary>
    /// Represents a deck of playing cards.
    /// </summary>
    public class RDeck : IDeck
    {
        /// <summary>
        /// <see cref="IEnDec{T}"/> for coding <see cref="RDeck"/> instances.
        /// </summary>
        /// <param name="cardEnDec"></param>
        /// <returns></returns>
        public static IEnDec<RDeck> ENDEC(IEnDec<ICard> cardEnDec) => EnDecUtil.KeyedEnDecBuilder<string, RDeck>(EnDecUtil.STRING)
            .Add("prototype", cardEnDec.ListOf(), (x) => x.Prototype.ToList())
            .Add("state", cardEnDec.ListOf(), (x) => x)
            .Build((p, s) => new RDeck(p, s));



        /// <inheritdoc/>
        public IImmutableList<ICard> Prototype => _prototype;
        private readonly ImmutableList<ICard> _prototype;

        private readonly List<ICard> _cards = new();



        /// <inheritdoc/>
        public RDeck(params ICard[] cards) : this((IEnumerable<ICard>)cards) { } // end ctor

        /// <inheritdoc/>
        public RDeck(IEnumerable<ICard> cards, IEnumerable<ICard>? state = null)
        {
            _prototype = cards.AssertNotNull().Select((x) => x.AssertNotNull()).ToImmutableList();
            _cards.AddRange(state?.Select((x) => x.AssertNotNull()) ?? _prototype);
        } // end ctor



        /// <inheritdoc/>
        public void Restore()
        {
            _cards.Clear();
            _cards.AddRange(Prototype);
        } // end Restore();

        /// <summary>
        /// Randomizes the order of cards in the deck using Fisher-Yates shuffle.
        /// </summary>
        public void Shuffle() => _cards.Shuffled<List<ICard>, ICard>();

        /// <inheritdoc/>
        public ICard Draw()
        {
            if (_cards.Count == 0)
                throw new InvalidOperationException("Cannot draw from an empty deck.");

            ICard drawnCard = _cards[0];
            _cards.RemoveAt(0);
            return drawnCard;
        } // end Draw()



        // IList<ICard> Implementation
        /// <inheritdoc/>
        public int Count => _cards.Count;
        /// <inheritdoc/>
        public bool IsReadOnly => false;
        /// <inheritdoc/>
        public void Add(ICard card) => _cards.Add(card);
        /// <inheritdoc/>
        public void Clear() => _cards.Clear();
        /// <inheritdoc/>
        public bool Contains(ICard card) => _cards.Contains(card);
        /// <inheritdoc/>
        public void CopyTo(ICard[] array, int arrayIndex) => _cards.CopyTo(array, arrayIndex);
        /// <inheritdoc/>
        public bool Remove(ICard card) => _cards.Remove(card);
        /// <inheritdoc/>
        public IEnumerator<ICard> GetEnumerator() => _cards.GetEnumerator();
        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        /// <inheritdoc/>
        public int IndexOf(ICard card) => _cards.IndexOf(card);
        /// <inheritdoc/>
        public void Insert(int index, ICard card) => _cards.Insert(index, card);
        /// <inheritdoc/>
        public void RemoveAt(int index) => _cards.RemoveAt(index);
        /// <inheritdoc/>
        public ICard this[int index] { get => _cards[index]; set => _cards[index] = value; }
    } // end class
} // end namespace
