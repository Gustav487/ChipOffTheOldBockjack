using ChipMasters.Games.Matches;
using ChipMasters.IO;
using GSR.Utilic.Generic;
using System.Collections.Generic;

namespace ChipMasters.Registers
{
    /// <summary>
    /// Static class containing a registery of <see cref="IMatch"/> <see cref="IType{T}"/>s that the game can recognize and handle.
    /// </summary>
    public static class SMatchTypes
    {
        /// <summary>
        /// One to one(Bijective) dictionary of all defined <see cref="IMatch"/> <see cref="IType{T}"/>s, and their identifiers.
        /// </summary>
        public static readonly IBijectiveDictionary<string, IType<IMatch>> REGISTER = new Dictionary<string, IType<IMatch>>()
        {
            { "standard", new RType<IMatch>(RMatch.ENDEC.Cast<IMatch, RMatch>(), (x) => x.GetType() == typeof(RMatch)) },
            { "turned", new RType<IMatch>(RTurnedMatch.ENDEC.Cast<IMatch, RTurnedMatch>(), (x) => x.GetType() == typeof(RTurnedMatch)) },
        }.ToImmutableBijectiveDictionary();
    } // end class
} // end namespace