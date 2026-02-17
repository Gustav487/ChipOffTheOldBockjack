using ChipMasters.GodotWrappers;
using ChipMasters.Menu.Displays;
using ChipMasters.Menu.Displays.ChipRecords;
using ChipMasters.User;
using Fakes.ChipMasters.GodotWrappers;

namespace Tests.ChipMasters.Menus.Displays.ChipRecords
{
    [TestClass]
    public class TestRChipRecordDeltaDisplay
    {
        [TestMethod]
        [DataRow(RChipRecordDisplay.UNDEFINED,
            0L, 0, null, "1/1/1970 12:00:00 AM:   0",
            0L, 1, 92, "1/1/1970 12:00:00 AM:   1          (+92)")]
        [DataRow(RChipRecordDisplay.UNDEFINED,
            2L, 36, -911, "1/1/1970 12:00:02 AM:   36         (-911)",
            null, null, 92, RChipRecordDisplay.UNDEFINED)]
        [DataRow(RChipRecordDisplay.UNDEFINED,
            2032040L, 2100000000, -911, "1/24/1970 12:27:20 PM:  2100000000 (-911)",
            null, null, 92, RChipRecordDisplay.UNDEFINED)]
        [DataRow(RChipRecordDisplay.UNDEFINED,
            null, null, null, RChipRecordDisplay.UNDEFINED,
            null, null, null, RChipRecordDisplay.UNDEFINED)]
        public void Set(
            string exPre,
            long? dt1, int? cc1, int? del1, string exText1,
            long? dt2, int? cc2, int? del2, string exText2)
        {
            ILabel l = new FakeLabel();
            IDisplay<VChipRecord?> crd = new RChipRecordDisplay(l);

            Assert.AreEqual(exPre, l.Text);

            crd.Display = cc1 is null ? null : new VChipRecord(DateTimeOffset.FromUnixTimeSeconds(dt1!.Value).UtcDateTime, cc1.Value, del1);

            Assert.AreEqual(exText1, l.Text);

            crd.Display = cc2 is null ? null : new VChipRecord(DateTimeOffset.FromUnixTimeSeconds(dt2!.Value).UtcDateTime, cc2.Value, del2);

            Assert.AreEqual(exText2, l.Text);
        } // end Set()


    } // end class
} // end namespace