namespace ChipMasters.User
{
    /// <summary>
    /// Describes a static achievement.
    /// </summary>
    public class RAchievement : IAchievement
    {
        private readonly string _displayName;
        private readonly string _watchedStat;
        private readonly int _threshold;

        /// <summary>
        /// Initializes a new instance of the <see cref="RAchievement"/> class.
        /// </summary>
        public RAchievement(string displayName, string watchedStat, int threshold)
        {
            _displayName = displayName;
            _watchedStat = watchedStat;
            _threshold = threshold;
        }

        /// <summary>
        /// Gets the display name of the achievement.
        /// </summary>
        public string DisplayName => _displayName;

        /// <summary>
        /// Gets the stat being watched for this achievement.
        /// </summary>
        public string WatchedStat => _watchedStat;

        /// <summary>
        /// Gets the threshold value for the achievement.
        /// </summary>
        public int Threshold => _threshold;
    }// end class
}// end namespace
