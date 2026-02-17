using ChipMasters.Games.Hands;
using ChipMasters.GodotWrappers;
using Godot;
using GSR.EnDecic;
using GSR.EnDecic.EnDecs;

namespace ChipMasters.Games.Appraisers
{

    /// <summary>
    /// <see cref="NNode"/> based <see cref="IAppraiser"/> that wraps a <see cref="NTotalValueAppraiser"/>.
    /// </summary>
    public sealed partial class NTotalValueAppraiser : NNode, IAppraiser
    {
        /// <summary>
        /// <see cref="IEnDec{T}"/> for coding <see cref="NTotalValueAppraiser"/> instances.
        /// Type unsafe, only user with <see cref="NTotalValueAppraiser"/>s.
        /// </summary>
        public static readonly IEnDec<IAppraiser> ENDEC = RTotalValueAppraiser.ENDEC
            .Map<IAppraiser, RTotalValueAppraiser>(
                (x) => (RTotalValueAppraiser)(NTotalValueAppraiser)x,
                (x) => x);

        [Export] private int _maxValue;



        private RTotalValueAppraiser Inner => _inner ?? throw new RNotReadyException();
        private RTotalValueAppraiser? _inner;



        /// <inheritdoc/>
        public override void _Ready()
        {
            base._Ready();
            _inner = new RTotalValueAppraiser(_maxValue);
        } // end _Ready()



        /// <inheritdoc/>
        public VHandAppraisal AppraiseHand(IHand hand, bool includeHidden)
            => Inner.AppraiseHand(hand, includeHidden);

        /// <summary>
        /// Unwrap and get inner <see cref="RTotalValueAppraiser"/>.
        /// </summary>
        /// <param name="operand"></param>
        public static implicit operator RTotalValueAppraiser(NTotalValueAppraiser operand) => operand.Inner;
    } // end class
} // end namespace
