using ChipMasters.Menu.AchievementUnlocked;
using ChipMasters.Menu.Displays;
using ChipMasters.User;
using Fakes.ChipMasters.Menus.Displays;
using Fakes.ChipMasters.Users;

namespace Tests.ChipMasters.Menus.AchievementUnlockeds
{
    [TestClass]
    public class TestRAchievementUnlockedMenu
    {
        [TestMethod]
        public async Task ShowOneAchievement()
        {
            FakeAchievementTracker at = new();
            IDisplay<IAchievement> d = new FakeDisplay<IAchievement>();
            int shownCount = 0;
            int hiddenCount = 0;
            RAchievementUnlockedMenu m = new(at, d, 1, () => shownCount += 1, () => hiddenCount += 1);

            IAchievement a1 = new FakeAchievement();
            at.Add(a1);

            Assert.AreEqual(a1, d.Display);
            Assert.AreEqual(1, shownCount);
            Assert.AreEqual(0, hiddenCount);

            await Task.Delay(1000); // hopefully waiting an entire second vs .001 seconds async period lets state machine process by the next assert - technically it can do whatever so this test could "randomly" fail

            Assert.AreEqual(1, shownCount);
            Assert.AreEqual(1, hiddenCount);
        } // end ShowOneAchievement()

        [TestMethod]
        public async Task ShowTwoAchievements()
        {
            FakeAchievementTracker at = new();
            IDisplay<IAchievement> d = new FakeDisplay<IAchievement>();
            int shownCount = 0;
            int hiddenCount = 0;
            RAchievementUnlockedMenu m = new(at, d, 1, () => shownCount += 1, () => hiddenCount += 1);

            IAchievement a1 = new FakeAchievement();
            IAchievement a2 = new FakeAchievement();
            at.Add(a1);
            at.Add(a2);

            Assert.AreEqual(a1, d.Display);
            Assert.AreEqual(1, shownCount);
            Assert.AreEqual(0, hiddenCount);

            await Task.Delay(1000); // hopefully waiting an entire second vs .001 seconds async period lets state machine process by the next assert - technically it can do whatever so this test could "randomly" fail

            Assert.AreEqual(a2, d.Display);
            Assert.AreEqual(1, shownCount);
            Assert.AreEqual(1, hiddenCount); // can't accurately place the call mid interval, so assume it's already finished by waited before that

            /*            await Task.Delay(1000);

                        Assert.AreEqual(1, shownCount);
                        Assert.AreEqual(1, hiddenCount);*/
        } // end ShowTwoAchievements()

        [TestMethod]
        public void Dispose()
        {
            FakeAchievementTracker at = new();
            IDisplay<IAchievement> d = new FakeDisplay<IAchievement>();
            int shownCount = 0;
            int hiddenCount = 0;
            RAchievementUnlockedMenu m = new(at, d, 0, () => shownCount += 1, () => hiddenCount += 1);
            m.Dispose();

            at.Add(new FakeAchievement());

            Assert.AreEqual(null, d.Display);
            Assert.AreEqual(0, shownCount);
            Assert.AreEqual(0, hiddenCount);
        } // end Dispose()
    } // end class
} // end namespace