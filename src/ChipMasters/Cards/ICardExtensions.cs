namespace ChipMasters.Cards
{
    /// <summary>
    /// Extensions for <see cref="ICard"/>s.
    /// </summary>
    public static class ICardExtensions
    {
        /// <summary>
        /// Create identical but separate card.
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static ICard Clone(this ICard c) => new RCard(c.Rank, c.Suit, c.Veiled);
    } // end class
} // end namespace