using ChipMasters.IO;
using ChipMasters.Registers;
using GSR.EnDecic;
using System;

namespace ChipMasters.Cards
{
    /// <summary>
    /// Common contract of a card.
    /// </summary>
    public interface ICard
    {
        /// <summary>
        /// <see cref="IEnDec{T}"/> for coding cards associated with <see cref="ICardType"/>s in the <see cref="SCardTypes.REGISTER"/>.
        /// </summary>
        public static readonly IEnDec<ICard> ENDEC = SCardTypes.REGISTER.TypeRegistryEnDec();

        /// <summary>
        /// The <see cref="ECardSuit"/> of the card.
        /// </summary>
        ECardSuit Suit { get; }

        /// <summary>
        /// The <see cref="ECardRank"/> of the card.
        /// </summary>
        ECardRank Rank { get; }

        /// <summary>
        /// Is the card face down?
        /// </summary>
        bool Veiled { get; set; }

        /// <summary>
        /// Event fired when card flips, ie it's <see cref="Veiled"/> value changed.
        /// </summary>
        event Action? OnFlipped;
    } // end interface
} // end namespace
