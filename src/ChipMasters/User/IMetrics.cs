using ChipMasters.Games.Matches;
using ChipMasters.IO;
using ChipMasters.Registers;
using GSR.EnDecic;
using GSR.EnDecic.EnDecs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ChipMasters.User
{
    /// <summary>
    /// Contract for an object storing a <see cref="IUser"/>s metrics.
    /// </summary>
    public interface IMetrics
    {
        /// <summary>
        /// <see cref="IEnDec{T}"/> for coding cards associated with <see cref="IMetrics"/> <see cref="IType{T}"/>s in the <see cref="SMetricsTypes.REGISTER"/>.
        /// </summary>
        public static readonly IEnDec<IMetrics> REGISTRY_ENDEC = SMetricsTypes.REGISTER.TypeRegistryEnDec();

        /// <summary>
        /// <see cref="IEnDec{T}"/> for <see cref="IMetrics"/>s. Returns standard implmentation, only encodes data in contract.
        /// </summary>
        public static readonly IEnDec<IMetrics> CONTRACT_ENDEC = EnDecUtil.KeyedEnDecBuilder<string, IMetrics>(EnDecUtil.STRING)
            .Add("match_history", VMatchRecord.ENDEC.ListOf(), (x) => x.MatchHistory.ToList())
            .Add("chip_history", VChipRecord.ENDEC.ListOf(), (x) => x.ChipHistory.ToList())
            .Add("stats", EnDecUtil.Map(EnDecUtil.STRING, EnDecUtil.INT_32.Ranged(0, int.MaxValue)), (x) => new Dictionary<string, int>(x.Stats))
            .Build((h, c, s) => new RMetrics(h, c, s));

        /// <summary>
        /// Record of recent matches played.
        /// </summary>
        public IReadOnlyList<VMatchRecord> MatchHistory { get; }

        /// <summary>
        /// User's win:tie:loss ratio.
        /// </summary>
        public VWinRatio WinRatio { get; }

        /// <summary>
        /// Log a <see cref="IMatch"/>, placing it both in the recent history, and in the aggregated statistics.
        /// </summary>
        /// <param name="match"></param>
        public void RecordMatch(IMatch match);

        /// <summary>
        /// Record of chip counts over time.
        /// </summary>
        IReadOnlyList<VChipRecord> ChipHistory { get; }

        /// <summary>
        /// Record the number of chips to the <see cref="ChipHistory"/>.
        /// </summary>
        /// <param name="chipCount"></param>
        void RecordChipCount(int chipCount);



#warning could easily bundle these following four into a single IStatTracker contract. Then we could naturally use an indexer as well.
        /// <summary>
        /// Each stat recorded and it's associated value. See also <see cref="GetStat(string)"/>, <see cref="SetStat(string, int)"/>, and <see cref="OnStatChanged"/>.
        /// </summary>
        IReadOnlyDictionary<string, int> Stats { get; }

        /// <summary>
        /// Event raised when a stat's value changes.
        /// </summary>
        event Action? OnStatChanged;

        /// <summary>
        /// Gets the current value for the specified stat.
        /// Implementations should return values for stats such as "games_played", "hands_won", "chip_count", etc.
        /// </summary>
        /// <param name="stat">The name of the stat to retrieve.</param>
        /// <returns>The current value of the specified stat, 0 if undefined.</returns>
        int GetStat(string stat);

        /// <summary>
        /// Set the value of a given state.
        /// </summary>
        /// <param name="stat"></param>
        /// <param name="value"></param>
        void SetStat(string stat, int value);

    } // end class
} // end namespace