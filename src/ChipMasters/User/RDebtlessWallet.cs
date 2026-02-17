using GSR.EnDecic;
using GSR.EnDecic.EnDecs;
using System;

namespace ChipMasters.User
{
    /// <summary>
    /// Simple <see cref="IWallet"/> implementation. Can become bankrupt, chips can never fall below zero.
    /// </summary>
    public sealed class RDebtlessWallet : AWallet
    {
        /// <summary>
        /// <see cref="ENDEC"/>'s key for the <see cref="AWallet.Chips"/> property.
        /// </summary>
        public const string CHIPS_KEY = "chips";

        /// <summary>
        /// <see cref="IEnDec{T}"/> for codes <see cref="RDebtlessWallet"/> instances.
        /// </summary>
        public static readonly IEnDec<RDebtlessWallet> ENDEC = EnDecUtil.KeyedEnDecBuilder<string, RDebtlessWallet>(EnDecUtil.STRING)
            .Add(CHIPS_KEY, EnDecUtil.INT_32.Ranged(0, int.MaxValue), (x) => x.Chips)
            .Build((c) => new(c));

        /// <inheritdoc/>
        public RDebtlessWallet(int startingChips = 0) : base(Math.Max(startingChips, 0)) { } // end ctor



        /// <inheritdoc/>
        protected override int ConstrainChips(int value) => Math.Max(value, 0);
    } // end class
} // end namespace