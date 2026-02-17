using ChipMasters.Games.Hands;

namespace ChipMasters.Games.Appraisers
{
    /// <summary>
    /// Enum of possible <see cref="IHand"/> states.
    /// </summary>
    public enum EHandState
    {
        /// <summary>
        /// Hand is fine.
        /// </summary>
        Neutral,
        /// <summary>
        /// Hand's a blackjack, the perfect hand.
        /// </summary>
        Blackjack,
        /// <summary>
        /// Hand exceeds the value limit.
        /// </summary>
        Bust,
        /// <summary>
        /// Not all values are known.
        /// </summary>
        Unknown
    } // end enum
} // end namespace