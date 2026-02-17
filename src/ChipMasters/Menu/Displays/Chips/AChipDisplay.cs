namespace ChipMasters.Menu.Displays.Chips
{
    /// <summary>
    /// <see cref="IChipDisplay"/> base class that handles supporting logic. Base classes only must implement display logic through the RefreshDisplay() method.
    /// </summary>
    public abstract class AChipDisplay : IChipDisplay
    {
        /// <inheritdoc/>
        public int? Chips
        {
            get => _chips;
            set
            {
                if (value == _chips)
                    return;

                _chips = value;
                RefreshDisplay();
            }
        } // end Chips
        private int? _chips;

        /// <inheritdoc/>
        public bool ExplicitSign
        {
            get => _explicitSign;
            set
            {
                if (value == _explicitSign)
                    return;

                _explicitSign = value;
                RefreshDisplay();
            }
        }
        private bool _explicitSign;


        /// <summary>
        /// Redraw any displays, called when created and when <see cref="Chips"/> changed.
        /// </summary>
        protected abstract void RefreshDisplay();
    } // end class
} // end namespace