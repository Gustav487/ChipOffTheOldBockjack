using ChipMasters.IO;
using ChipMasters.Registers;
using GSR.EnDecic;
using GSR.EnDecic.EnDecs;

namespace ChipMasters.User
{
    /// <summary>
    /// Contract for an object representing an achievement.
    /// </summary>
    public partial interface IAchievement
    {
        /// <summary>
        /// <see cref="IEnDec{T}"/> for coding <see cref="IAchievement"/>s by the <see cref="SAchievementTypes"/> registry.
        /// </summary>
        public static readonly IEnDec<IAchievement> ENDEC = SAchievementTypes.REGISTER.RegistryEnDec(EnDecUtil.STRING);

        /// <summary>
        /// Gets the display name of the achievement.
        /// </summary>
        string DisplayName { get; }

        /// <summary>
        /// Gets the stat key that this achievement watches.
        /// </summary>
        string WatchedStat { get; }

        /// <summary>
        /// Gets the threshold required to unlock this achievement.
        /// </summary>
        int Threshold { get; }
    } // end class
} // end namespace
