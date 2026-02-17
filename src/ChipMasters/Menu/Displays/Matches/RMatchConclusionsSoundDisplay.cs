using ChipMasters.Games.Appraisers;
using ChipMasters.Games.Matches;
using ChipMasters.GodotWrappers;
using GSR.Utilic;

namespace ChipMasters.Menu.Displays.Matches
{
    /// <summary>
    /// <see cref="IMatch"/> <see cref="IDisplay{T}"/> for playing sounds on conclusion according to match result.
    /// </summary>
    public sealed class RMatchConclusionsSoundDisplay : IDisplay<IMatch>
    {
        /// <inheritdoc/>
        public IMatch? Display
        {
            get => _display;
            set
            {
                if (_display == value)
                    return;

                _display = value;
                if (_display is null)
                    return;

                if (_display.IsConcluded)
                    PlaySound();
                else
                    _display.OnConcluded += PlaySound;

            }
        } // end Match
        private IMatch? _display;

        private readonly IAudioStreamPlayer _winSound;
        private readonly IAudioStreamPlayer _tieSound;
        private readonly IAudioStreamPlayer _lossSound;



        /// <inheritdoc/>
        public RMatchConclusionsSoundDisplay(IAudioStreamPlayer winSound, IAudioStreamPlayer tieSound, IAudioStreamPlayer lossSound)
        {
            _winSound = winSound;
            _tieSound = tieSound;
            _lossSound = lossSound;
        } // end ctor

        /// <inheritdoc/>
        public void Dispose()
        {
            if (_display is not null)
                _display.OnConcluded -= PlaySound;
        } // end Dispose()



        /// <inheritdoc/>
        public void PlaySound()
        {
            if (Display is null)
                throw new UnexpectedStateException();

            _winSound.Stop();
            _tieSound.Stop();
            _lossSound.Stop();

            VHandAppraisal playerHandAppraised = Display.AppraisePlayerHand(false).AssertKnown();
            VHandAppraisal dealerHandAppraised = Display.AppraiseDealerHand(false).AssertKnown();

            if (playerHandAppraised > dealerHandAppraised)
                _winSound.Play();
            else if (playerHandAppraised == dealerHandAppraised)
                _tieSound.Play();
            else
                _lossSound.Play();
        } // end PlaySound()
    } // end class
} // end namespace