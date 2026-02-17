using ChipMasters.Menu.Displays;
using ChipMasters.Menu.Displays.ChipRecords;
using ChipMasters.User;
using ChipMasters.Util;
using Fakes.ChipMasters.Menus.Displays;

namespace Tests.ChipMasters.Menus.Displays.ChipRecords
{
    [TestClass]
    public class TestRChipHistoryDisplay
    {
        [TestMethod]
        public void SetHistory()
        {
            IPool<IDisplay<VChipRecord?>> dPool
                = new RPool<IDisplay<VChipRecord?>>(() => new FakeDisplay<VChipRecord?>());
            IList<IDisplay<VChipRecord?>> ding = new List<IDisplay<VChipRecord?>>();
            IDisplay<IReadOnlyList<VChipRecord>> chd = new RChipHistoryDisplay(dPool, ding.Add, (x) => ding.Remove(x));

            Assert.AreEqual(0, ding.Count);

            List<VChipRecord> toSet = new()
            {
                new VChipRecord(DateTime.Now, 20, null),
                new VChipRecord(DateTime.Now, 0, -20),
                new VChipRecord(DateTime.Now, 411, null),
            };
            chd.Display = toSet;

            Assert.AreEqual(3, ding.Count);

            Assert.IsNotNull(ding[0].Display);
            Assert.AreEqual(20, ding[0].Display!.Value.Count);
            Assert.AreEqual(null, ding[0].Display!.Value.Delta);

            Assert.IsNotNull(ding[1].Display);
            Assert.AreEqual(0, ding[1].Display!.Value.Count);
            Assert.AreEqual(-20, ding[1].Display!.Value.Delta);

            Assert.IsNotNull(ding[2].Display);
            Assert.AreEqual(411, ding[2].Display!.Value.Count);
            Assert.AreEqual(null, ding[2].Display!.Value.Delta);



            chd.Display = new List<VChipRecord>();
            Assert.AreEqual(0, ding.Count);

            chd.Display = null;
            Assert.AreEqual(0, ding.Count);
        } // end SetHistory()
    } // end class
} // end namespace