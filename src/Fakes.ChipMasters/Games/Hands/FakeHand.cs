using ChipMasters.Cards;
using ChipMasters.Games.Hands;
using System.Collections;

namespace Fakes.ChipMasters.Games.Hands
{
    public sealed class FakeHand : IHand
    {
        public IReadOnlyList<ICard> Cards => _cards;
        public int Count => throw new NotImplementedException();
        public ICard this[int index] => throw new NotImplementedException();
        public event Action<ICard>? OnCardAdded;



        private readonly List<ICard> _cards;


        public FakeHand(params ICard[] cards)
            : this((IEnumerable<ICard>)cards) { } // end ctor

        public FakeHand(IEnumerable<ICard> cards)
        {
            _cards = new List<ICard>(cards);
        } // end ctor



        public void AddCard(ICard card)
        {
            _cards.Add(card);
            OnCardAdded?.Invoke(card);
        }

        public IEnumerator<ICard> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    } // end class
} // end namespace