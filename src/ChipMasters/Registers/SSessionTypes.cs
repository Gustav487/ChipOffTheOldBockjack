using ChipMasters.Games.Sessions;
using ChipMasters.IO;
using GSR.Utilic.Generic;
using System.Collections.Generic;

namespace ChipMasters.Registers
{
    /// <summary>
    /// Static class containing a registery of <see cref="ISession"/> <see cref="IType{T}"/>s that the game can recognize and handle.
    /// </summary>
    public static class SSessionTypes
    {
        /// <summary>
        /// One to one(Bijective) dictionary of all defined <see cref="ISession"/> <see cref="IType{T}"/>s, and their identifiers.
        /// </summary>
        public static readonly IBijectiveDictionary<string, IType<ISession>> REGISTER = new Dictionary<string, IType<ISession>>()
        {
            { "standard", new RType<ISession>(RStandardSession.ENDEC.Cast<ISession, RStandardSession>(), (x) => x.GetType() == typeof(RStandardSession)) },
        }.ToImmutableBijectiveDictionary();
    } // end class
} // end namespace