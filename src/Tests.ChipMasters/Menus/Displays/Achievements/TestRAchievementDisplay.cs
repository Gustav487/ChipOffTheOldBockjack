using ChipMasters.GodotWrappers;
using ChipMasters.Menu.Displays;
using ChipMasters.Menu.Displays.Achievements;
using ChipMasters.User;
using Fakes.ChipMasters.GodotWrappers;
using Fakes.ChipMasters.Users;

namespace Tests.ChipMasters.Menus.Displays.Achievements
{
    [TestClass]
    public class TestRAchievementDisplay
    {
        [TestMethod]
        [DataRow("", "")]
        [DataRow("Name", "Name")]
        [DataRow(null, RAchievementDisplay.UNDEFINED)]
        public void Set(string? setToDName, string ex)
        {
            ILabel l = new FakeLabel();
            Assert.AreEqual("", l.Text);

            IDisplay<IAchievement> d = new RAchievementDisplay(l);
            Assert.AreEqual(RAchievementDisplay.UNDEFINED, l.Text);

            d.Display = setToDName is null ? null : new FakeAchievement(displayName: setToDName);
            Assert.AreEqual(ex, l.Text);
        } // end Set()

    } // end class
} // end namespace