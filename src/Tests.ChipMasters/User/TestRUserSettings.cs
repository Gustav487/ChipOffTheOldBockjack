using ChipMasters.User;

namespace Tests.ChipMasters.User
{
    public class TestRUserSettings
    {
        [TestClass]
        public class Valid
        {
            [TestMethod]
            public void DefaultConstructor()
            {
                IUserSettings us = new RUserSettings();
                Assert.IsNotNull(us.VolumeLevels);
                Assert.AreEqual(0, us.VolumeLevels.Count);
            } // end DefaultConstructor()

            [TestMethod]
            public void ConstructorWithVolumeLevels()
            {
                IUserSettings us = new RUserSettings(new Dictionary<string, float> { { "test", 100 } });
                Assert.AreEqual(1, us.VolumeLevels.Count);
                Assert.AreEqual(100, us.VolumeLevels["test"]);
            } // end ConstructorWithVolumeLevels()
        } // end inner class Valid
    } // end class
} // end namespace
