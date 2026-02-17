using ChipMasters.Cards;
using ChipMasters.GodotWrappers;
using Godot;
using Godot.Collections;
using System.Linq;

namespace ChipMasters.Menu.Displays.Animations.ControlCardDisplay
{
    /// <summary>
    /// <see cref="NNode"/> based <see cref="IAnimator{T}"/> that wraps a <see cref="RSequenceAnimator{T}"/>.
    /// </summary>
    public sealed partial class NSequentialCardAnimator : NNode, IAnimator<IDisplay<ICard>>
    {
        [Export] private Array<Node> _animators = null!;



        private IAnimator<IDisplay<ICard>> Inner => _inner ?? throw new RNotReadyException();
        private IAnimator<IDisplay<ICard>>? _inner;



        /// <inheritdoc/>
        public override void _Ready()
        {
            base._Ready();

            _inner = new RSequenceAnimator<IDisplay<ICard>>(_animators.AssertNotNull()
                .Cast<IAnimator<IDisplay<ICard>>>());

        } // end _Ready()



        /// <inheritdoc/>
        public IAnimation Animate(IDisplay<ICard> instance) => Inner.Animate(instance);
    } // end class
} // end namespace
