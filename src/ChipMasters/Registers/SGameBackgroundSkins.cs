using ChipMasters.Resources;
using ChipMasters.Resources.Skins;
using GSR.Utilic.Generic;
using System.Collections.Generic;

namespace ChipMasters.Registers
{
    /// <summary>
    /// Static class containing a register of all <see cref="IGameBackgroundSkin"/>s that the game can recognize and handle.
    /// </summary>
    public static class SGameBackgroundSkins
    {
        #region IDs
        /// <summary>
        /// Identifier of <see cref="DEFAULT"/>.
        /// </summary>
        public const string DEFAULT_ID = SAssetUtil.DEFAULT_ASSET_SET_DIR_;

        /// <summary>
        /// Identifier of <see cref="BREATHING_SPACE"/>.
        /// </summary>
        public const string BREATHING_SPACE_ID = SAssetUtil.BREATHING_SPACE_DIR;

        /// <summary>
        /// Identifier of <see cref="COLOR_SHADE_CROSS"/>.
        /// </summary>
        public const string COLOR_SHADE_CROSS_ID = SAssetUtil.COLOR_SHADE_CROSS_DIR;
        #endregion



        #region Values
        /// <summary>
        /// Default skin set.
        /// </summary>
        public static readonly IGameBackgroundSkin DEFAULT = new RGameBackgroundSkin();

        /// <summary>
        /// Animated purple outer space skin.
        /// </summary>
        public static readonly IGameBackgroundSkin BREATHING_SPACE = new RGameBackgroundSkin();

        /// <summary>
        /// Animated rainbow, black, white and gray skin.
        /// </summary>
        public static readonly IGameBackgroundSkin COLOR_SHADE_CROSS = new RGameBackgroundSkin();
        #endregion



        /// <summary>
        /// One to one(Bijective) dictionary of all defined <see cref="IGameBackgroundSkin"/>s, and their identifiers.
        /// 
        /// The unique identifier is both written to save files, and used to identify the asset that to find assets from. 
        /// The <see cref="IGameBackgroundSkin"/> contains no information and is simply a reference lookup for now.
        /// </summary>
        public static readonly IBijectiveDictionary<string, IGameBackgroundSkin> REGISTER = new Dictionary<string, IGameBackgroundSkin>()
        {
            { DEFAULT_ID, DEFAULT },
            { BREATHING_SPACE_ID, BREATHING_SPACE},
            { COLOR_SHADE_CROSS_ID, COLOR_SHADE_CROSS},
        }.ToImmutableBijectiveDictionary();
    } // end interface
} // end namespace