using ChipMasters.GodotWrappers;
using Godot;

namespace ChipMasters.Menu.Displays.Animations.Controls
{
    /// <summary>
    /// <see cref="NNode"/> based <see cref="IAnimator{T}"/> wrapping <see cref="RVisibilityAnimator"/>.
    /// </summary>
    public sealed partial class NVisibilityAnimator : NNode, IAnimator<IControl>
    {
        [Export] private bool _setTo;



        private IAnimator<IControl> Inner => _inner ?? throw new RNotReadyException();
        private IAnimator<IControl>? _inner;



        /// <inheritdoc/>
        public override void _Ready()
        {
            base._Ready();

            _inner = new RVisibilityAnimator(_setTo);
        } // end _Ready()



        /// <inheritdoc/>
        public IAnimation Animate(IControl instance) => Inner.Animate(instance);
    } // end class
} // end namespace
