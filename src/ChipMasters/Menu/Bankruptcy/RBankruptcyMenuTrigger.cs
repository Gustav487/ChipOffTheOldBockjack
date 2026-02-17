using ChipMasters.Games.Sessions;
using ChipMasters.Menu.Displays.Assets.Animations.MatchConclusion;
using ChipMasters.User;
using System;

namespace ChipMasters.Menu.Bankruptcy
{
    /// <summary>
    /// Class that displays an <see cref="IBankruptcyMenu"/> when an <see cref="IMatchConclusionAnimator"/> has stopped.
    /// </summary>
    public sealed class RBankruptcyMenuTrigger : IDisposable
    {
        private readonly IWallet _wallet;
        private readonly ISession _session;
        private readonly IMatchConclusionAnimator _animator;
        private readonly IBankruptcyMenu _bankruptcyMenu;

        /// <inheritdoc/>
        public RBankruptcyMenuTrigger(IWallet wallet, ISession session, IMatchConclusionAnimator animator, IBankruptcyMenu bankruptcyMenu)
        {
            _wallet = wallet;
            _session = session;
            _animator = animator.AssertNotNull();
            _bankruptcyMenu = bankruptcyMenu.AssertNotNull();
            _animator.OnStopped += AnimationStopped;
            AnimationStopped();
        } // end ctor

        /// <inheritdoc/>
        public void Dispose()
        {
            _animator.OnStopped -= AnimationStopped;
        } // end Dispose()

        /// <summary>
        /// Display the bankruptcy confirmation box if the match is concluded, 
        /// the conclusion animation has stopped, and the user is bankrupt.
        /// </summary>
        private void AnimationStopped()
        {
            if (_session.Match.IsConcluded && !_animator.IsPlaying && _wallet.IsBankrupt())
                _bankruptcyMenu.ShowBankruptcyBox();
        } // end AnimationStopped()
    } // end class
} // end namespace