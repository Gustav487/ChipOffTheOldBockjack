using ChipMasters.GodotWrappers;

namespace ChipMasters.Menu.Displays.Chips
{
    /// <summary>
    /// <see cref="NLabel"/> <see cref="IChipDisplay"/> that wraps <see cref="RChipDisplay"/>.
    /// </summary>
    public partial class NChipDisplay : ANChipDisplay
    {
        /// <inheritdoc/>
        protected override IChipDisplay CreateInner() => new RChipDisplay(this);

    } // end class
} // end namespace
