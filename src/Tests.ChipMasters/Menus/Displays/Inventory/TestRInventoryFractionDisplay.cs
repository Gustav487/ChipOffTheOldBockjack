using ChipMasters.GodotWrappers;
using ChipMasters.Items;
using ChipMasters.Menu.Displays;
using ChipMasters.Menu.Displays.Inventory;
using Fakes.ChipMasters.GodotWrappers;
using Fakes.ChipMasters.Items;

namespace Tests.ChipMasters.Menus.Displays.Inventory
{
    [TestClass]
    public class TestRInventoryFractionDisplay
    {
        [TestMethod]
        [DataRow(null, RInventoryFractionDisplay.UNDEFINED)]
        [DataRow(11, "11 / 7")]
        [DataRow(0, "0 / 7")]
        [DataRow(3, "3 / 7")]
        [DataRow(7, "7 / 7")]
        public void Set(int? itmCount, string ex)
        {
            ILabel l = new FakeLabel();
            Assert.AreEqual("", l.Text);

            IDisplay<IInventory> d = new RInventoryFractionDisplay(l);
            Assert.AreEqual(RInventoryFractionDisplay.UNDEFINED, l.Text);

            if (itmCount is not null)
            {
                RInventory inv = new();
                for (int i = 0; i < itmCount; i++)
                    inv.Add(new FakeItem());
                d.Display = inv;
            }
            else
                d.Display = null;

            Assert.AreEqual(ex, l.Text);
        } // end Set()
    } // end class
} // end namespace