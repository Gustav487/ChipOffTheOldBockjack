using ChipMasters.GodotWrappers;
using ChipMasters.User;

namespace ChipMasters.Menu.Displays.AchievementTrackers
{
    /// <summary>
    /// <see cref="IAchievementTracker"/> <see cref="ANLabelDisplay{T}"/> wrapper of <see cref="RAchievementsFractionDisplay"/>.
    /// </summary>
    public sealed partial class NAchievementsFractionDisplay : ANLabelDisplay<IAchievementTracker>
    {
        /// <inheritdoc/>
        protected override IDisplay<IAchievementTracker> ConstructInner()
            => new RAchievementsFractionDisplay((ILabel)_label.AssertNotNull());
    } // end class
} // end namespace