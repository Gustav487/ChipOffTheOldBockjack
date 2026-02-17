using ChipMasters.GodotWrappers;
using Godot;

namespace ChipMasters.Menu.Displays.Chips
{
    /// <summary>
    /// <see cref="NLabel"/> <see cref="IChipDisplay"/> that wraps <see cref="RAbbreviatedChipDisplay"/>.
    /// </summary>
    public partial class NWrappingChipDisplay : NNode, IChipDisplay
    {
        [Export] private Node _chipDisplay = null!;

        /// <inheritdoc/>
        public int? Chips { get => Inner.Chips; set => Inner.Chips = value; }
        /// <inheritdoc/>
        public bool ExplicitSign { get => Inner.ExplicitSign; set => Inner.ExplicitSign = value; }

        private IChipDisplay Inner => _inner ?? throw new RNotReadyException();
        private IChipDisplay? _inner;



        /// <inheritdoc/>
        public override void _Ready()
        {
            base._Ready();
            _inner = (IChipDisplay)_chipDisplay.AssertNotNull();
        } // end _Ready()

    } // end class
} // end namespace
