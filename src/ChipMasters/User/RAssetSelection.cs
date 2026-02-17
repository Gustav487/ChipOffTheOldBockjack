using ChipMasters.Registers;
using ChipMasters.Resources.Animations;
using ChipMasters.Resources.Skins;
using System;

namespace ChipMasters.User
{
    /// <summary>
    /// Simple <see cref="IAssetSelection"/> implementation.
    /// </summary>
    public class RAssetSelection : IAssetSelection
    {
        /// <inheritdoc/>
        public ICardSkin CardSkin
        {
            get => _cardSkin;
            set
            {
                if (_cardSkin == value) return;
                _cardSkin = value;
                OnCardSkinChanged?.Invoke();
            }
        }
        private ICardSkin _cardSkin;

        /// <inheritdoc/>
        public IGameBackgroundSkin GameBackgroundSkin
        {
            get => _gameBackgroundSkin;
            set
            {
                if (_gameBackgroundSkin == value) return;
                _gameBackgroundSkin = value;
                OnGameBackgroundSkinChanged?.Invoke();
            }
        }
        private IGameBackgroundSkin _gameBackgroundSkin;

        /// <inheritdoc/>
        public IGUISkin GUISkin
        {
            get => _guiSkin;
            set
            {
                if (_guiSkin == value) return;
                _guiSkin = value;
                OnGUISkinChanged?.Invoke();
            }
        }
        private IGUISkin _guiSkin;

        /// <inheritdoc/>
        public ICardFlipAnimType CardFlip
        {
            get => _cardFlip;
            set
            {
                if (_cardFlip == value) return;
                _cardFlip = value;
                OnCardFlipChanged?.Invoke();
            }
        }
        private ICardFlipAnimType _cardFlip;

        /// <inheritdoc/>
        public event Action? OnCardSkinChanged;
        /// <inheritdoc/>
        public event Action? OnGameBackgroundSkinChanged;
        /// <inheritdoc/>
        public event Action? OnGUISkinChanged;
        /// <inheritdoc/>
        public event Action? OnCardFlipChanged;



        /// <inheritdoc/>
        /// <param name="cardFlip">If null "one_eighty" is used.</param>
        /// <param name="cardSkin">If null "_" is used.</param>
        /// <param name="gameBackgroundSkin">If null "_" is used.</param>
        /// <param name="guiSkin">If null "_" is used.</param>
        public RAssetSelection(ICardFlipAnimType? cardFlip = null, ICardSkin? cardSkin = null, IGameBackgroundSkin? gameBackgroundSkin = null, IGUISkin? guiSkin = null)
        {
            _cardFlip = cardFlip ?? SCardFlipTypes.ONE_EIGHTY;
            _cardSkin = cardSkin ?? SCardSkins.DEFAULT;
            _gameBackgroundSkin = gameBackgroundSkin ?? SGameBackgroundSkins.DEFAULT;
            _guiSkin = guiSkin ?? SGUISkins.DEFAULT;
        } // end ctor()

    } // end class
} // end namespace