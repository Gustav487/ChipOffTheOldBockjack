using ChipMasters.Games.Matches.Providers;
using ChipMasters.IO;
using GSR.Utilic.Generic;
using System.Collections.Generic;

namespace ChipMasters.Registers
{
    /// <summary>
    /// Static class containing a registery of <see cref="IMatchProvider"/> <see cref="IType{T}"/>s that the game can recognize and handle.
    /// </summary>
    public static class SMatchProviderTypes
    {
        /// <summary>
        /// One to one(Bijective) dictionary of all defined <see cref="IMatchProvider"/> <see cref="IType{T}"/>s, and their identifiers.
        /// </summary>
        public static readonly IBijectiveDictionary<string, IType<IMatchProvider>> REGISTER = new Dictionary<string, IType<IMatchProvider>>()
        {
            { "standard", new RType<IMatchProvider>(RStandardMatchProvider.ENDEC.Cast<IMatchProvider, RStandardMatchProvider>(), (x) => x.GetType() == typeof(RStandardMatchProvider)) },
            { "n_standard", new RType<IMatchProvider>(NStandardMatchProvider.ENDEC, (x) => x.GetType() == typeof(NStandardMatchProvider)) },
            { "turned", new RType<IMatchProvider>(RTurnedMatchProvider.ENDEC.Cast<IMatchProvider, RTurnedMatchProvider>(), (x) => x.GetType() == typeof(RTurnedMatchProvider)) },
            { "n_turned", new RType<IMatchProvider>(NTurnedMatchProvider.ENDEC, (x) => x.GetType() == typeof(NTurnedMatchProvider)) },
        }.ToImmutableBijectiveDictionary();
    } // end class
} // end namespace