using ChipMasters.GodotWrappers;
using ChipMasters.Registers;
using ChipMasters.User;

namespace ChipMasters.Menu.Displays.AchievementTrackers
{
    /// <summary>
    /// <see cref="IAchievementTracker"/> <see cref="IDisplay{T}"/> that displays the amount achieved out of the amount registered in <see cref="SAchievementTypes"/>.
    /// </summary>
    public sealed class RAchievementsFractionDisplay : ALabelDisplay<IAchievementTracker>
    {
        /// <inheritdoc/>
        public RAchievementsFractionDisplay(ILabel label) : base(label)
        { } // end ctor

        /// <inheritdoc/>
        protected override string ToString(IAchievementTracker displaying)
            => $"{displaying.Achievements.Count} / {SAchievementTypes.REGISTER.Count}";
    } // end class
} // end namespace