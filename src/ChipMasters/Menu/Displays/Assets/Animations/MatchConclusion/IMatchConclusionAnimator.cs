using ChipMasters.Games.Matches;

namespace ChipMasters.Menu.Displays.Assets.Animations.MatchConclusion
{
    /// <summary>
    /// Contract for an object that runs <see cref="IMatch"/> conclusion animations.
    /// </summary>
    public interface IMatchConclusionAnimator : IAnimator
    {
        /// <summary>
        /// Play the match won animation.
        /// </summary>
        void PlayWin();

        /// <summary>
        /// Play the match tied animation.
        /// </summary>
        void PlayTie();

        /// <summary>
        /// Play the match lossed animation.
        /// </summary>
        void PlayLoss();

    } // end interface
} // end namespace