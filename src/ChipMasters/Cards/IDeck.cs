using GSR.EnDecic;
using GSR.EnDecic.EnDecs;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace ChipMasters.Cards
{
    /// <summary>
    /// Contract for an object representing a deck of <see cref="ICard"/>s.
    /// </summary>
    public interface IDeck : IList<ICard>
    {
        /// <summary>
        /// Simple <see cref="IEnDec{T}"/> that codes <see cref="IDeck"/>s.
        /// </summary>
        public static readonly IEnDec<IDeck> ENDEC = EnDecUtil.KeyedEnDecBuilder<string, IDeck>(EnDecUtil.STRING)
            .Add("prototype", ICard.ENDEC.ListOf(), (x) => x.Prototype.ToList())
            .Add("state", ICard.ENDEC.ListOf(), (x) => x)
            .Build((p, s) => new RDeck(p, s));



        /// <summary>
        /// The cards that're held in the deck by default.
        /// </summary>
        IImmutableList<ICard> Prototype { get; }

        /// <summary>
        /// Randomize the order of the entire deck.
        /// </summary>
        void Shuffle();

        /// <summary>
        /// Take the card from the top of the deck (index 0).
        /// </summary>
        /// <returns>The drawn <see cref="ICard"/>.</returns>
        ICard Draw();

        /// <summary>
        /// Restore all cards to the deck and reset the order.
        /// </summary>
        void Restore();
    } // end class
} // end namespace
