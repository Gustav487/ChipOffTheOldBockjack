using ChipMasters.Cards;
using ChipMasters.Games.Hands;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace ChipMasters.Games.Appraisers
{
    /// <summary>
    /// Abstract <see cref="IAppraiser"/> that considers card by their typical evaluation, but allows the definition of "blackjack" to be altered.
    /// </summary>
    public abstract class ASubstandardAppraiser : IAppraiser
    {
        /// <summary>
        /// Max hand value.
        /// </summary>
        protected readonly int _maxValue;



        /// <inheritdoc/>
        public ASubstandardAppraiser(int maxValue)
        {
            _maxValue = maxValue;
        } // end ctor



        /// <inheritdoc/>
        public VHandAppraisal AppraiseHand(IHand hand, bool includeHidden)
        {
            ImmutableList<ICard> toAp = hand.Where((x) => includeHidden || x.Veiled == false).ToImmutableList();
            Tuple<int[], int> ap = Appraise(toAp);

            return new VHandAppraisal(
                ap.Item1,
                StateOf(ap.Item2, hand.Count, toAp.Count != hand.Count));
        } // end AppraiseHand()



        /// <summary>
        /// Is the hand a blackjack based on current appraisal.
        /// </summary>
        /// <param name="total"></param>
        /// <param name="cardCount"></param>
        /// <returns></returns>
        protected abstract bool IsBlackjack(int total, int cardCount);

        /// <summary>
        /// Determine hand state based on hand appraisal.
        /// </summary>
        /// <param name="total"></param>
        /// <param name="cardCount"></param>
        /// <param name="unknown"></param>
        /// <returns></returns>
        private EHandState StateOf(int total, int cardCount, bool unknown)
        {
            if (unknown)
                return EHandState.Unknown;

            if (total > _maxValue)
                return EHandState.Bust;
            else if (IsBlackjack(total, cardCount))
                return EHandState.Blackjack;

            return EHandState.Neutral;
        } // end StateOf()

        private Tuple<int[], int> Appraise(IList<ICard> hand)
        {
            int value = 0;
            int[] values = new int[hand.Count];

            for (int i = 0; i < hand.Count; i++)
            {
                int v = ValueOf(hand[i].Rank);
                value += v;
                values[i] = v;
            }

            // Adjust for aces if value exceeds 21
            if (value > _maxValue)
                for (int i = 0; i < hand.Count; i++)
                {
                    if (hand[i].Rank != ECardRank.Ace)
                        continue;

                    value -= 10;
                    values[i] -= 10;

                    if (value <= _maxValue)
                        break;
                }

            return Tuple.Create(values, value);
        } // end Appraise()



        private static int ValueOf(ECardRank rank) => rank switch
        {
            ECardRank.Two => 2,
            ECardRank.Three => 3,
            ECardRank.Four => 4,
            ECardRank.Five => 5,
            ECardRank.Six => 6,
            ECardRank.Seven => 7,
            ECardRank.Eight => 8,
            ECardRank.Nine => 9,
            ECardRank.Ten => 10,
            ECardRank.Jack => 10,
            ECardRank.Queen => 10,
            ECardRank.King => 10,
            ECardRank.Ace => 11,
            _ => throw new InvalidOperationException($"Value out of range for {nameof(ECardRank)}: {rank}, {(int)rank}")
        };

    } // end class
} // end namespace