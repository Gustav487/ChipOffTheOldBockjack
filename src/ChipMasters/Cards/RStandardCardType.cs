using GSR.EnDecic;
using GSR.EnDecic.EnDecs;

namespace ChipMasters.Cards
{
    /// <summary>
    /// <see cref="ICardType"/> implementation for a <see cref="RCard"/>.
    /// </summary>
    public class RStandardCardType : ICardType
    {
        /// <inheritdoc/>
        public IEnDec<ICard> EnDec { get; }

        private readonly ECardRank _rank;
        private readonly ECardSuit _suit;



        /// <inheritdoc/>
        public RStandardCardType(ECardRank rank, ECardSuit suit)
        {
#warning validate enum validity. "(ECardRank)9000000" is valid syntax, but wouldn't make sense here
            _rank = rank;
            _suit = suit;

            EnDec = EnDecUtil.KeyedEnDecBuilder<string, ICard>(EnDecUtil.STRING)
                .Add("veiled", EnDecUtil.BOOLEAN, (x) => x.Veiled)
                .Build((v) => new RCard(rank, suit, veiled: v));
        } // end ctor



        /// <inheritdoc/>
        public ICard Instantiate() => new RCard(_rank, _suit);

        /// <inheritdoc/>
        public bool IsDefault(ICard t) => !t.Veiled;

        /// <inheritdoc/>
        public bool IsTypeOf(ICard card) =>
            card.GetType() == typeof(RCard)
            && card.Rank == _rank
            && card.Suit == _suit;
    } // end class
} // end namespace
