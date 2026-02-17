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
    /// <see cref="IMatch"/> where player and dealer take turns
    /// </summary>
    public sealed class RTurnedMatch : IMatch
    {
        /// <summary>
        /// <see cref="IEnDec{T}"/> that codes <see cref="RTurnedMatch"/> instances.
        /// </summary>
        /// <returns></returns>
        public static readonly IEnDec<RTurnedMatch> ENDEC = EnDecUtil.STRING.KeyedEnDecBuilder<string, RTurnedMatch>()
            .Add("player_deck", IDeck.ENDEC, (x) => x._playerDeck)
            .Add("dealer_deck", IDeck.ENDEC.NullableOfR(), (x) => ReferenceEquals(x._playerDeck, x._dealerDeck) ? null : x._dealerDeck)
            .Add("dealer_hand", IHand.ENDEC, (x) => x.DealerHand)
            .Add("player_hand", IHand.ENDEC, (x) => x.PlayerHand)
            .Add("bet", EnDecUtil.INT_32, (x) => x.Bet)
            .Add("concluded", EnDecUtil.BOOLEAN, (x) => x.IsConcluded)
            .Add("dealer_goal", EnDecUtil.INT_32, (x) => x._dealerGoal)
            .Add("appraiser", IAppraiser.ENDEC, (x) => x.Appraiser)
            .Build((pd, dd, dh, ph, s, c, dg, ap) => new RTurnedMatch(pd, dd ?? pd, dh, ph, s, c, dg, ap));



        /// <inheritdoc/>
        public IAppraiser Appraiser { get; }

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
        private readonly int _dealerGoal;



        /// <inheritdoc/>
        public RTurnedMatch(IDeck playerDeck, IDeck dealerDeck, int bet, int dealerGoal = 17, IAppraiser? appraiser = null)
            : this(playerDeck, dealerDeck, new RHand(), new RHand(), bet, false, dealerGoal, appraiser)
        {
            DealInitialCards();
        } // end ctor

        /// <inheritdoc/>
        public RTurnedMatch(IDeck playerDeck, IDeck dealerDeck, IHand dealerHand, IHand playerHand, int bet, bool isConcluded, int dealerGoal = 17, IAppraiser? appraiser = null)
        {
            _playerDeck = playerDeck.AssertNotNull();
            _dealerDeck = dealerDeck.AssertNotNull();
            DealerHand = dealerHand.AssertNotNull();
            PlayerHand = playerHand.AssertNotNull();
            Bet = bet >= 0 ? bet : throw new ArgumentException();
            IsConcluded = isConcluded;
            _dealerGoal = dealerGoal;
            Appraiser = appraiser ?? new RStandardAppraiser();

            DealerHand.OnCardAdded += (_) => OnCardAddedToHand(DealerHand);
            PlayerHand.OnCardAdded += (_) => OnCardAddedToHand(PlayerHand);
        } // end ctor



        private void OnCardAddedToHand(IHand hand)
        {
            if (Appraiser.AppraiseHand(hand, true).State != EHandState.Neutral)
                EndGame();
        } // end OnCardAddedToDealerHand()



        private void EndGame()
        {
            // unveil any veiled card(the hole card)
            if (!IsConcluded)
            {
                DealerHand.ForEvery((x) => x.Veiled = false);
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



        /// <summary>
        /// Draw a card, then transition to dealer turn.
        /// </summary>
        /// <exception cref="RGameOverException"></exception>
        public void Hit()
        {
            if (IsConcluded)
                throw new RGameOverException("Cannot perform action. The game is already over.");

            DealCard(PlayerHand);
            if (!IsConcluded)
                DealerTurn();
        } // end Hit()

        /// <summary>
        /// On stand dealer is allowed infinitely more turn, then game ends.
        /// </summary>
        /// <exception cref="RGameOverException"></exception>
        public void Stand()
        {
            if (IsConcluded)
                throw new RGameOverException("Cannot perform action. The game is already over.");

            DealerTurn(true);
            EndGame();
        } // end Stand()



        private void DealerTurn(bool playerStood = false)
        {
            if (IsConcluded)
                throw new RGameOverException("Cannot perform action. The game is already over.");

            DealerHand.ForEvery((x) => x.Veiled = false);
            while (Appraiser.AppraiseHand(DealerHand, true).AssertKnown().TotalValue < _dealerGoal)
            {
                DealCard(DealerHand);
                if (!playerStood)
                    return; // only go once unless player has stood.
            }
        } // end DealerTurn()
    } // end class
} // end namespace