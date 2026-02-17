using ChipMasters.Cards;

namespace ChipMasters.Menu.Displays.Animations.CardDisplay
{
    /// <summary>
    /// <see cref="IAnimator{T}"/> that set a card face down.
    /// </summary>
    public sealed class RCardVeilAnimator : IAnimator<IDisplay<ICard>>
    {
        /// <inheritdoc/>
        public IAnimation Animate(IDisplay<ICard> cardDisplay)
        {
            ICard c = cardDisplay.Display!.Clone();
            cardDisplay.Display = null;
            c.Veiled = true;
            cardDisplay.Display = c;
            return IAnimation.EMPTY;
        } // Animate()

    } // end class
} // end namespace