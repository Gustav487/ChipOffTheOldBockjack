using ChipMasters.Games.BetHandlers;
using ChipMasters.IO;
using GSR.Utilic.Generic;
using System.Collections.Generic;

namespace ChipMasters.Registers
{
    /// <summary>
    /// Static class containing a registery of <see cref="IBetHandler"/> <see cref="IType{T}"/>s that the game can recognize and handle.
    /// </summary>
    public static class SBetHandlerTypes
    {
        /// <summary>
        /// One to one(Bijective) dictionary of all defined <see cref="IBetHandler"/> <see cref="IType{T}"/>s, and their identifiers.
        /// </summary>
        public static readonly IBijectiveDictionary<string, IType<IBetHandler>> REGISTER = new Dictionary<string, IType<IBetHandler>>()
        {
            { "standard", new RType<IBetHandler>(RStandardBetHandler.ENDEC.Cast<IBetHandler, RStandardBetHandler>(), (x) => x.GetType() == typeof(RStandardBetHandler)) },
            { "n_standard", new RType<IBetHandler>(NStandardBetHandler.ENDEC, (x) => x.GetType() == typeof(NStandardBetHandler)) },
        }.ToImmutableBijectiveDictionary();

    } // end class
} // end namespace
