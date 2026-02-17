using ChipMasters.Resources;
using ChipMasters.Resources.Skins;
using GSR.Utilic.Generic;
using System.Collections.Generic;

namespace ChipMasters.Registers
{
    /// <summary>
    /// Static class containing a register of all <see cref="ICardSkin"/>s that the game can recognize and handle.
    /// </summary>
    public static class SCardSkins
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


        /// <summary>
        /// Identifier of <see cref="FLORAL_GOLD"/>.
        /// </summary>
        public const string FLORAL_GOLD_ID = SAssetUtil.FLORAL_GOLD_DIR;

        /// <summary>
        /// Identifier of <see cref="LUNAR_SILVER"/>.
        /// </summary>
        public const string LUNAR_SILVER_ID = SAssetUtil.LUNAR_SILVER_DIR;

        /// <summary>
        /// Identifier of <see cref="DEFAULT"/>.
        /// </summary>
        public const string PLACEHOLDER_ID = SAssetUtil.PLACEHOLDER_DIR;
        #endregion



        #region Values
        /// <summary>
        /// Default skin set.
        /// </summary>
        public static readonly ICardSkin DEFAULT = new RCardSkins();

        /// <summary>
        /// Animated purple outer space skin.
        /// </summary>
        public static readonly ICardSkin BREATHING_SPACE = new RCardSkins();

        /// <summary>
        /// Animated rainbow, black, white and gray skin.
        /// </summary>
        public static readonly ICardSkin COLOR_SHADE_CROSS = new RCardSkins();

        /// <summary>
        /// Flora and gold themed skin.
        /// </summary>
        public static readonly ICardSkin FLORAL_GOLD = new RCardSkins();

        /// <summary>
        /// Luna and silver themed skin.
        /// </summary>
        public static readonly ICardSkin LUNAR_SILVER = new RCardSkins();

        /// <summary>
        /// Placeholder skin set used during sprint 1.
        /// </summary>
        public static readonly ICardSkin PLACEHOLDER = new RCardSkins();
        #endregion



        /// <summary>
        /// One to one(Bijective) dictionary of all defined <see cref="ICardSkin"/>s, and their identifiers.
        /// 
        /// The unique identifier is both written to save files, and used to identify the asset that to find assets from. 
        /// The <see cref="IGameBackgroundSkin"/> contains no information and is simply a reference lookup for now.
        /// </summary>
        public static readonly IBijectiveDictionary<string, ICardSkin> REGISTER = new Dictionary<string, ICardSkin>()
        {
            { DEFAULT_ID, DEFAULT },
            { BREATHING_SPACE_ID, BREATHING_SPACE },
            { COLOR_SHADE_CROSS_ID, COLOR_SHADE_CROSS },
            { FLORAL_GOLD_ID, FLORAL_GOLD },
            { LUNAR_SILVER_ID, LUNAR_SILVER },
            { PLACEHOLDER_ID, PLACEHOLDER },
        }.ToImmutableBijectiveDictionary();

    } // end interface
} // end namespace