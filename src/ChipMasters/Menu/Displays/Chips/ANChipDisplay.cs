using ChipMasters.GodotWrappers;
using Godot;

namespace ChipMasters.Menu.Displays.Chips
{
    /// <summary>
    /// <see cref="NLabel"/> <see cref="IChipDisplay"/> that wraps an <see cref="IChipDisplay"/>.
    /// </summary>
    public abstract partial class ANChipDisplay : NLabel, IChipDisplay
    {
        [Export] private bool _explicitSign = false;



        /// <inheritdoc/>
        public int? Chips { get => _inner.Chips; set => _inner.Chips = value; }

        /// <inheritdoc/>
        public bool ExplicitSign { get => _inner.ExplicitSign; set => _inner.ExplicitSign = value; }

        /// <summary>
        /// <see cref="IChipDisplay"/> to wrap.
        /// </summary>
        protected readonly IChipDisplay _inner;



        /// <inheritdoc/>
        public ANChipDisplay()
        {
            _inner = CreateInner().AssertNotNull();
        } // end ctor

        /// <summary>
        /// Create the <see cref="IChipDisplay"/> to wrap.
        /// </summary>
        /// <returns></returns>
        protected abstract IChipDisplay CreateInner();



        /// <inheritdoc/>
        public override void _Ready()
        {
            base._Ready();
            ExplicitSign = _explicitSign;
        } // end _Ready()
    } // end class
} // end namespace