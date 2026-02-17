using ChipMasters.Registers;
using ChipMasters.User;
using Fakes.ChipMasters.Users;


namespace Tests.ChipMasters.User
{
    [TestClass]
    public class TestRAchievementTracker
    {
        [TestMethod]

        [DataRow("bust", SStats.BUSTS, 0, false)]
        [DataRow("bust", SStats.BUSTS, 10, true)]
        public void UnlocksAchievement(string key, string statKey, int value, bool expectedUnlocked)
        {
            var ach = SAchievementTypes.REGISTER[key];
            var metrics = new FakeMetrics();
            var tracker = new RAchievementTracker(metrics);

            metrics.SetStat(statKey, value);

            Assert.AreEqual(expectedUnlocked, tracker.Achievements.Contains(ach));
        } // end UnlocksAchievement()


        [TestMethod]
        public void UnlocksMultipleAchievements()
        {
            var a1 = SAchievementTypes.REGISTER["blackjack_ace"];
            var a2 = SAchievementTypes.REGISTER["welcome_to_blackjack"];

            var metrics = new FakeMetrics();
            var tracker = new RAchievementTracker(metrics);

            metrics.SetStat(a1.WatchedStat, 100);

            Assert.AreEqual(2, tracker.Achievements.Count);
        } // end UnlocksMultipleAchievements()


        [TestMethod]
        public void UnlocksAchiever()
        {
            var metrics = new FakeMetrics();
            var tracker = new RAchievementTracker(metrics);

            foreach (var ach in SAchievementTypes.REGISTER.Values)
            {
                if (ach == SAchievementTypes.ACHIEVER)
                    continue;

                metrics.SetStat(ach.WatchedStat, ach.Threshold);
            }

            Assert.IsTrue(tracker.Achievements.Contains(SAchievementTypes.ACHIEVER));
        } // end UnlocksAchiever()


        [TestMethod]

        [DataRow("blackjack_ace", 49, 0)]
        [DataRow("blackjack_ace", 50, 1)]
        [DataRow("blackjack_ace", 60, 1)]
        [DataRow("dealers_demons", 100, 1)]
        [DataRow("dealers_demons", 99, 0)]
        public void AchievementUnlocks(string key, int statValue, int expected)
        {
            var ach = SAchievementTypes.REGISTER[key];
            var metrics = new FakeMetrics();
            var tracker = new RAchievementTracker(metrics);

            metrics.SetStat(ach.WatchedStat, statValue);

            Assert.AreEqual(expected, tracker.Achievements.Contains(ach) ? 1 : 0);
        } // end AchievementUnlocks()

    } // end class
} // end namespace
