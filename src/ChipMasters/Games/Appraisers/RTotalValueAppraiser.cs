using GSR.EnDecic;
using GSR.EnDecic.EnDecs;

namespace ChipMasters.Games.Appraisers
{
    /// <summary>
    /// Appraiser implementation that counts any hand with a total value of 21 as a blackjack.
    /// This deviates from the traditional rule (which only recognizes a two-card blackjack)
    /// and also considers the best use of Aces as either 1 or 11.
    /// </summary>
    public class RTotalValueAppraiser : ASubstandardAppraiser
    {
        /// <summary>
        /// <see cref="ENDEC"/>'s key for the <see cref="ASubstandardAppraiser._maxValue"/> field.
        /// </summary>
        public const string MAX_VALUE_KEY = "max_value";

        /// <summary>
        /// <see cref="IEnDec{T}"/> for coding <see cref="RStandardAppraiser"/> instances.
        /// </summary>
        public static readonly IEnDec<RTotalValueAppraiser> ENDEC = EnDecUtil.KeyedEnDecBuilder<string, RTotalValueAppraiser>(EnDecUtil.STRING)
            .Add(MAX_VALUE_KEY, EnDecUtil.INT_32, (x) => x._maxValue)
            .Build((mv) => new(mv));



        /// <inheritdoc/>
        public RTotalValueAppraiser(int max)
            : base(max) { } // end ctor



        /// <inheritdoc/>
        protected override bool IsBlackjack(int total, int cardCount) => total == _maxValue;
    } // end class
} // end namespace