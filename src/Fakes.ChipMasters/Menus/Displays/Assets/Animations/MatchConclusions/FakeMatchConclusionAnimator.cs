using ChipMasters.Menu.Displays.Assets.Animations.MatchConclusion;

namespace Fakes.ChipMasters.Menus.Displays.Assets.Animations.MatchConclusions
{
    public sealed class FakeMatchConclusionAnimator : IMatchConclusionAnimator
    {
        public int Wins { get; private set; }
        public int Ties { get; private set; }
        public int Losses { get; private set; }

        public bool IsPlaying { get; private set; }

        public event Action? OnPlaying;
        public event Action? OnStopped;



        public void PlayLoss()
        {
            Play();
            Losses++;
        } // PlayLoss()
        public void PlayTie()
        {
            Play();
            Ties++;
        } // PlayTie()
        public void PlayWin()
        {
            Play();
            Wins++;
        } // PlayWin()



        private void Play()
        {
            IsPlaying = true;
            OnPlaying?.Invoke();
        } // end Pay()

        public void Stop()
        {
            IsPlaying = false;
            OnStopped?.Invoke();
        }
    } // end class
} // end namespace