using ChipMasters.Resources;
using ChipMasters.Resources.Skins;
using System.Collections.Generic;
using System.Linq;

namespace ChipMasters.Registers
{
    /// <summary>
    /// Static class containing a registery for identifying the <see cref="ETextureCatgeory"/> of each texture.
    /// </summary>
    public static class STextureCategories
    {
        /// <summary>
        /// Registery for identifying the <see cref="ETextureCatgeory"/> of each texture
        /// </summary>
        public static readonly IDictionary<string, ETextureCatgeory> REGISTER =
            new Dictionary<string, ETextureCatgeory>(
                new Dictionary<string, ETextureCatgeory>()
                    {
                        { SAssetUtil.CARD_BACK_PATH , ETextureCatgeory.Card },
                        { SAssetUtil.CARD_FRONT_PATH , ETextureCatgeory.Card },
                        { "textures/guis/title_bg", ETextureCatgeory.GUI },
                        { "textures/guis/title_logo", ETextureCatgeory.GUI },
                        { "textures/guis/menu_bg", ETextureCatgeory.GUI },
                        { "textures/guis/game_bg", ETextureCatgeory.GameBackground },
                        { "textures/guis/game_overlay", ETextureCatgeory.GUI }
                    }
                .Concat(
                    SCardTypes.REGISTER.Keys
                    .Select((x) => KeyValuePair.Create($"textures/cards/{x}", ETextureCatgeory.Card))));
    } // end class
} // end namespace