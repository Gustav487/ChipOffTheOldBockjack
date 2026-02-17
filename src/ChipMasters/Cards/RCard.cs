using System;

namespace ChipMasters.Cards
{
    /// <summary>
    /// Represents a standard playing card with a rank and a suit.
    /// </summary>
    public class RCard : ICard
    {
        /// <summary>
        /// The rank of the card (e.g., Two, Three, Ace, King).
        /// </summary>
        public ECardRank Rank { get; }

        /// <summary>
        /// The suit of the card (e.g., Hearts, Spades).
        /// </summary>
        public ECardSuit Suit { get; }

        /// <inheritdoc/>
        public bool Veiled
        {
            get => _veiled;
            set
            {
                if (_veiled == value)
                    return;

                _veiled = value;
                OnFlipped?.Invoke();
            }
        }
        private bool _veiled;

        /// <inheritdoc/>
        public event Action? OnFlipped;



        /// <summary>
        /// Initializes a new instance of the <see cref="RCard"/> class.
        /// </summary>
        /// <param name="rank">The rank of the card.</param>
        /// <param name="suit">The suit of the card.</param>
        /// <param name="veiled">Is the card face hidden.</param>
        public RCard(ECardRank rank, ECardSuit suit, bool veiled = false)
        {
            Rank = rank;
            Suit = suit;
            Veiled = veiled;
        } // end ctor



        /// <summary>
        /// Returns a string representation of the card.
        /// </summary>
        /// <returns>A string describing the card (e.g., "Ace of Spades").</returns>
        public override string ToString() => $"{Rank} of {Suit}";

        /// <inheritdoc/>
        public override int GetHashCode() => Tuple.Create(Rank, Suit).GetHashCode();

        /// <inheritdoc/>
        public override bool Equals(object? obj)
            => obj is ICard other
            && other.Rank == Rank
            && other.Suit == Suit; // don't consider veiledness, a card upside down or rightside up is the same card

        /// <inheritdoc/>
        public static bool operator ==(RCard a, ICard b) => a.Equals(b);

        /// <inheritdoc/>
        public static bool operator !=(RCard a, ICard b) => !(a == b);

    } // end class
} // end namespace
