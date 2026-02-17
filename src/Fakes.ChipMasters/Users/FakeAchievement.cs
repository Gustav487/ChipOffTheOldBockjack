using ChipMasters.User;

namespace Fakes.ChipMasters.Users
{
    public sealed class FakeAchievement : IAchievement
    {
        public string DisplayName { get; }
        public string WatchedStat { get; }
        public int Threshold { get; }

        public FakeAchievement() { }

        public FakeAchievement(string displayName, string watchedStat, int threshold)
        {
            DisplayName = displayName;
            WatchedStat = watchedStat;
            Threshold = threshold;
        }



        public FakeAchievement(string displayName = "")
        {
            DisplayName = displayName;
        } // end ctor

    } // end class
} // end namespace