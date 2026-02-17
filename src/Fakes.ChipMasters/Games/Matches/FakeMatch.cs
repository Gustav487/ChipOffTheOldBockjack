using ChipMasters.Cards;
using ChipMasters.Games.Appraisers;
using ChipMasters.Games.Hands;
using ChipMasters.Games.Matches;
using Fakes.ChipMasters.Games.Hands;

namespace Fakes.ChipMasters.Games.Matches
{
    public class FakeMatch : IMatch
    {
        public int _HitPresses_ { get; private set; }
        public int _StandPresses_ { get; private set; }



        public IDeck Deck => new RDeck();

        public IHand DealerHand { get; set; } = new FakeHand();

        public IHand PlayerHand { get; } = new RHand();

        public int Bet { get; }

        public bool IsConcluded { get; private set; }

        public IAppraiser Appraiser => _appraiser ?? throw new InvalidOperationException();
        private readonly IAppraiser? _appraiser;

        public event Action? OnConcluded;



        public FakeMatch(bool isConcluded = false, int bet = 0, IAppraiser? appraiser = null)
        {
            IsConcluded = isConcluded;
            Bet = bet;
            _appraiser = appraiser;
        } // end ctor



        public void Conclude()
        {
            IsConcluded = true;
            OnConcluded?.Invoke();
        }

        public void Hit() => _HitPresses_++;

        public void Stand() => _StandPresses_++;
    } // end class
} // end namespace
