using ChipMasters.Registers;
using ChipMasters.Resources.Animations;
using ChipMasters.Resources.Skins;
using ChipMasters.User;

namespace Fakes.ChipMasters.Users
{
    /// <summary>
    /// Fake implementation of ICosmeticSelection for testing purposes.
    /// </summary>
    public sealed class FakeAssetSelection : IAssetSelection
    {

        public ICardFlipAnimType CardFlip { get; set; } = SCardFlipTypes.ONE_EIGHTY;
        public ICardSkin CardSkin { get; set; } = SCardSkins.DEFAULT;
        public IGameBackgroundSkin GameBackgroundSkin { get; set; } = SGameBackgroundSkins.DEFAULT;
        public IGUISkin GUISkin { get; set; } = SGUISkins.DEFAULT;

        public event Action? OnCardSkinChanged;
        public event Action? OnGameBackgroundSkinChanged;
        public event Action? OnCardFlipChanged;
        public event Action? OnGUISkinChanged;



        public void CardFlipChanged()
            => OnCardFlipChanged?.Invoke();
    } // end class
} // end namespace