using ChipMasters.Resources;
using ChipMasters.Resources.Skins;
using GSR.Utilic.Generic;
using System.Collections.Generic;

namespace ChipMasters.Registers
{
    /// <summary>
    /// Static class containing a register of all <see cref="ICardSkin"/>s that the game can recognize and handle.
    /// </summary>
    public static class SGUISkins
    {
        #region IDs
        /// <summary>
        /// Identifier of <see cref="DEFAULT"/>.
        /// </summary>
        public const string DEFAULT_ID = SAssetUtil.DEFAULT_ASSET_SET_DIR_;
        #endregion



        #region Values
        /// <summary>
        /// Default skin set.
        /// </summary>
        public static readonly IGUISkin DEFAULT = new RGUISkins();
        #endregion



        /// <summary>
        /// One to one(Bijective) dictionary of all defined <see cref="ICardSkin"/>s, and their identifiers.
        /// 
        /// The unique identifier is both written to save files, and used to identify the asset that to find assets from. 
        /// The <see cref="IGameBackgroundSkin"/> contains no information and is simply a reference lookup for now.
        /// </summary>
        public static readonly IBijectiveDictionary<string, IGUISkin> REGISTER = new Dictionary<string, IGUISkin>()
        {
            { DEFAULT_ID, DEFAULT }
        }.ToImmutableBijectiveDictionary();

    } // end interface
} // end namespace