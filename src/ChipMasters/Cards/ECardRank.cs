namespace ChipMasters.Cards
{
    /// <summary>
    /// Represents the rank of a playing card in a standard deck.
    /// The numerical values are assigned to make card comparisons easier.
    /// </summary>
    public enum ECardRank
    {
        // Ace can be high or low in some games, defaulting to 14 here (Can be changed to 1 or 11 for Blackjack depending on the rules).
        /// <inheritdoc/>
        Ace = 1,
        // Number cards (assigned their face values)
        /// <inheritdoc/>
        Two = 2,
        /// <inheritdoc/>
        Three = 3,
        /// <inheritdoc/>
        Four,
        /// <inheritdoc/>
        Five,
        /// <inheritdoc/>
        Six,
        /// <inheritdoc/>
        Seven,
        /// <inheritdoc/>
        Eight,
        /// <inheritdoc/>
        Nine,
        /// <inheritdoc/>
        Ten,

        // Face cards (Can be changed to 10 for Blackjack depending on the rules)
        /// <inheritdoc/>
        Jack = 11,
        /// <inheritdoc/>
        King = 12,
        /// <inheritdoc/>
        Queen = 13
    } // end class
} // end namespace