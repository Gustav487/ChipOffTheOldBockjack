using ChipMasters.Cards;
using ChipMasters.Games.Appraisers;
using GSR.EnDecic;
using GSR.EnDecic.EnDecs;

namespace ChipMasters.Games.Matches.Providers
{
    /// <summary>
    /// <see cref="IMatchProvider"/> for <see cref="RMatch"/>s.
    /// </summary>
    public class RTurnedMatchProvider : IMatchProvider
    {
        /// <summary>
        /// <see cref="IEnDec{T}"/> for coding <see cref="RStandardMatchProvider"/> instances.
        /// </summary>
        public static readonly IEnDec<RTurnedMatchProvider> ENDEC = EnDecUtil.STRING.KeyedEnDecBuilder<string, RTurnedMatchProvider>()
            .Add("appraiser", IAppraiser.ENDEC, (x) => x._appraiser)
            .Add("player_deck", IDeck.ENDEC, (x) => x._playerDeck)
            .Add("dealer_deck", IDeck.ENDEC.NullableOfR(), (x) => ReferenceEquals(x._playerDeck, x._dealerDeck) ? null : x._dealerDeck)
            .Add("dealer_goal", EnDecUtil.INT_32, (x) => x._dealerGoal)
            .Build((ap, pd, dd, dg) => new(ap, pd, dd ?? pd, dg));



        private readonly IAppraiser _appraiser;
        private readonly IDeck _playerDeck;
        private readonly IDeck _dealerDeck;
        private readonly int _dealerGoal;



        /// <inheritdoc/>
        public RTurnedMatchProvider(IAppraiser appraiser, IDeck Playerdeck, IDeck DealerDeck, int dealerGoal)
        {
            _appraiser = appraiser.AssertNotNull();
            _playerDeck = Playerdeck.AssertNotNull();
            _dealerDeck = DealerDeck.AssertNotNull();
            _dealerGoal = dealerGoal;
        } // end ctor



        /// <inheritdoc/>
        public IMatch Create(int bet)
        {
            _playerDeck.Restore();
            _playerDeck.Shuffle();
            _dealerDeck.Restore();
            _dealerDeck.Shuffle();
            return new RTurnedMatch(_playerDeck, _dealerDeck, bet, dealerGoal: _dealerGoal, appraiser: _appraiser);
        } // end Create()

    } // end class
} // end namespace