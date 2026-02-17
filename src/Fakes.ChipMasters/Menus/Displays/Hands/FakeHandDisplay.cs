using ChipMasters.Games.Hands;
using ChipMasters.Menu.Displays.Hand;

namespace Fakes.ChipMasters.Menus.Displays.Hands
{
    public class FakeHandDisplay : IHandDisplay
    {
        public IHand? Hand { get; set; }



        public void Dispose()
        {
            throw new NotImplementedException();
        }
    } // end class
} // end namespace