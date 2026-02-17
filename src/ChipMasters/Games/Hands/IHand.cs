using ChipMasters.Cards;
using GSR.EnDecic;
using GSR.EnDecic.EnDecs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ChipMasters.Games.Hands
{
    /// <summary>
    /// Common contract for a participant in a blackjack match
    /// </summary>
    public interface IHand : IEnumerable<ICard>
    {
        /// <summary>
        /// Simple <see cref="IEnDec{T}"/> that codes <see cref="IHand"/>s.
        /// </summary>
        public static readonly IEnDec<IHand> ENDEC = ICard.ENDEC.ListOf().Map((IHand x) => x.ToList(), (x) => new RHand(x));

        /// <summary>
        /// Number of <see cref="ICard"/>s in <see cref="IHand"/>.
        /// </summary>
        public int Count { get; }

        /// <summary>
        /// Get the <see cref="ICard"/> at a given position in the <see cref="IHand"/>.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public ICard this[int index] { get; }



        /// <summary>
        /// View of the cards held.
        /// </summary>
        [Obsolete("use direct indexer/count property/or enumerator.")]
        public IReadOnlyList<ICard> Cards { get; }



        /// <summary>
        /// Event triggered when a new card is added to the hand.
        /// </summary>
        event Action<ICard>? OnCardAdded;

        /// <summary>
        /// Add a new card to the hand
        /// </summary>
        /// <param name="card"></param>
        public void AddCard(ICard card);
    } // end interface
} // end namespace
