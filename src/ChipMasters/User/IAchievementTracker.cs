using System;
using System.Collections.Generic;

namespace ChipMasters.User
{
    /// <summary>
    /// Tracks unlocked achievements for a player.
    /// </summary>
    public interface IAchievementTracker
    {
        /// <summary>
        /// List of unlocked achievements.
        /// </summary>
        IReadOnlySet<IAchievement> Achievements { get; }

        /// <summary>
        /// Achievement fired when an achievement is unlocked.
        /// </summary>
        event Action<IAchievement>? OnUnlocked;

    } // end class
} // end namespace