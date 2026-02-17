using ChipMasters.Resources.Animations;
using ChipMasters.Resources.Skins;
using GSR.EnDecic;
using GSR.EnDecic.EnDecs;
using System;

namespace ChipMasters.User
{
    /// <summary>
    /// Stores the currently equipped asset selections.
    /// </summary>
    public interface IAssetSelection
    {
        /// <summary>
        /// <see cref="IEnDec{T}"/> for coding <see cref="IAssetSelection"/> instances.
        /// </summary>
        public static readonly IEnDec<IAssetSelection> ENDEC = EnDecUtil.KeyedEnDecBuilder<string, IAssetSelection>(EnDecUtil.STRING)
            .Add("card_flip", ICardFlipAnimType.ENDEC, (x) => x.CardFlip)
            .Add("card_skin", ICardSkin.ENDEC, (x) => x.CardSkin)
            .Add("game_bg", IGameBackgroundSkin.ENDEC, (x) => x.GameBackgroundSkin)
            .Add("gui", IGUISkin.ENDEC, (x) => x.GUISkin)
            .Build((cf, cs, gbg, gui) => new RAssetSelection(cf, cs, gbg, gui));


        /// <summary>
        /// The currently selected card flip animation type.
        /// </summary>
        ICardFlipAnimType CardFlip { get; set; }

        /// <summary>
        /// Fired when <see cref="CardFlip"/> changes.
        /// </summary>
        event Action? OnCardFlipChanged;

        /// <summary>
        /// The currently selected card skin.
        /// </summary>
        ICardSkin CardSkin { get; set; }

        /// <summary>
        /// Fired when the card skin selection changes.
        /// </summary>
        event Action? OnCardSkinChanged;

        /// <summary>
        /// The currently selected game background.
        /// </summary>
        IGameBackgroundSkin GameBackgroundSkin { get; set; }

        /// <summary>
        /// Fired when the game background selection changes.
        /// </summary>
        event Action? OnGameBackgroundSkinChanged;

        /// <summary>
        /// The currently selected gui skin.
        /// </summary>
        IGUISkin GUISkin { get; set; }

        /// <summary>
        /// Fired when the <see cref="GUISkin"/> selection changes.
        /// </summary>
        event Action? OnGUISkinChanged;
    } // end interface
} // end namespace