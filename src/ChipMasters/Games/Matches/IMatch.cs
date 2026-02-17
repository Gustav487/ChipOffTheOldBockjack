using ChipMasters.Games.Appraisers;
using ChipMasters.Games.Hands;
using ChipMasters.IO;
using ChipMasters.Registers;
using GSR.EnDecic;
using System;

namespace ChipMasters.Games.Matches
{
    /// <summary>
    /// Contract for a blackjack match.
    /// </summary>
    public interface IMatch
    {
        /// <summary>
        /// <see cref="IEnDec{T}"/> for coding <see cref="IMatch"/> associated with <see cref="IMatch"/> <see cref="IType{T}"/>s in the <see cref="SMatchTypes.REGISTER"/>.
        /// </summary>
        public static readonly IEnDec<IMatch> ENDEC = SMatchTypes.REGISTER.TypeRegistryEnDec();



        /// <summary>
        /// <see cref="IAppraiser"/> used to determine if the match must end.
        /// </summary>
        public IAppraiser Appraiser { get; }

        /// <summary>
        /// The dealer's hand
        /// </summary>
        public IHand DealerHand { get; }

        /// <summary>
        /// The players's hand.
        /// </summary>
        public IHand PlayerHand { get; }

        /// <summary>
        /// Has the match been ended.
        /// </summary>
        public bool IsConcluded { get; }

        /// <summary>
        /// Event fired after the match has concluded.
        /// </summary>
        public event Action? OnConcluded;

        /// <summary>
        /// Bet player made when starting the round.
        /// </summary>
        public int Bet { get; }



        /// <summary>
        /// Player hits (draws a card).
        /// </summary>
        public void Hit();

        /// <summary>
        /// Player stands (ends their turn).
        /// </summary>
        public void Stand();
    } // end class
} // end namespace
