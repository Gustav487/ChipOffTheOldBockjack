using ChipMasters.GodotWrappers;
using ChipMasters.Menu.Displays.Cards;
using Godot;
using Godot.Collections;
using System.Linq;

namespace ChipMasters.Menu.Displays.Animations.ControlCardDisplay
{
    /// <summary>
    /// <see cref="NNode"/> based <see cref="IAnimator{T}"/> that wraps a <see cref="RSequenceAnimator{T}"/>.
    /// </summary>
    public sealed partial class NSequentialControlCardAnimator : NNode, IAnimator<IControlCardDisplay>
    {
        [Export] private Array<Node> _animators = null!;



        private IAnimator<IControlCardDisplay> Inner => _inner ?? throw new RNotReadyException();
        private IAnimator<IControlCardDisplay>? _inner;



        /// <inheritdoc/>
        public override void _Ready()
        {
            base._Ready();

            _inner = new RSequenceAnimator<IControlCardDisplay>(_animators.AssertNotNull()
                .Cast<IAnimator<IControlCardDisplay>>());

        } // end _Ready()


        /// <inheritdoc/>
        public IAnimation Animate(IControlCardDisplay instance) => Inner.Animate(instance);
    } // end class
} // end namespace