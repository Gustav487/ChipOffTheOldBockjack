using ChipMasters.Games.Appraisers;

namespace ChipMasters.Games.Matches
{
    /// <summary>
    /// Assorted extension methods for <see cref="IMatch"/>.
    /// </summary>
    public static class SIMatchExtensions
    {
        /// <summary>
        /// Get player hand appraisal for the match.
        /// </summary>
        /// <param name="match"></param>
        /// <param name="includeHidden"></param>
        /// <returns></returns>
        public static VHandAppraisal AppraisePlayerHand(this IMatch match, bool includeHidden)
            => match.Appraiser.AppraiseHand(match.PlayerHand, includeHidden);

        /// <summary>
        /// Get dealer hand appraisal for the match.
        /// </summary>
        /// <param name="match"></param>
        /// <param name="includeHidden"></param>
        /// <returns></returns>
        public static VHandAppraisal AppraiseDealerHand(this IMatch match, bool includeHidden)
            => match.Appraiser.AppraiseHand(match.DealerHand, includeHidden);
    } // end class
} // end namespace