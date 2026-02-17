using ChipMasters.Cards;

namespace ChipMasters.Menu.Displays.Animations.CardDisplay
{
    /// <summary>
    /// <see cref="IAnimator{T}"/> the triggers a flip over animation.
    /// </summary>
    public sealed class RCardFlipAnimator : IAnimator<IDisplay<ICard>>
    {
        /// <inheritdoc/>
        public IAnimation Animate(IDisplay<ICard> cardDisplay)
        {
            ICard c = cardDisplay.Display!; // should've always been set now
            cardDisplay.Display = null; // remove card briefly to allow force setting the display.
            c.Veiled = true;
            cardDisplay.Display = c; // set back with display set to veiled.
            c.Veiled = false; // restore state and trigger animation to play.
            return IAnimation.EMPTY;
        } // end Animate()
    } // end class
} // end namespace