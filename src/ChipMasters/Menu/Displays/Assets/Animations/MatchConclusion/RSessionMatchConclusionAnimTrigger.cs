using ChipMasters.Games.Appraisers;
using ChipMasters.Games.Matches;
using ChipMasters.Games.Sessions;
using System;

namespace ChipMasters.Menu.Displays.Assets.Animations.MatchConclusion
{
    /// <summary>
    /// Class that causes a <see cref="IMatchConclusionAnimator"/> to play in coordination with an <see cref="ISession"/>.
    /// </summary>
    public sealed class RSessionMatchConclusionAnimTrigger : IDisposable
    {
        private readonly ISession _session;
        private readonly IMatchConclusionAnimator _animator;



        /// <inheritdoc/>
        public RSessionMatchConclusionAnimTrigger(ISession session, IMatchConclusionAnimator animator)
        {
            _session = session.AssertNotNull();
            _animator = animator.AssertNotNull();
            _session.OnMatchChanged += MatchChanged;
            MatchChanged();
        } // end ctor

        /// <inheritdoc/>
        public void Dispose()
        {
            _session.OnMatchChanged -= MatchChanged;
            _session.Match.OnConcluded -= Play;
        } // end Dispose()

        private void MatchChanged()
        {
            if (_session.Match.IsConcluded)
                Play();
            else
                _session.Match.OnConcluded += Play;
        } // end MatchChanged()



        private void Play()
        {

            var player_hand_appraised = _session.Match.AppraisePlayerHand(false).AssertKnown();
            var dealer_hand_appraised = _session.Match.AppraiseDealerHand(false).AssertKnown();

            if (player_hand_appraised > dealer_hand_appraised)
                _animator.PlayWin();
            else if (player_hand_appraised == dealer_hand_appraised)
                _animator.PlayTie();
            else
                _animator.PlayLoss();
        } // end Play()

    } // end class
} // end namespace