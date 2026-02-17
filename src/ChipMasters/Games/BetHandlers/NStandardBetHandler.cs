using ChipMasters.Games.Matches;
using ChipMasters.GodotWrappers;
using ChipMasters.User;
using GSR.EnDecic;
using GSR.EnDecic.EnDecs;

namespace ChipMasters.Games.BetHandlers
{
    /// <summary>
    /// <see cref="NNode"/> based <see cref="IBetHandler"/> that wraps a <see cref="RStandardBetHandler"/>.
    /// </summary>
    public sealed partial class NStandardBetHandler : NNode, IBetHandler
    {
        /// <summary>
        /// <see cref="IEnDec{T}"/> for coding <see cref="NStandardBetHandler"/> instances.
        /// Type unsafe, only user with <see cref="NStandardBetHandler"/>s.
        /// </summary>
        public static readonly IEnDec<IBetHandler> ENDEC = RStandardBetHandler.ENDEC
            .Map<IBetHandler, RStandardBetHandler>(
                (x) => (RStandardBetHandler)(NStandardBetHandler)x,
                (x) => x);

        private readonly RStandardBetHandler _inner = new RStandardBetHandler(RUser.INSTANCE.Wallet, new RInfiniteWallet());



        /// <inheritdoc/>
        public void Payout(IMatch match) => _inner.Payout(match);

        /// <summary>
        /// Unwrap and get inner <see cref="RStandardBetHandler"/>.
        /// </summary>
        /// <param name="operand"></param>
        public static implicit operator RStandardBetHandler(NStandardBetHandler operand) => operand._inner;
    } // end class
} // end namespace