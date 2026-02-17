namespace ChipMasters.User
{
    /// <summary>
    /// Class containing extension methods for <see cref="IWallet"/>
    /// </summary>
    public static class SIWalletExtensions
    {
        /// <inheritdoc/>
        public const int DEFAULT_CHIPS = 50;

        /// <summary>
        /// Returns true if chips are less than or equal to zero.
        /// </summary>
        /// <param name="wallet">The user's wallet.</param>
        /// <returns></returns>
        public static bool IsBankrupt(this IWallet wallet)
        {
            return wallet.Chips <= 0;
        }

        /// <summary>
        /// Adds <see cref="DEFAULT_CHIPS"/> to the wallet's chip count,
        /// thus resetting the wallet to its default state.
        /// </summary>
        /// <param name="wallet">The user's wallet.</param>
        public static void GiveBankruptcyPayout(this IWallet wallet)
        {
            wallet.Chips += DEFAULT_CHIPS;
        }
    }
}
