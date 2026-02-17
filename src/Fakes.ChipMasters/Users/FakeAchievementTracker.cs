using ChipMasters.User;

namespace Fakes.ChipMasters.Users
{
    public class FakeAchievementTracker : IAchievementTracker
    {
        public IReadOnlySet<IAchievement> Achievements => _achievments;
        private readonly HashSet<IAchievement> _achievments = new();

        public event Action<IAchievement>? OnUnlocked;

        public void Add(IAchievement achievement)
        {
            _achievments.Add(achievement);
            OnUnlocked?.Invoke(achievement);
        } // end Add()
    } // end class
} // end namespace