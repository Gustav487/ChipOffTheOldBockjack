namespace ChipMasters.User
{
    /// <summary>
    /// Contains constant definitions for stat names used throughout the achievement and metrics system.
    /// </summary>
    public static class SStats
    {
        /// <summary>
        /// Stat key for the total number of games played.
        /// </summary>
        public const string GAMES_PLAYED = "games_played";

        /// <summary>
        /// Stat key for the number of hands won.
        /// </summary>
        public const string WINS = "wins";
        /// <summary>
        /// Stat key for number of hands tied.
        /// </summary>
        public const string TIES = "ties";
        /// <summary>
        /// Stat key for number of hands lost.
        /// </summary>
        public const string LOSSES = "losses";

        /// <summary>
        /// Stat key for number of blackjacks attained.
        /// </summary>
        public const string BLACKJACKS = "blackjacks";
        /// <summary>
        /// Stat key for number of busts attained.
        /// </summary>
        public const string BUSTS = "busts";

        /// <summary>
        /// Stat key for the number of wins where exactly four cards were held.
        /// </summary>
        public const string FOUR_CARD_WINS = "4_card_wins";
        /// <summary>
        /// Stat key for the number of wins where exactly five cards were held.
        /// </summary>
        public const string FIVE_CARD_WINS = "5_card_wins";
        /// <summary>
        /// Stat key for the number of wins where exactly six cards were held.
        /// </summary>
        public const string SIX_CARD_WINS = "6_card_wins";
        /// <summary>
        /// Stat key for the number of wins where the hands value was exactly seventeen.
        /// </summary>
        public const string STAND_SEVENTEEN_WINS = "17_stand_wins";
        /// <summary>
        /// Stat key for the all time sum of all bets placed and concluded.
        /// </summary>
        public const string BETTED_AMOUNT = "betted_amount";
        /// <summary>
        /// Stat key for the number of matches forfeit.
        /// </summary>
        public const string FORFEITS = "forfeits";
        /// <summary>
        /// Stat key for the number of bets placed that were over 5000. (not including those that're forfeit?)
        /// </summary>
        public const string HIGH_BETS = "high_bets";
        /// <summary>
        /// Stat key for the highest amount of chips held.
        /// </summary>
        public const string MAX_BALANCE = "max_balance";
        /// <summary>
        /// Stat key to be used when a stat isn't truly tracked.
        /// </summary>
        public const string NULL = "_";
    } // end class
} // end namespace