using ChipMasters.Cards;
using ChipMasters.GodotWrappers;
using Godot;

namespace ChipMasters.Menu.Displays.Cards
{
    /// <summary>
    /// Godot <see cref="Node"/> <see cref="IControlCardDisplay"/> that wraps another Godot Node <see cref="ICard"/><see cref="IDisplay{T}"/>
    /// </summary>
    public partial class NWrappingCardDisplay : NControl, IControlCardDisplay
    {
        [Export] private Node? _cardDisplay;



        /// <inheritdoc/>
        public ICard? Display { get => Inner.Display; set => Inner.Display = value; }



        private IDisplay<ICard> Inner => _inner ?? throw new RNotReadyException();
        private IDisplay<ICard>? _inner;



        /// <inheritdoc/>
        public override void _Ready()
        {
            base._Ready();
            _inner = (IDisplay<ICard>)_cardDisplay.AssertNotNull();
        } // end _Ready()

    } // end class
} // end namespace
