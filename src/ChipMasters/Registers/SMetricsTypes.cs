using ChipMasters.IO;
using ChipMasters.User;
using GSR.Utilic.Generic;
using System.Collections.Generic;

namespace ChipMasters.Registers
{
    /// <summary>
    /// Static class containing a registery of <see cref="IMetrics"/> <see cref="IType{T}"/>s that the game can recognize and handle.
    /// </summary>
    public static class SMetricsTypes
    {
        /// <summary>
        /// One to one(Bijective) dictionary of all defined <see cref="IWallet"/> <see cref="IType{T}"/>s, and their identifiers.
        /// </summary>
        public static readonly IBijectiveDictionary<string, IType<IMetrics>> REGISTER = new Dictionary<string, IType<IMetrics>>()
        {
            { "user", new RType<IMetrics>(new RLazyConstantEnDec<IMetrics>(() => RUser.INSTANCE.Metrics), () => RUser.INSTANCE.Metrics, (x) => true, (x) => ReferenceEquals(x, RUser.INSTANCE.Metrics)) }
        }.ToImmutableBijectiveDictionary();

    } // end class
} // end namespace