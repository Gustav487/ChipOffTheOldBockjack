using ChipMasters.Cards;
using ChipMasters.Games.Appraisers;
using ChipMasters.GodotWrappers;
using Godot;
using GSR.EnDecic;
using GSR.EnDecic.EnDecs;

namespace ChipMasters.Games.Matches.Providers
{
    /// <summary>
    /// <see cref="NNode"/> based <see cref="IMatchProvider"/> that wraps a <see cref="RStandardMatchProvider"/>.
    /// </summary>
    public sealed partial class NTurnedMatchProvider : NNode, IMatchProvider
    {
        /// <summary>
        /// <see cref="IEnDec{T}"/> for coding <see cref="NTurnedMatchProvider"/> instances.
        /// Type unsafe, only user with <see cref="NTurnedMatchProvider"/>s.
        /// </summary>
        public static readonly IEnDec<IMatchProvider> ENDEC = RTurnedMatchProvider.ENDEC
            .Map<IMatchProvider, RTurnedMatchProvider>(
                (x) => (RTurnedMatchProvider)(NTurnedMatchProvider)x,
                (x) => x);

        [Export] private Node _playerDeck = null!;
        [Export] private Node _dealerDeck = null!;
        [Export] private Node _appraiser = null!;
        [Export] private int _dealerGoal = 17;



        private RTurnedMatchProvider Inner => _inner ?? throw new RNotReadyException();
        private RTurnedMatchProvider? _inner;



        /// <inheritdoc/>
        public override void _Ready()
        {
            base._Ready();
            _inner = new RTurnedMatchProvider(
                (IAppraiser)_appraiser.AssertNotNull(),
                (IDeck)_playerDeck.AssertNotNull(),
                (IDeck)_dealerDeck.AssertNotNull(),
                _dealerGoal);
        } // end _Ready()



        /// <inheritdoc/>
        public IMatch Create(int bet) => Inner.Create(bet);

        /// <summary>
        /// Unwrap and get inner <see cref="RTurnedMatchProvider"/>.
        /// </summary>
        /// <param name="operand"></param>
        public static implicit operator RTurnedMatchProvider(NTurnedMatchProvider operand) => operand.Inner;
    } // end class
} // end namespace
