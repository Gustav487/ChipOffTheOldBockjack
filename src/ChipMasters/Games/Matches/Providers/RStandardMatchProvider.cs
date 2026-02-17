using ChipMasters.Cards;
using GSR.EnDecic;
using GSR.EnDecic.EnDecs;

namespace ChipMasters.Games.Matches.Providers
{
    /// <summary>
    /// <see cref="IMatchProvider"/> for <see cref="RMatch"/>s.
    /// </summary>
    public class RStandardMatchProvider : IMatchProvider
    {
        /// <summary>
        /// <see cref="IEnDec{T}"/> for coding <see cref="RStandardMatchProvider"/> instances.
        /// </summary>
        public static readonly IEnDec<RStandardMatchProvider> ENDEC = EnDecUtil.STRING.KeyedEnDecBuilder<string, RStandardMatchProvider>()
            .Add("player_deck", IDeck.ENDEC, (x) => x._playerDeck)
            .Add("dealer_deck", IDeck.ENDEC.NullableOfR(), (x) => ReferenceEquals(x._playerDeck, x._dealerDeck) ? null : x._dealerDeck)
            .Build((p, d) => new(p, d ?? p));



        private readonly IDeck _playerDeck;
        private readonly IDeck _dealerDeck;


        /// <inheritdoc/>
        public RStandardMatchProvider(IDeck deck1, IDeck deck2)
        {
            _playerDeck = deck1.AssertNotNull();
            _dealerDeck = deck2.AssertNotNull();
        } // end ctor



        /// <inheritdoc/>
        public IMatch Create(int bet)
        {
            _playerDeck.Restore();
            _playerDeck.Shuffle();
            _dealerDeck.Restore();
            _dealerDeck.Shuffle();
            return new RMatch(_playerDeck, _dealerDeck, bet);
        } // end Create(),

    } // end class
} // end namespace