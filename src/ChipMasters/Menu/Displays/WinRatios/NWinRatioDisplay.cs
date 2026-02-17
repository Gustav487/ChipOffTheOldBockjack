using ChipMasters.GodotWrappers;
using ChipMasters.User;

namespace ChipMasters.Menu.Displays.WinRatios
{
    /// <summary>
    /// <see cref="NLabel"/> for displaying the current app user's win:tie:loss ratio
    /// </summary>
    public partial class NWinRatioDisplay : ANLabelDisplay<VWinRatio?>
    {
        /// <inheritdoc/>
        protected override IDisplay<VWinRatio?> ConstructInner()
            => new RWinRatioDisplay((ILabel)_label.AssertNotNull());
    } // end class
} // end namespace