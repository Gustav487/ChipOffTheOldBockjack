
using ChipMasters.Cards;

namespace Fakes.ChipMasters.Cards
{
    public sealed class FakeCard : ICard
    {
        public ECardSuit Suit { get; }

        public ECardRank Rank { get; }

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



        public FakeCard(ECardRank rank = ECardRank.Two, ECardSuit suit = ECardSuit.Clubs, bool veiled = false)
        {
            Rank = rank;
            Suit = suit;
            Veiled = veiled;
        } // end ctor



        public event Action? OnFlipped;

        public void Flip()
            => OnFlipped?.Invoke();
    } // end class
} // end namespace