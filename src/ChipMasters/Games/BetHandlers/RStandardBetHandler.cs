using ChipMasters.Games.Appraisers;
using ChipMasters.Games.Matches;
using ChipMasters.User;
using GSR.EnDecic;
using GSR.EnDecic.EnDecs;

namespace ChipMasters.Games.BetHandlers
{
    /// <summary>
    /// <see cref="IBetHandler"/> for standard blackjack bets.
    /// </summary>
    public sealed class RStandardBetHandler : IBetHandler
    {
        /// <summary>
        /// <see cref="IEnDec{T}"/> for coding <see cref="RStandardBetHandler"/> instances.
        /// </summary>
        public static readonly IEnDec<RStandardBetHandler> ENDEC = EnDecUtil.STRING.KeyedEnDecBuilder<string, RStandardBetHandler>()
            .Add("pay_to", IWallet.ENDEC, (x) => x._payTo)
            .Add("pay_from", IWallet.ENDEC, (x) => x._payFrom)
            .Build((t, f) => new(t, f));

        private readonly IWallet _payTo;
        private readonly IWallet _payFrom;



        /// <inheritdoc/>
        public RStandardBetHandler(IWallet payTo, IWallet payFrom)
        {
            _payTo = payTo.AssertNotNull();
            _payFrom = payFrom.AssertNotNull();
        } // end RStandardAppraiser()



        /// <inheritdoc/>
        public void Payout(IMatch match)
        {
            int amount = DueAmount(match);
            _payTo.Chips += amount;
            _payFrom.Chips -= amount;
        } // end Payout()

        private static int DueAmount(IMatch match)
        {
            VHandAppraisal player = match.AppraisePlayerHand(false).AssertKnown();
            VHandAppraisal dealer = match.AppraiseDealerHand(false).AssertKnown();

            float betScale;
            if (dealer == player) // tie
                betScale = 0f;
            else if (player.State == EHandState.Blackjack) // player blackjack
                betScale = 2f;
            else
                betScale = player > dealer ? 1f : -1f;

            return (int)(match.Bet * betScale);
        } // end PayAmount()
    } // end class
} // end namespace