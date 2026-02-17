namespace ChipMasters.Menu.Displays.Chips
{
    /// <summary>
    /// Contract for an object that displays a chip count.
    /// </summary>
    public interface IChipDisplay
    {
        /// <summary>
        /// Number of chips to display. Null if display not set.
        /// </summary>
        int? Chips { get; set; }

        /// <summary>
        /// Should '+' be displayed before positive amounts.
        /// </summary>
        bool ExplicitSign { get; set; }
    } // end interface
} // end namespace