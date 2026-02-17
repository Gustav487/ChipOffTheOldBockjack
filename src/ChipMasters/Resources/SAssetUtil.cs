using ChipMasters.Cards;
using ChipMasters.IO;
using ChipMasters.Items;
using ChipMasters.Registers;
using ChipMasters.Resources.Materials;
using ChipMasters.User;
using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ChipMasters.Resources
{
    /// <summary>
    /// Utility class for holding various asset pathing utility methods and constants.
    /// </summary>
    public static class SAssetUtil
    {
        // NOTE: I am going to be referring to one of the unlockable alternate game looks/styles/skins as an "asset set" here.

        private static readonly IDictionary<string, IAssetProvider<IMaterialData>> TEXTURE_PROVIDERS = new Dictionary<string, IAssetProvider<IMaterialData>>();

        /// <summary>
        /// Name of the directory storing game assets.
        /// </summary>
        public const string ASSET_DIR = $"res://assets";

        /// <summary>
        /// Name of the directory storing the default textures/audio asset set.
        /// </summary>
        public const string DEFAULT_ASSET_SET_DIR_ = $"_";
        /// <summary>
        /// Name of the directory storing the purple animated celestial texture.
        /// </summary>
        public const string BREATHING_SPACE_DIR = $"breathing_space";
        /// <summary>
        /// Name of the directory storing the animated hue texture.
        /// </summary>
        public const string COLOR_SHADE_CROSS_DIR = $"color_shade_cross";
        /// <summary>
        /// Name of the directory storing the plants and gold texture.
        /// </summary>
        public const string FLORAL_GOLD_DIR = $"floral_gold";
        /// <summary>
        /// Name of the directory storing the moon and silver texture.
        /// </summary>
        public const string LUNAR_SILVER_DIR = $"lunar_silver";
        /// <summary>
        /// The placeholder asset set that was used for the cards during sprint 1.
        /// </summary>
        public const string PLACEHOLDER_DIR = $"placeholder";

        /// <summary>
        /// Name of the directory of texture files.
        /// </summary>
        public const string TEXTURES_DIR = "textures";


        /// <summary>
        /// Name of the directory of card textures. 
        /// </summary>
        public const string CARDS_DIR = "cards";

        /// <summary>
        /// Path inside an asset set to the texture for rendering a cards back
        /// </summary>
        public const string CARD_BACK_PATH = $"{TEXTURES_DIR}/{CARDS_DIR}/card_back";
        /// <summary>
        /// Path inside an asset set to the texture for rendering a cards front, underneath the layer indicating suit and rank.
        /// </summary>
        public const string CARD_FRONT_PATH = $"{TEXTURES_DIR}/{CARDS_DIR}/card_front";



        /// <summary>
        /// Lazy loaded missing texture texture.
        /// </summary>
        public static readonly Lazy<IMaterialData> MISSING_TEXTURE = new(() => new RStaticMaterialData(GD.Load<Texture2D>($"{ASSET_DIR}/_/{TEXTURES_DIR}/missing.tres")));





        /// <summary>
        /// Get the <see cref="IMaterialData"/> for the users currently selected skin for <paramref name="card"/>.
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        public static IMaterialData GetMaterials(this ICard card) => GetMaterials($"{TEXTURES_DIR}/{CARDS_DIR}/{SCardTypes.REGISTER.I[card.Type()]}");
        /// <summary>
        /// Get item preview
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static IMaterialData GetPreviewMaterials(IItem item) => GetMaterialProvider($"previews/{SItems.REGISTER.I[item]}").GetOrMissing("_");

        /// <summary>
        /// Get the <see cref="IMaterialData"/> for the users currently selected skin for <paramref name="partialPath"/>.
        /// </summary>
        /// <param name="partialPath"></param>
        /// <returns></returns>
        public static IMaterialData GetMaterials(string partialPath)
            => GetMaterialProvider(partialPath)
            .GetOrMissing(UsersSelectionFor(partialPath));

        private static string UsersSelectionFor(string partialPath)
            => STextureCategories.REGISTER[partialPath] switch
            {
                Skins.ETextureCatgeory.Card => SCardSkins.REGISTER.I[RUser.INSTANCE.AssetSelection.CardSkin],
                Skins.ETextureCatgeory.GameBackground => SGameBackgroundSkins.REGISTER.I[RUser.INSTANCE.AssetSelection.GameBackgroundSkin],
                Skins.ETextureCatgeory.GUI => SGUISkins.REGISTER.I[RUser.INSTANCE.AssetSelection.GUISkin],
                _ => throw new InvalidOperationException(),
            };



        /// <summary>
        /// Get the asset, or the standard missing texture.
        /// </summary>
        /// <param name="assetProvider"></param>
        /// <param name="assetSetID"></param>
        /// <returns></returns>
        public static IMaterialData GetOrMissing(this IAssetProvider<IMaterialData> assetProvider, string assetSetID)
            => assetProvider.TryGet(assetSetID, out IMaterialData? t)
            ? t
            : MISSING_TEXTURE.Value;

        /// <summary>
        /// Returns the texture of a specific card face.
        /// </summary>
        /// <param name="card">The card to get the texture for.</param>
        /// <returns>The texture of the card face.</returns>
        public static IAssetProvider<IMaterialData> GetMaterialProvider(this ICard card)
            => GetMaterialProvider($"{TEXTURES_DIR}/{CARDS_DIR}/{SCardTypes.REGISTER.I[card.Type()]}");

        /// <summary>
        /// Get the <see cref="IAssetProvider{TAss}"/> for the texture path.
        /// </summary>
        /// <param name="partialPath"></param>
        /// <returns></returns>
        public static IAssetProvider<IMaterialData> GetMaterialProvider(string partialPath)
        {
            if (TEXTURE_PROVIDERS.TryGetValue(partialPath, out IAssetProvider<IMaterialData>? t))
                return t;

            TEXTURE_PROVIDERS[partialPath] = new RCachedMaterialProvider(partialPath);
            return TEXTURE_PROVIDERS[partialPath];
        } // end GetTextureProvider()



        /// <summary>
        /// Get the <see cref="ICardType"/> of this <paramref name="card"/> from the <see cref="ICardType"/>s that's register in <see cref="SCardTypes.REGISTER"/>.
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        public static IType<ICard> Type(this ICard card) => card.Type(SCardTypes.REGISTER.Values);

        /// <summary>
        /// Get the <see cref="IType{T}"/> of this <paramref name="instance"/> from the <see cref="IType{T}"/>s that're provided in <paramref name="instance"/>.
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="types"></param>
        /// <returns></returns>
        public static IType<T> Type<T>(this T instance, IEnumerable<IType<T>> types)
            => types.First((x) => x.IsTypeOf(instance));

    } // end class
} // end namespace