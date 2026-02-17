using ChipMasters.GodotWrappers;
using ChipMasters.User;
using System.Collections.Generic;

namespace ChipMasters.Menu.Displays.MatchRecords
{
    /// <summary>
    /// <see cref="IReadOnlyList{T}"/> of <see cref="VMatchRecord"/>s <see cref="ANDisplay{T}"/> that wraps a <see cref="RMatchHistoryDisplay"/>.
    /// </summary>
    public sealed partial class NMatchHistoryDisplay : ANLabelDisplay<IReadOnlyList<VMatchRecord>>
    {
        /// <inheritdoc/>
        protected override IDisplay<IReadOnlyList<VMatchRecord>> ConstructInner()
            => new RMatchHistoryDisplay(
                (ILabel)_label.AssertNotNull());
    } // end class
} // end namespace
