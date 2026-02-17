using ChipMasters.Games.Hands;
using ChipMasters.IO;
using GSR.EnDecic;
using GSR.EnDecic.EnDecs;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace ChipMasters.Games.Appraisers
{
    /// <summary>
    /// Value of hand within a given set of rules.
    /// </summary>
    public struct VHandAppraisal
    {
        /// <summary>
        /// <see cref="IEnDec{T}"/> for coding <see cref="VHandAppraisal"/> instances.
        /// </summary>
        public static readonly IEnDec<VHandAppraisal> ENDEC = EnDecUtil.STRING.KeyedEnDecBuilder<string, VHandAppraisal>()
            .Add("values", EnDecUtil.INT_32.ListOf(), (x) => x.Values.ToList())
            .Add("state", new REnumEnDec<EHandState>(), (x) => x.State)
            .Build((v, s) => new VHandAppraisal(v.ToImmutableList(), s));



        /// <summary>
        /// Per card values.
        /// </summary>
        public IReadOnlyList<int> Values { get; }

        /// <summary>
        /// The <see cref="EHandState"/> of the <see cref="IHand"/>.
        /// </summary>
        public EHandState State { get; }

        /// <summary>
        /// Calculate the total value for the hand.
        /// </summary>
        public int TotalValue => _totalValue.Value;
        private readonly Lazy<int> _totalValue;



        /// <summary>
        /// Do not use.
        /// </summary>
        /// <exception cref="InvalidOperationException"></exception>
        public VHandAppraisal()
        {
            throw new InvalidOperationException("Use parameterized constructor.");
        } // end ctor

        /// <inheritdoc/>
        public VHandAppraisal(IEnumerable<int> values, EHandState state)
        {
            IReadOnlyList<int> vs = values.ToImmutableList();
            Values = vs;
            State = state;
            _totalValue = new(() => vs.Aggregate(0, (seed, x) => seed + x));
        } // end ctor



        /// <inheritdoc/>
        public override string ToString() => State == EHandState.Neutral
            ? TotalValue.ToString()
            : State.ToString();

        /// <inheritdoc/>
        public override int GetHashCode() => Tuple.Create(Values, State).GetHashCode();

        /// <summary>
        /// Are hands equal in value. i.e. a tie.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals([NotNullWhen(true)] object? obj)
        {
            if (obj is not VHandAppraisal other)
                return false;

            if (State == EHandState.Unknown || other.State == EHandState.Unknown)
                return false;

            if (State != other.State)
                return false;

            if (State == EHandState.Neutral)
                return TotalValue == other.TotalValue;

            return true;
        } // end Equals()             

        /// <summary>
        /// Are operands tied.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator ==(VHandAppraisal a, VHandAppraisal b) => a.Equals(b);

        /// <summary>
        /// Are operands not at tie.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator !=(VHandAppraisal a, VHandAppraisal b) => !(a == b);

        /// <summary>
        /// Was right operand the winner.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator >(VHandAppraisal a, VHandAppraisal b)
        {
            if (a == b)
                return false;

            if (a.State == EHandState.Unknown || b.State == EHandState.Unknown)
                return false;

            if (a.State == EHandState.Blackjack) // blackjack, and not equal, was the winner
                return true;
            if (a.State == EHandState.Bust) // bust, and not equal, was the loser
                return false;

            if (b.State == EHandState.Blackjack) // blackjack, and not equal, was the winner
                return false;
            if (b.State == EHandState.Bust) // bust, and not equal, was the loser
                return true;

            return a.TotalValue > b.TotalValue;
        } // end >

        /// <summary>
        /// Is left hand the winner.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator <(VHandAppraisal a, VHandAppraisal b)
        {
            if (a.State == EHandState.Unknown || b.State == EHandState.Unknown)
                return false;

            return a != b && !(a > b);
        } // end <

    } // end record struct
} // end namespace