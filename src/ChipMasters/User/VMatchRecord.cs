using ChipMasters.Games.Appraisers;
using ChipMasters.Games.Matches;
using GSR.EnDecic;
using GSR.EnDecic.EnDecs;
using System;

namespace ChipMasters.User
{
    /// <summary>
    /// Represents a record of a completed match for history tracking.
    /// </summary>
    public struct VMatchRecord
    {
        /// <summary>
        /// <see cref="IEnDec{T}"/> for coding <see cref="VMatchRecord"/> instances.
        /// </summary>
        public static readonly IEnDec<VMatchRecord> ENDEC = EnDecUtil.STRING.KeyedEnDecBuilder<string, VMatchRecord>()
            .Add("bet", EnDecUtil.INT_32, (x) => x.Bet)
            .Add("player_result", VHandAppraisal.ENDEC, (x) => x.PlayerResult)
            .Add("dealer_result", VHandAppraisal.ENDEC, (x) => x.DealerResult)
            .Build((b, pr, dr) => new VMatchRecord(b, pr, dr));



        /// <summary>
        /// Amount of chips that had been bet.
        /// </summary>
        public int Bet { get; } // The amount bet by the player
        /// <summary>
        /// Player final score.
        /// </summary>
        public VHandAppraisal PlayerResult { get; } // Player's final hand value
        /// <summary>
        /// Dealer final score
        /// </summary>
        public VHandAppraisal DealerResult { get; } // Dealer's final hand value



        /// <summary>
        /// Do not use.
        /// </summary>
        /// <exception cref="InvalidOperationException"></exception>
        public VMatchRecord()
        {
            throw new InvalidOperationException("Use parameterized constructor.");
        } // end ctor

        /// <summary>
        /// Store history of a match.
        /// </summary>
        /// <param name="match"></param>
        public VMatchRecord(IMatch match) : this(
            match.Bet,
            match.AppraisePlayerHand(false),
            match.AppraiseDealerHand(false))
        { } // end ctor

        /// <inheritdoc/>
        public VMatchRecord(int bet, VHandAppraisal playerResult, VHandAppraisal dealerResult)
        {
            Bet = bet;
            PlayerResult = playerResult.AssertKnown(); // assure state known, recording an uncompleted match doesn't make sense, and isn't representative.
            DealerResult = dealerResult.AssertKnown();
        } // end ctor
    } // end class
} // end namespace 
