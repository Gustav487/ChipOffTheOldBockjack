using ChipMasters.Games.Appraisers;
using ChipMasters.Games.Hands;

namespace ChipMasters.Menu.Displays.Hand.Appraisal
{
    /// <summary>
    /// Contract for an object which displays the appraisal of a blackjack <see cref="IHand"/>s value.
    /// </summary>
    public interface IHandAppraisalDisplay : IHandDisplay
    {
        /// <summary>
        /// <see cref="IAppraiser"/> to appraise hands by.
        /// </summary>
        IAppraiser? Appraiser { get; set; }
    } // end interface
} // end namespace