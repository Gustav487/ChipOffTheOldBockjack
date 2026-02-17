namespace ChipMasters.User
{
    /// <summary>
    /// Extension methods for <see cref="IMetrics"/>.
    /// </summary>
    public static class SIMetricExtensions
    {
        /// <summary>
        /// Increase the value of given a stat by 1.
        /// </summary>
        /// <param name="m"></param>
        /// <param name="stat"></param>
        public static void IncrementStat(this IMetrics m, string stat) => m.SetStat(stat, m.GetStat(stat) + 1);

        /// <summary>
        /// Increase the value of given a stat by an amount <paramref name="amount"/>.
        /// </summary>
        /// <param name="m"></param>
        /// <param name="stat"></param>
        /// <param name="amount"></param>
        public static void IncreaseStat(this IMetrics m, string stat, int amount) => m.SetStat(stat, m.GetStat(stat) + amount);
    } // end class
} // end namespace