using ChipMasters.Games.Appraisers;
using ChipMasters.Games.Matches;
using ChipMasters.IO;
using GSR.Utilic.Generic;
using System.Collections.Generic;

namespace ChipMasters.Registers
{
    /// <summary>
    /// Static class containing a registery of <see cref="IMatch"/> <see cref="IType{T}"/>s that the game can recognize and handle.
    /// </summary>
    public static class SAppraiserTypes
    {
        /// <summary>
        /// One to one(Bijective) dictionary of all defined <see cref="IMatch"/> <see cref="IType{T}"/>s, and their identifiers.
        /// </summary>
        public static readonly IBijectiveDictionary<string, IType<IAppraiser>> REGISTER = new Dictionary<string, IType<IAppraiser>>()
        {
            { "standard", new RType<IAppraiser>(RStandardAppraiser.ENDEC.Cast<IAppraiser, RStandardAppraiser>(), (x) => x.GetType() == typeof(RStandardAppraiser)) },
            { "n_standard", new RType<IAppraiser>(NStandardAppraiser.ENDEC, (x) => x.GetType() == typeof(NStandardAppraiser)) },

            { "total_val", new RType<IAppraiser>(RTotalValueAppraiser.ENDEC.Cast<IAppraiser, RTotalValueAppraiser>(), (x) => x.GetType() == typeof(RTotalValueAppraiser)) },
            { "n_total_val", new RType<IAppraiser>(NTotalValueAppraiser.ENDEC, (x) => x.GetType() == typeof(NTotalValueAppraiser)) },
        }.ToImmutableBijectiveDictionary();
    } // end class
} // end namespace