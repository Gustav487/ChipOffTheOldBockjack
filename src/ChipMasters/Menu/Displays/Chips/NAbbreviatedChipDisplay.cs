using ChipMasters.GodotWrappers;

namespace ChipMasters.Menu.Displays.Chips
{
    /// <summary>
    /// <see cref="NLabel"/> <see cref="IChipDisplay"/> that wraps <see cref="RAbbreviatedChipDisplay"/>.
    /// </summary>
    public partial class NAbbreviatedChipDisplay : ANChipDisplay
    {
        /// <inheritdoc/>
        protected override IChipDisplay CreateInner() => new RAbbreviatedChipDisplay(this);
    } // end class
} // end namespace
