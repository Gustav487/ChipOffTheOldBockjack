using ChipMasters.GodotWrappers;
using ChipMasters.User;

namespace ChipMasters.Menu.Displays.Achievements
{
    /// <summary>
    /// <see cref="ILabel"/> <see cref="ANLabelDisplay{T}"/> that wraps a <see cref="RAchievementDisplay"/>.
    /// </summary>
    public sealed partial class NAchievementDisplay : ANLabelDisplay<IAchievement>
    {
        /// <inheritdoc/>
        protected override IDisplay<IAchievement> ConstructInner()
            => new RAchievementDisplay((ILabel)_label.AssertNotNull());
    } // end class
} // end namespace