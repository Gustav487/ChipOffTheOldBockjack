using ChipMasters.GodotWrappers;

namespace ChipMasters.Menu.Displays.Chips
{
    /// <summary>
    /// <see cref="IChipDisplay"/> that displays chip count as is, without any formatting.
    /// </summary>
    public sealed class RChipDisplay : AChipDisplay
    {
        private readonly ILabel _label;



        /// <inheritdoc/>
        public RChipDisplay(ILabel label)
        {
            _label = label.AssertNotNull();
            RefreshDisplay();
        } // end ctor



        /// <inheritdoc/>
        protected override void RefreshDisplay()
        {
            _label.Text = Chips?.ToString() ?? "N/A";
            if (ExplicitSign && Chips > 0)
                _label.Text = $"+{_label.Text}";
        } // end RefreshDisplay()
    } // end class
} // end namespace