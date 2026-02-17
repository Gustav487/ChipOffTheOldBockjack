using ChipMasters.GodotWrappers;
using System;

namespace ChipMasters.Menu.Displays.Assets.Animations.MatchConclusion
{
    /// <summary>
    /// <see cref="IMatchConclusionAnimator"/> that plays using a <see cref="IAnimatedSprite2D"/>
    /// </summary>
    public sealed class RMatchConclusionAnimator : IMatchConclusionAnimator
    {
        private readonly IAnimatedSprite2D _sprite;
        private readonly string _winKey;
        private readonly string _tieKey;
        private readonly string _lossKey;

        /// <inheritdoc/>
        public bool IsPlaying => _sprite.IsPlaying();
        /// <inheritdoc/>
        public event Action? OnPlaying;
        /// <inheritdoc/>
        public event Action? OnStopped;



        /// <inheritdoc/>
        public RMatchConclusionAnimator(IAnimatedSprite2D sprite, string winKey, string tieKey, string lossKey)
        {
            _sprite = sprite.AssertNotNull();
            _winKey = winKey.AssertNotNull();
            _tieKey = tieKey.AssertNotNull();
            _lossKey = lossKey.AssertNotNull();
            _sprite.AnimationFinished += () => OnStopped?.Invoke();
        } // end _Ready()



        /// <inheritdoc/>
        public void PlayLoss() => Play(_lossKey);

        /// <inheritdoc/>
        public void PlayTie() => Play(_tieKey);

        /// <inheritdoc/>
        public void PlayWin() => Play(_winKey);


        private void Play(string key)
        {
            _sprite.Visible = true;
            _sprite.Play(key);
            OnPlaying?.Invoke();
        } // end Play()

    } // end class
} // end namespace