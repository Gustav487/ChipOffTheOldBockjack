using ChipMasters.GodotWrappers;
using Godot;
using GSR.EnDecic;

namespace ChipMasters.Cards
{
    /// <summary>
    /// Godot <see cref="Node"/> wrapper of <see cref="RStandardCardType"/>.
    /// </summary>
    public partial class NStandardCardType : NNode, ICardType
    {
        [Export] private ECardSuit _suit;
        [Export] private ECardRank _rank;




        /// <inheritdoc/>
        public IEnDec<ICard> EnDec => CardType.EnDec;



        private ICardType CardType => _cardType ?? throw new RNotReadyException();
        private ICardType? _cardType;



        /// <inheritdoc/>
        public override void _Ready()
        {
            base._Ready();
            _cardType = new RStandardCardType(_rank, _suit);
        } // end _Ready()



        /// <inheritdoc/>
        public ICard Instantiate() => CardType.Instantiate();

        /// <inheritdoc/>
        public bool IsDefault(ICard t) => CardType.IsDefault(t);

        /// <inheritdoc/>
        public bool IsTypeOf(ICard card) => CardType.IsTypeOf(card);



        /// <inheritdoc/>
        public override int GetHashCode() => CardType.GetHashCode();

        /// <inheritdoc/>
        public override bool Equals(object? obj) =>
            ReferenceEquals(this, obj)
            || CardType.Equals(obj);

        /// <inheritdoc/>
        public static bool operator ==(NStandardCardType a, ICardType b) => a.Equals(b);

        /// <inheritdoc/>
        public static bool operator !=(NStandardCardType a, ICardType b) => !(a == b);

    } // end class
} // end namespace
