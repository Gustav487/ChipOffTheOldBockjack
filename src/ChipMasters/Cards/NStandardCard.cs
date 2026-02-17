using ChipMasters.GodotWrappers;
using ChipMasters.Registers;
using Godot;
using System;

namespace ChipMasters.Cards
{
    /// <summary>
    /// <see cref="Godot.Node"/> based <see cref="ICardType"/>.
    /// </summary>
    public partial class NStandardCard : NNode, ICard
    {
        [Export] private string? _type;




        /// <inheritdoc/>
        public ECardSuit Suit => Card.Suit;

        /// <inheritdoc/>
        public ECardRank Rank => Card.Rank;

        /// <inheritdoc/>
        public bool Veiled { get => Card.Veiled; set => Card.Veiled = value; }

        /// <inheritdoc/>
        public event Action? OnFlipped;



        private ICard Card => _card ?? throw new RNotReadyException();
        private ICard? _card;



        /// <inheritdoc/>
        public override void _Ready()
        {
            base._Ready();
            _card = SCardTypes.REGISTER[_type.AssertNotNull()].Instantiate();
            _card.OnFlipped += () => OnFlipped?.Invoke();
        } // end _Ready()



        /// <inheritdoc/>
        public override string ToString() => Card.ToString() ?? throw new ArgumentNullException();
    } // end class
} // end namespace
