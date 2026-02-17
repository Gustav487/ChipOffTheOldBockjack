using ChipMasters.GodotWrappers;
using Godot;
using System;

namespace ChipMasters.Menu.Displays.Assets.Animations.MatchConclusion
{
    /// <summary>
    /// <see cref="NNode"/> based <see cref="IMatchConclusionAnimator"/> that wraps <see cref="RMatchConclusionAnimator"/>
    /// </summary>
    public partial class NMatchConclusionAnimator : NNode, IMatchConclusionAnimator
    {
        [Export] private Node _sprite = null!;
        [Export] private string _winKey = "You_Win";
        [Export] private string _tieKey = "Tie";
        [Export] private string _lossKey = "Loss";

        /// <inheritdoc/>
        public bool IsPlaying => Inner.IsPlaying;
        /// <inheritdoc/>
        public event Action? OnPlaying;
        /// <inheritdoc/>
        public event Action? OnStopped;



        private IMatchConclusionAnimator Inner => _inner ?? throw new RNotReadyException();
        private IMatchConclusionAnimator? _inner;

        /// <inheritdoc/>
        public override void _Ready()
        {
            base._Ready();
            _inner = new RMatchConclusionAnimator((IAnimatedSprite2D)_sprite.AssertNotNull(), _winKey.AssertNotNull(), _tieKey.AssertNotNull(), _lossKey.AssertNotNull());

            _inner.OnPlaying += () => OnPlaying?.Invoke();
            _inner.OnStopped += () => OnStopped?.Invoke();
        } // end _Ready()



        /// <inheritdoc/>
        public void PlayLoss() => Inner.PlayLoss();

        /// <inheritdoc/>
        public void PlayTie() => Inner.PlayTie();

        /// <inheritdoc/>
        public void PlayWin() => Inner.PlayWin();

    } // end class
} // end namespace
