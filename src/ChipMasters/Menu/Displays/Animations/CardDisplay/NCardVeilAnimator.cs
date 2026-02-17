using ChipMasters.Cards;
using ChipMasters.GodotWrappers;
using ChipMasters.Menu.Displays.Animations.CardDisplay;

namespace ChipMasters.Menu.Displays.Animations.ControlCardDisplay
{
    /// <summary>
    /// <see cref="NNode"/> based <see cref="IAnimator{T}"/> wrapping <see cref="RCardFlipAnimator"/>.
    /// </summary>
    public sealed partial class NCardVeilAnimator : NNode, IAnimator<IDisplay<ICard>>
    {
        private readonly IAnimator<IDisplay<ICard>> _inner = new RCardVeilAnimator();

        /// <inheritdoc/>
        public IAnimation Animate(IDisplay<ICard> controlCardDisplay) => _inner.Animate(controlCardDisplay);
    } // end class
} // end namespace