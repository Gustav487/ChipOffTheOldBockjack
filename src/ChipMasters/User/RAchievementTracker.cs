using ChipMasters.Registers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ChipMasters.User
{
    /// <summary>
    /// Simple <see cref="IAchievementTracker"/> implementation.
    /// </summary>
    public class RAchievementTracker : IAchievementTracker
    {
        /// <inheritdoc/>
        public IReadOnlySet<IAchievement> Achievements => _achievements;
        private readonly HashSet<IAchievement> _achievements = new();

        /// <inheritdoc/>
        public event Action<IAchievement>? OnUnlocked;

        private readonly IMetrics _metrics;



        /// <inheritdoc/>
        public RAchievementTracker(IMetrics metrics)
        : this(Array.Empty<IAchievement>(), metrics)
        { }

        /// <inheritdoc/>
        public RAchievementTracker(IEnumerable<IAchievement> achievements, IMetrics metrics)
        {
            _achievements = achievements.AssertNotNull().Select((x) => x.AssertNotNull()).ToHashSet();
            _metrics = metrics.AssertNotNull();
            _metrics.OnStatChanged += CheckForNewAchievements;
        } // end ctor



        /// <inheritdoc/>
        public void CheckForNewAchievements()
        {
            foreach (IAchievement achievement in SAchievementTypes.REGISTER.Values)
            {
                if (_achievements.Contains(achievement))
                    continue;

                int statValue = _metrics.GetStat(achievement.WatchedStat);
                if (statValue >= achievement.Threshold)
                {
                    _achievements.Add(achievement);
                    OnUnlocked?.Invoke(achievement);
                }
            }
            if (Achievements.Count == (SAchievementTypes.REGISTER.Count - 1))
            {
                IAchievement a = SAchievementTypes.ACHIEVER;
                _achievements.Add(a);
                OnUnlocked?.Invoke(a);
            }
        } // end CheckForNewAchievements()

    } // end class
} // end namespace
