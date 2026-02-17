using ChipMasters.IO;
using GSR.EnDecic;

namespace ChipMasters.Games.Appraisers
{
    /// <summary>
    /// Appraiser according to the standard blackjack logic.
    /// </summary>
    public sealed class RStandardAppraiser : ASubstandardAppraiser
    {
        /// <summary>
        /// <see cref="IEnDec{T}"/> for coding <see cref="RStandardAppraiser"/> instances.
        /// </summary>
        public static readonly IEnDec<RStandardAppraiser> ENDEC = new RConstantEnDec<RStandardAppraiser>(new());



        /// <inheritdoc/>
        public RStandardAppraiser()
            : base(21) { } // end ctor



        /// <inheritdoc/>
        protected override bool IsBlackjack(int total, int cardCount)
            => total == 21 && cardCount == 2;

    } // end class
} // end namespace