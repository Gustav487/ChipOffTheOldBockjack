using ChipMasters.GodotWrappers;
using ChipMasters.Registers;
using ChipMasters.Resources;
using ChipMasters.User;
using Godot;
using System;

namespace ChipMasters.Menu.Displays.Assets.Textures
{
    /// <summary>
    /// <see cref="Godot.TextureRect"/> which displays an asset from the current asset set.
    /// </summary>
    public partial class NTextureDisplay : NControl // technically valid for any CanvasItem
    {
        [Export] private string _assetPath = null!;



        /// <inheritdoc/>
        public override void _Ready()
        {
            base._Ready();
            _assetPath.AssertNotNull();

            RefreshDisplay();

            switch (STextureCategories.REGISTER[_assetPath])
            {
                case Resources.Skins.ETextureCatgeory.Card:
                    RUser.INSTANCE.AssetSelection.OnCardSkinChanged += RefreshDisplay;
                    break;
                case Resources.Skins.ETextureCatgeory.GameBackground:
                    RUser.INSTANCE.AssetSelection.OnGameBackgroundSkinChanged += RefreshDisplay;
                    break;
                case Resources.Skins.ETextureCatgeory.GUI:
                    RUser.INSTANCE.AssetSelection.OnGUISkinChanged += RefreshDisplay;
                    break;
                default:
                    throw new InvalidOperationException();
            }
        } // end _Ready()

        /// <inheritdoc/>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            switch (STextureCategories.REGISTER[_assetPath])
            {
                case Resources.Skins.ETextureCatgeory.Card:
                    RUser.INSTANCE.AssetSelection.OnCardSkinChanged -= RefreshDisplay;
                    break;
                case Resources.Skins.ETextureCatgeory.GameBackground:
                    RUser.INSTANCE.AssetSelection.OnGameBackgroundSkinChanged -= RefreshDisplay;
                    break;
                case Resources.Skins.ETextureCatgeory.GUI:
                    RUser.INSTANCE.AssetSelection.OnGUISkinChanged -= RefreshDisplay;
                    break;
                default:
                    throw new InvalidOperationException();
            }
        } // end Dispose



        private void RefreshDisplay() => Material = SAssetUtil.GetMaterials(_assetPath).Material2D;

    } // end class
} // end namespace
