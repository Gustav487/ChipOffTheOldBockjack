using ChipMasters.GodotWrappers;
using ChipMasters.Menu.Displays;
using ChipMasters.Menu.Displays.AchievementTrackers;
using ChipMasters.User;
using Fakes.ChipMasters.GodotWrappers;
using Fakes.ChipMasters.Users;

namespace Tests.ChipMasters.Menus.Displays.AchievementTrackers
{
    [TestClass]
    public class TestRAchievementsFractionDisplay
    {
        private const string ACHIEVMENT_COUNT = "28";
        [TestMethod]
        [DataRow(null, RAchievementsFractionDisplay.UNDEFINED)]
        [DataRow(30, "30 / " + ACHIEVMENT_COUNT)]
        [DataRow(0, "0 / " + ACHIEVMENT_COUNT)]
        [DataRow(3, "3 / " + ACHIEVMENT_COUNT)]
        [DataRow(7, "7 / " + ACHIEVMENT_COUNT)]
        public void Set(int? achCount, string ex)
        {
            ILabel l = new FakeLabel();
            Assert.AreEqual("", l.Text);

            IDisplay<IAchievementTracker> atd = new RAchievementsFractionDisplay(l);
            Assert.AreEqual(RAchievementsFractionDisplay.UNDEFINED, l.Text);

            if (achCount is not null)
            {
                FakeAchievementTracker fat = new();
                for (int i = 0; i < achCount; i++)
                    fat.Add(new FakeAchievement());
                atd.Display = fat;
            }
            else
                atd.Display = null;

            Assert.AreEqual(ex, l.Text);
        } // end Set();
    } // end class
} // end namespace