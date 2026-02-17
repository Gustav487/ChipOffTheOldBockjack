using ChipMasters.Cards;
using ChipMasters.GodotWrappers;
using ChipMasters.Menu.Displays.Animations.CardDisplay;
using Godot;

namespace ChipMasters.Menu.Displays.Animations.ControlCardDisplay
{
    /// <summary>
    /// <see cref="NNode"/> based <see cref="IAnimator{T}"/> wrapping <see cref="RCardFlipAnimator"/>.
    /// </summary>
    public sealed partial class NIfVeiledAnimator : NNode, IAnimator<IDisplay<ICard>>
    {
        [Export] private Node _animator = null!;
        [Export] private bool _negate;



        private IAnimator<IDisplay<ICard>> Inner => _inner ?? throw new RNotReadyException();
        private IAnimator<IDisplay<ICard>>? _inner;



        /// <inheritdoc/>
        public override void _Ready()
        {
            base._Ready();

            _inner = new RIfVeiledAnimator(
                (IAnimator<IDisplay<ICard>>)_animator.AssertNotNull(),
                _negate);
        } // end _Ready()



        /// <inheritdoc/>
        public IAnimation Animate(IDisplay<ICard> instance) => Inner.Animate(instance);
    } // end class
} // end namespace
