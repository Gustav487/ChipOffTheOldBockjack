using ChipMasters.GodotWrappers;
using Godot;
using Godot.Collections;
using System.Collections.Immutable;
using System.Linq;

namespace ChipMasters.Menu.Displays
{
    /// <summary>
    /// <see cref="NNode"/> base <see cref="IDisplay{T}"/> that displays to several sub displays of the same type, abstract dut to godot constraints on generics.
    /// </summary>
    public abstract partial class ANAggregateDisplay<T> : NNode, IDisplay<T>
    {
        [Export] private Array<Node> _subDisplays = null!;



        /// <inheritdoc/>
        public T? Display
        {
            get => _displaying;
            set
            {
                _displaying = value;
                foreach (IDisplay<T> i in Inners)
                    i.Display = value;
            }
        } // end Chips
        private T? _displaying;

        private IImmutableList<IDisplay<T>> Inners => _inners ?? throw new RNotReadyException(nameof(Inners));
        private IImmutableList<IDisplay<T>>? _inners;



        /// <inheritdoc/>
        public override void _Ready()
        {
            base._Ready();
            _inners = _subDisplays.AssertNotNull().Cast<IDisplay<T>>().ToImmutableList();
        } // end _Ready()
    } // end class
} // end namespace