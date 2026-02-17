using ChipMasters.GodotWrappers;
using Godot;
using System.Collections.Generic;

namespace ChipMasters.Menu.Displays.Arrangers
{
    /// <summary>
    /// <see cref="IArranger{T}"/> for setting a series of <see cref="IControl"/>s anchors such that they lay within a rectangular bound as cards would on a table.
    /// </summary>
    public sealed partial class NHandControlArranger : NNode, IArranger<IControl>
    {
        [Export] private Vector2 _displayOffset;
        [Export] private float _displayWidth;
        [Export] private Vector2 _cardSize;
        [Export] private float _maximalSpacing;



        private IArranger<IControl> Inner => _inner ?? throw new RNotReadyException();
        private IArranger<IControl>? _inner;



        /// <inheritdoc/>
        public override void _Ready()
        {
            base._Ready();
            _inner = new RHandControlArranger(_displayOffset.To(), _displayWidth, _cardSize.To(), _maximalSpacing);
        } // end _Ready()



        /// <inheritdoc/>
        public void Arrange(IReadOnlyList<IControl> elements) => Inner.Arrange(elements);
    } // end class
} // end namespace