using ChipMasters.Cards;
using ChipMasters.Games.Hands;

namespace ChipMasters.Menu.Displays.Hand
{
    /// <summary>
    /// Base class of a display of some attribute of a <see cref="IHand"/>.
    /// </summary>
    public abstract class AHandDisplay : IHandDisplay
    {
        /// <inheritdoc/>
        public IHand? Hand
        {
            get => _hand;
            set
            {
                if (_hand == value)
                    return;

                if (_hand is not null)
                {
                    _hand.OnCardAdded -= RefreshDisplay;
                    HandRemoved(_hand);
                }

                _hand = value;

                if (_hand is not null)
                {
                    _hand.OnCardAdded += RefreshDisplay;
                    HandAdded(_hand);
                }
            }
        }
        private IHand? _hand;
        private bool _disposing = false;



        /// <inheritdoc/>
        public virtual void Dispose()
        {
            _disposing = true;
            if (_hand is null)
                return;

            _hand.OnCardAdded -= RefreshDisplay;
            HandRemoved(_hand);
        } // end Dispose()



        /// <summary>
        /// Update the display.
        /// </summary>
        /// <param name="addedCard">Card that's added.</param>
        protected virtual void RefreshDisplay(ICard addedCard) => RefreshDisplay();

        /// <summary>
        /// Update the display.
        /// </summary>
        protected abstract void RefreshDisplay();

        /// <summary>
        /// <paramref name="hand"/>'s not longer being display.
        /// 
        /// WARNING: Called during disposal.
        /// </summary>
        /// <param name="hand"></param>
        protected virtual void HandRemoved(IHand hand)
        {
            if (!_disposing)
                RefreshDisplay();
        } // end HandRemoved()

        /// <summary>
        /// <paramref name="hand"/> is now being display.
        /// </summary>
        /// <param name="hand"></param>
        protected virtual void HandAdded(IHand hand)
        {
            RefreshDisplay();
        } // end HandAdded()
    } // end class
} // end namespace