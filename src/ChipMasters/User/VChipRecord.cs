using ChipMasters.IO;
using GSR.EnDecic;
using GSR.EnDecic.EnDecs;
using System;

namespace ChipMasters.User
{
    /// <summary>
    /// Record of a chip amount at a given point in time.
    /// </summary>
    /// <param name="Timestamp">The time of the given data point.</param>
    /// <param name="Count">The amount of chips held.</param>
    /// <param name="Delta">The change from the prior data point, null if none.</param>
    public readonly record struct VChipRecord(DateTime Timestamp, int Count, int? Delta)
    {
        /// <summary>
        /// <see cref="IEnDec{T}"/> for encoding and decoding <see cref="VChipRecord"/> instances.
        /// </summary>
        public static readonly IEnDec<VChipRecord> ENDEC = EnDecUtil.KeyedEnDecBuilder<string, VChipRecord>(EnDecUtil.STRING)
            .Add("timestamp", SIOUtil.DATE_TIME_ENDEC, (x) => x.Timestamp)
            .Add("count", EnDecUtil.INT_32, (x) => x.Count)
            .Add("delta", EnDecUtil.INT_32.NullableOfV(), (x) => x.Delta)
            .Build((t, c, d) => new VChipRecord(t, c, d));

    } // end class
} // end namespace