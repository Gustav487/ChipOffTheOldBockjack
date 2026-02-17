using ChipMasters.Cards;
using System.Collections;
using System.Collections.Immutable;

namespace Fakes.ChipMasters.Cards
{
    public sealed class FakeDeck : IDeck
    {
        public IImmutableList<ICard> Prototype { get; } = ImmutableList<ICard>.Empty;

        private readonly IImmutableList<ICard> _proto;
        private Queue<ICard> _cards;

        public FakeDeck(params ICard[] cards)
        {
            _proto = cards.ToImmutableList();
            _cards = new Queue<ICard>(_proto);
        } // end ctor

        public ICard this[int index] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public int Count => _cards.Count;

        public bool IsReadOnly => throw new NotImplementedException();

        public bool WasShuffled { get; private set; }


        public void Add(ICard item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(ICard item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(ICard[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public ICard Draw() => _cards.Dequeue();

        public IEnumerator<ICard> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public int IndexOf(ICard item)
        {
            throw new NotImplementedException();
        }

        public void Insert(int index, ICard item)
        {
            throw new NotImplementedException();
        }

        public bool Remove(ICard item)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        public void Restore() => _cards = new Queue<ICard>(_proto);

        public void Shuffle() => WasShuffled = true;

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    } // end class
} // end namespace