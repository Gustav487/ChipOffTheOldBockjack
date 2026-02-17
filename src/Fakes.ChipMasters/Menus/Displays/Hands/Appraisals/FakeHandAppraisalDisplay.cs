using ChipMasters.Games.Appraisers;
using ChipMasters.Games.Hands;
using ChipMasters.Menu.Displays.Hand.Appraisal;

namespace Fakes.ChipMasters.Menus.Displays.Hands.Appraisals
{
    public sealed class FakeHandAppraisalDisplay : IHandAppraisalDisplay
    {
        public IAppraiser? Appraiser { get; set; }
        public IHand? Hand { get; set; }



        public void Dispose()
        {
            throw new NotImplementedException();
        }
    } // end class
} // end namespace