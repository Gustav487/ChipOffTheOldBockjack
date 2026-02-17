using ChipMasters.GodotWrappers;
using ChipMasters.User;

namespace ChipMasters.Menu.Displays.Achievements
{
    /// <summary>
    /// <see cref="IAchievement"/> <see cref="ALabelDisplay{T}"/> that displays an achievement's name and description as text.
    /// </summary>
    public sealed class RAchievementDisplay : ALabelDisplay<IAchievement>
    {
        /// <inheritdoc/>
        public RAchievementDisplay(ILabel label) : base(label)
        { } // end ctor

        /// <inheritdoc/>
        protected override string ToString(IAchievement displaying)
            => displaying.DisplayName;
    } // end class
} // end namespace