using ChipMasters.Cards;
using ChipMasters.GodotWrappers;
using ChipMasters.Registers;
using ChipMasters.User;

namespace ChipMasters.Menu.Displays.Cards
{
    /// <summary>
    /// <see cref="ICard"/> <see cref="IDisplay{T}"/> that handles animating a card to perform a flip animation.
    /// </summary>
    public sealed class RCardFlipAnimationDisplay : IDisplay<ICard>
    {
        /// <inheritdoc/>
        public ICard? Display
        {
            get => _displaying;
            set
            {
                if (_displaying == value)
                    return;

                if (_displaying is not null)
                    _displaying.OnFlipped -= AnimateFlip;

                _displaying = value;

                if (_displaying is not null)
                    _displaying.OnFlipped += AnimateFlip;
                RefreshDisplay();
            }
        } // end Card
        private ICard? _displaying;

        private string Anim => SCardFlipTypes.REGISTER.I[_assetSelection.CardFlip];
        private readonly IAssetSelection _assetSelection;
        private readonly IAnimationPlayer _animationPlayer;



        /// <inheritdoc/>
        public RCardFlipAnimationDisplay(IAssetSelection assetSelection, IAnimationPlayer animationPlayer)
        {
            _assetSelection = assetSelection.AssertNotNull();
            _animationPlayer = animationPlayer.AssertNotNull();

            _assetSelection.OnCardFlipChanged += RefreshDisplay;
        } // end _Ready()

        /// <inheritdoc/>
        public void Dispose()
        {
            _assetSelection.OnCardFlipChanged -= RefreshDisplay;
            if (_displaying is not null)
                _displaying.OnFlipped -= AnimateFlip;
        } // end Dispose()



        private void RefreshDisplay()
        {
            if (_displaying is null)
                return;

            if (_displaying.Veiled)
                SetVeiled();
            else
                SetRevealed();
        } // end RefreshDisplay()

        private void SetVeiled()
        {
            _animationPlayer.Play(Anim); // Animations transition card from back facing to front facing. Thus setting to start of animation makes card veiled
            _animationPlayer.Stop(keepState: true);
        } // end SetVeiled()

        private void SetRevealed()
        {
            _animationPlayer.PlayBackwards(Anim); // Animations transition card from back facing to front facing. Thus setting to start of the reversed animation makes card unveiled
            _animationPlayer.Stop(keepState: true);
        } // end SetRevealed()

        private void AnimateFlip()
        {
            if (_displaying!.Veiled)
                _animationPlayer.PlayBackwards(Anim);
            else
                _animationPlayer.Play(Anim);
        } // end AnimateFlip()

    } // end class
} // end namespace
