using ChipMasters.Cards;
using ChipMasters.GodotWrappers;
using Godot;
using GSR.EnDecic;
using GSR.EnDecic.EnDecs;

namespace ChipMasters.Games.Matches.Providers
{
    /// <summary>
    /// <see cref="NNode"/> based <see cref="IMatchProvider"/> that wraps a <see cref="RStandardMatchProvider"/>.
    /// </summary>
    public sealed partial class NStandardMatchProvider : NNode, IMatchProvider
    {
        /// <summary>
        /// <see cref="IEnDec{T}"/> for coding <see cref="NStandardMatchProvider"/> instances.
        /// Type unsafe, only user with <see cref="NStandardMatchProvider"/>s.
        /// </summary>
        public static readonly IEnDec<IMatchProvider> ENDEC = RStandardMatchProvider.ENDEC
            .Map<IMatchProvider, RStandardMatchProvider>(
                (x) => (RStandardMatchProvider)(NStandardMatchProvider)x,
                (x) => x);

        [Export] private Node _playerDeck = null!;
        [Export] private Node _DealerDeck = null!;



        private RStandardMatchProvider Inner => _inner ?? throw new RNotReadyException();
        private RStandardMatchProvider? _inner;



        /// <inheritdoc/>
        public override void _Ready()
        {
            base._Ready();
            _inner = new RStandardMatchProvider((IDeck)_playerDeck.AssertNotNull(), (IDeck)_DealerDeck.AssertNotNull());
        } // end _Ready()



        /// <inheritdoc/>
        public IMatch Create(int bet) => Inner.Create(bet);

        /// <summary>
        /// Unwrap and get inner <see cref="RStandardMatchProvider"/>.
        /// </summary>
        /// <param name="operand"></param>
        public static implicit operator RStandardMatchProvider(NStandardMatchProvider operand) => operand.Inner;
    } // end class
} // end namespace
