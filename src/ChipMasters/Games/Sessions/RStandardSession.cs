using ChipMasters.Games.BetHandlers;
using ChipMasters.Games.Matches;
using ChipMasters.Games.Matches.Providers;
using ChipMasters.User;
using GSR.EnDecic;
using GSR.EnDecic.EnDecs;
using System;

namespace ChipMasters.Games.Sessions
{
    /// <summary>
    /// Simple <see cref="ISession"/> implementation.
    /// </summary>
    public sealed class RStandardSession : ISession
    {
        /// <summary>
        /// <see cref="IEnDec{T}"/> for codes <see cref="RStandardSession"/> instances.
        /// </summary>
        public static readonly IEnDec<RStandardSession> ENDEC = EnDecUtil.STRING.KeyedEnDecBuilder<string, RStandardSession>()
            .Add("match", IMatch.ENDEC, (x) => x.Match)
            .Add("match_provider", IMatchProvider.ENDEC, (x) => x._matchProvider)
            .Add("bet_handler", IBetHandler.ENDEC, (x) => x._betHandler)
            .Add("metrics", IMetrics.REGISTRY_ENDEC, (x) => x._metrics)
            .Build((m, mp, bh, met) => new(m, mp, bh, met));



        /// <inheritdoc/>
        public IMatch Match { get; private set; }

        /// <inheritdoc/>
        public event Action? OnMatchChanged;


        private readonly IMatchProvider _matchProvider;
        private readonly IBetHandler _betHandler;
        private readonly IMetrics _metrics;



        /// <inheritdoc/>
        public RStandardSession(int bet, IMatchProvider matchProvider, IBetHandler betHandler, IMetrics metrics)
            : this(matchProvider.Create(bet), matchProvider, betHandler, metrics) { }

        /// <inheritdoc/>
        public RStandardSession(IMatch match, IMatchProvider matchProvider, IBetHandler betHandler, IMetrics metrics)
        {
            Match = match.AssertNotNull();
            _matchProvider = matchProvider.AssertNotNull();
            _betHandler = betHandler.AssertNotNull();
            _metrics = metrics.AssertNotNull();

            AttachMatch();
        } // end ctor



        /// <inheritdoc/>
        public void PlayAgain(int bet)
        {
            DetachMatch(); // current match is no longer sessions concern
            Match = _matchProvider.Create(bet);
            AttachMatch(); // make sure session concerns itself on the new match

            OnMatchChanged?.Invoke();
        } // end PlayAgain()

        private void AttachMatch()
        {
            if (Match.IsConcluded)
                MatchConcluded();
            else
                Match.OnConcluded += MatchConcluded;
        } // end AttachMatch()

        private void DetachMatch() => Match.OnConcluded -= MatchConcluded;

        private void MatchConcluded()
        {
            _betHandler.Payout(Match);
            _metrics.RecordMatch(Match);
        } // end MatchConcluded()
    } // end class
} // end namespace