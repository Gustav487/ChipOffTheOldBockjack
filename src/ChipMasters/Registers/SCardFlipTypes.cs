using ChipMasters.Resources.Animations;
using GSR.Utilic.Generic;
using System.Collections.Generic;

namespace ChipMasters.Registers
{
    /// <summary>
    /// Static class holding a register for identifying the card flip animations.
    /// </summary>
    public static class SCardFlipTypes
    {
        #region IDs
        /// <summary>
        /// Identifier of <see cref="ONE_EIGHTY"/>.
        /// </summary>
        public const string ONE_EIGHTY_ID = "one_eighty";
        #endregion

        #region Values
        /// <summary>
        /// Extremely simple animation, card rotates 180 degrees staying in place.
        /// </summary>
        public static readonly ICardFlipAnimType ONE_EIGHTY = new RCardFlipAnimType();
        #endregion




        /// <summary>
        /// One to one(Bijective) dictionary for indentifying card flip animations. 
        /// The string key is the animation name on the card model, 
        /// the <see cref="ICardFlipAnimType"/> contains no information and is simply a reference lookup for now.
        /// </summary>
        public static readonly IBijectiveDictionary<string, ICardFlipAnimType> REGISTER = new Dictionary<string, ICardFlipAnimType>()
        {
            { ONE_EIGHTY_ID, ONE_EIGHTY },
        }.ToImmutableBijectiveDictionary();
    } // end class
} // end namespace