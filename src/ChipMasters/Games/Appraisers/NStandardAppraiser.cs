using ChipMasters.Games.Hands;
using ChipMasters.GodotWrappers;
using GSR.EnDecic;
using GSR.EnDecic.EnDecs;

namespace ChipMasters.Games.Appraisers
{

    /// <summary>
    /// <see cref="NNode"/> based <see cref="IAppraiser"/> that wraps a <see cref="RStandardAppraiser"/>.
    /// </summary>
    public sealed partial class NStandardAppraiser : NNode, IAppraiser
    {
        /// <summary>
        /// <see cref="IEnDec{T}"/> for coding <see cref="NStandardAppraiser"/> instances.
        /// Type unsafe, only user with <see cref="NStandardAppraiser"/>s.
        /// </summary>
        public static readonly IEnDec<IAppraiser> ENDEC = RStandardAppraiser.ENDEC
            .Map<IAppraiser, RStandardAppraiser>(
                (x) => (RStandardAppraiser)(NStandardAppraiser)x,
                (x) => x);

        private readonly RStandardAppraiser _inner = new RStandardAppraiser();



        /// <inheritdoc/>
        public VHandAppraisal AppraiseHand(IHand hand, bool includeHidden)
            => _inner.AppraiseHand(hand, includeHidden);



        /// <summary>
        /// Unwrap and get inner <see cref="RStandardAppraiser"/>.
        /// </summary>
        /// <param name="operand"></param>
        public static implicit operator RStandardAppraiser(NStandardAppraiser operand) => operand._inner;
    } // end class
} // end namespace