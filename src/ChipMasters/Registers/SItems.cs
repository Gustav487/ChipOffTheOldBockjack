using ChipMasters.Items;
using GSR.Utilic.Generic;
using System.Collections.Generic;

namespace ChipMasters.Registers
{
    /// <summary>
    /// Class containing and for loading all defined item types. 
    /// 
    /// Warning: referencing this type outside of the Godot context'll result in a load exception.
    /// </summary>
    public static class SItems
    {
        /// <summary>
        /// One to one(Bijective) dictionary of all defined <see cref="IItem"/>s, and their identifiers.
        /// </summary>
        public static readonly IBijectiveDictionary<string, IItem> REGISTER = new Dictionary<string, IItem>()
        {
            { $"{SCardSkins.DEFAULT_ID}_cs", new RCardSkinItem(EItemCategory.CardSkin, "Default", 0, SCardSkins.DEFAULT)},
            { $"{SCardSkins.PLACEHOLDER_ID}_cs", new RCardSkinItem(EItemCategory.CardSkin, "Placeholder", 5, SCardSkins.PLACEHOLDER)},
            { $"{SCardSkins.LUNAR_SILVER_ID}_cs", new RCardSkinItem(EItemCategory.CardSkin, "Lunar Silver", 250, SCardSkins.LUNAR_SILVER)},
            { $"{SCardSkins.FLORAL_GOLD_ID}_cs", new RCardSkinItem(EItemCategory.CardSkin, "Floral Gold", 500, SCardSkins.FLORAL_GOLD)},
            { $"{SCardSkins.COLOR_SHADE_CROSS_ID}_cs", new RCardSkinItem(EItemCategory.CardSkin, "Color X Shade", 750, SCardSkins.COLOR_SHADE_CROSS)},
            { $"{SCardSkins.BREATHING_SPACE_ID}_cs", new RCardSkinItem(EItemCategory.CardSkin, "Breathing Space", 1000, SCardSkins.BREATHING_SPACE)},

            { $"{SGameBackgroundSkins.DEFAULT_ID}_gbg", new RGameBackgroundSkinItem(EItemCategory.GameBackground, "Default", 0, SGameBackgroundSkins.DEFAULT)},
            { $"{SGameBackgroundSkins.COLOR_SHADE_CROSS_ID}_gbg", new RGameBackgroundSkinItem(EItemCategory.GameBackground, "Color X Shade", 750, SGameBackgroundSkins.COLOR_SHADE_CROSS)},
            { $"{SGameBackgroundSkins.BREATHING_SPACE_ID}_gbg", new RGameBackgroundSkinItem(EItemCategory.GameBackground, "Breathing Space", 1000, SGameBackgroundSkins.BREATHING_SPACE)}
        }.ToImmutableBijectiveDictionary();

        static SItems()
        {
#if GODOT && DEBUG
#warning print out messages indicating if any animation/skin/etc option is missing a corresponding item.
#endif
        } // end stor
    } // end class
} // end namespace
