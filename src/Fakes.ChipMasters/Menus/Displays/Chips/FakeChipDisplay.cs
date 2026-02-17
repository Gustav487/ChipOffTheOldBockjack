using ChipMasters.Menu.Displays.Chips;

namespace Fakes.ChipMasters.Menus.Displays.Chips
{
    public class FakeChipDisplay : IChipDisplay
    {
        public int? Chips { get; set; }
        public bool ExplicitSign { get; set; }
    } // end class
} // end namespace