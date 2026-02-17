using ChipMasters.Cards;
using ChipMasters.Games.Appraisers;
using ChipMasters.Games.Hands;
using GSR.EnDecic;
using GSR.EnDecic.EnDecs;
using GSR.Utilic.Generic;
using System;

namespace ChipMasters.Games.Matches
{
    /// <summary>
    /// Simple <see cref="IMatch"/> implementation.
    /// </summary>
    public sealed class RMatch : IMatch
    {
        /// <summary>
        /// <see cref="IEnDec{T}"/> that codes <see cref="RMatch"/> instances.
        /// </summary>
        /// <returns></returns>
        public static readonly IEnDec<RMatch> ENDEC = EnDecUtil.STRING.KeyedEnDecBuilder<string, RMatch>()
            .Add("player_deck", IDeck.ENDEC, (x) => x._playerDeck)
            .Add("dealer_deck", IDeck.ENDEC.NullableOfR(), (x) => ReferenceEquals(x._playerDeck, x._dealerDeck) ? null : x._dealerDeck)
            .Add("dealer_hand", IHand.ENDEC, (x) => x.DealerHand)
            .Add("player_hand", IHand.ENDEC, (x) => x.PlayerHand)
            .Add("bet", EnDecUtil.INT_32, (x) => x.Bet)
            .Add("concluded", EnDecUtil.BOOLEAN, (x) => x.IsConcluded)
            .Build((pd, dd, dh, ph, s, c) => new RMatch(pd, dd ?? pd, dh, ph, s, c));

        /// <inheritdoc/>
        public IAppraiser Appraiser { get; } = new RStandardAppraiser();

        /// <inheritdoc/>
        public IHand DealerHand { get; }

        /// <inheritdoc/>
        public IHand PlayerHand { get; }

        /// <inheritdoc/>
        public bool IsConcluded { get; private set; }

        /// <inheritdoc/>
        public int Bet { get; }

        /// <inheritdoc/>
        public event Action? OnConcluded;

        private readonly IDeck _playerDeck;
        private readonly IDeck _dealerDeck;




        /// <inheritdoc/>
        public RMatch(IDeck playerDeck, IDeck dealerDeck, int bet)
            : this(playerDeck, dealerDeck, new RHand(), new RHand(), bet, false)
        {

            DealInitialCards();
        } // end ctor

        /// <inheritdoc/>
        public RMatch(IDeck playerDeck, IDeck dealerDeck, IHand dealerHand, IHand playerHand, int bet, bool isConcluded)
        {
            _playerDeck = playerDeck.AssertNotNull();
            _dealerDeck = dealerDeck.AssertNotNull();
            DealerHand = dealerHand.AssertNotNull();
            PlayerHand = playerHand.AssertNotNull();
            Bet = bet >= 0 ? bet : throw new ArgumentException();
            IsConcluded = isConcluded;

            DealerHand.OnCardAdded += (_) => OnCardAddedToHand(DealerHand);
            PlayerHand.OnCardAdded += (_) => OnCardAddedToHand(PlayerHand);
        } // end ctor



        private void OnCardAddedToHand(IHand hand)
        {
            if (Appraiser.AppraiseHand(hand, true).State != EHandState.Neutral)
                EndGame(); // non neutral state means further play is not allowed
        } // end OnCardAddedToDealerHand()



        private void EndGame()
        {
            // unveil any veiled card(the hole card)
            if (!IsConcluded)
            {
                DealerHand.ForEvery((x) => x.Veiled = false);
                /*PlayerHand.ForEvery((x) => x.Veiled = false);*/
                IsConcluded = true;
                OnConcluded?.Invoke();
            }
        } // end EndGame()

        private void DealInitialCards()
        {
            DealCard(PlayerHand);
            DealCard(DealerHand);
            DealCard(PlayerHand);
            DealCard(DealerHand, veiled: !IsConcluded);
        } // end DealInitialCards()

        private void DealCard(IHand hand, bool veiled = false)
        {
            if (hand == PlayerHand)
            {
                ICard c = _playerDeck.Draw();
                c.Veiled = veiled;
                hand.AddCard(c);
            }
            else
            {
                ICard c = _dealerDeck.Draw();
                c.Veiled = veiled;
                hand.AddCard(c);
            }

        } // end DealCard()



        /// <inheritdoc/>
        public void Hit()
        {
            if (IsConcluded)
                throw new RGameOverException("Cannot perform action. The game is already over.");

            DealCard(PlayerHand);
        } // end Hit()

        /// <inheritdoc/>
        public void Stand()
        {
            if (IsConcluded)
                throw new RGameOverException("Cannot perform action. The game is already over.");


            DealerTurn();
        } // end Stand()



        private void DealerTurn()
        {
            while (Appraiser.AppraiseHand(DealerHand, true/*run as if hole card already unveiled*/).AssertKnown().TotalValue < 17)  // Dealer hits until they have 17 or more
                DealCard(DealerHand);

            // stops endgame from being fired twice. 
            EndGame();

        } // end DealerTurn()
    } // end class
} // end namespace
