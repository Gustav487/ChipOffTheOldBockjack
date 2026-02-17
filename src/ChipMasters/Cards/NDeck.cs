using ChipMasters.GodotWrappers;
using ChipMasters.Registers;
using Godot;
using Godot.Collections;
using GSR.EnDecic;
using GSR.EnDecic.EnDecs;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace ChipMasters.Cards
{
    /// <summary>
    /// Represents a deck of cards integrated with Godot.
    /// Delegates all functionality to an IDeck instance.
    /// </summary>
    public partial class NDeck : NNode, IDeck
    {
        /// <summary>
        /// <see cref="IEnDec{T}"/> for <see cref="NDeck"/>.
        /// </summary>
        /// <param name="cardEnDec"></param>
        /// <returns></returns>
        public static IEnDec<NDeck> ENDEC(IEnDec<ICard> cardEnDec) =>
            RDeck.ENDEC(cardEnDec)
            .Map((x) => x.Deck, (x) => new NDeck(x));



        [Export] private Array<string>? _cardsByType;



        /// <inheritdoc/>
        public IImmutableList<ICard> Prototype => Deck.Prototype;

        private RDeck Deck => _deck ?? throw new RNotReadyException($"{nameof(NDeck)}.{nameof(_deck)}");
        private RDeck? _deck;



        /// <inheritdoc/>
        public NDeck(RDeck deck)
        {
            _deck = deck.AssertNotNull();
        } // end ctor

        /// <inheritdoc/>
        public NDeck() { } // end ctor



        /// <inheritdoc/>
        public override void _Ready()
        {
            base._Ready();
            if (_deck is null)
                _deck = new RDeck(//_cards.AssertNotNull().Select((x) => x.AssertNotNull()).Cast<ICard>());
                    _cardsByType
                    .AssertNotNull()
                    .Select((x) => SCardTypes.REGISTER[x].Instantiate()));
        } // end _Ready()





        /// <inheritdoc/>
        public void Shuffle() => Deck.Shuffle();
        /// <inheritdoc/>
        public ICard Draw() => Deck.Draw();
        /// <inheritdoc/>
        public void Restore() => Deck.Restore();



        // IList<ICard> Implementation
        /// <inheritdoc/>
        public int Count => Deck.Count;
        /// <inheritdoc/>
        public bool IsReadOnly => Deck.IsReadOnly;


        /// <inheritdoc/>
        public void Add(ICard card) => Deck.Add(card);
        /// <inheritdoc/>
        public void Clear() => Deck.Clear();
        /// <inheritdoc/>
        public bool Contains(ICard card) => Deck.Contains(card);
        /// <inheritdoc/>
        public void CopyTo(ICard[] array, int arrayIndex) => Deck.CopyTo(array, arrayIndex);
        /// <inheritdoc/>
        public bool Remove(ICard card) => Deck.Remove(card);
        /// <inheritdoc/>
        public IEnumerator<ICard> GetEnumerator() => Deck.GetEnumerator();
        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        /// <inheritdoc/>
        public int IndexOf(ICard card) => Deck.IndexOf(card);
        /// <inheritdoc/>
        public void Insert(int index, ICard card) => Deck.Insert(index, card);
        /// <inheritdoc/>
        public void RemoveAt(int index) => Deck.RemoveAt(index);
        /// <inheritdoc/>
        public ICard this[int index] { get => Deck[index]; set => Deck[index] = value; }
    } // end class
} // end namespace
