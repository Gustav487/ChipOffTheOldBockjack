using GSR.EnDecic;
using GSR.EnDecic.EnDecs;

namespace ChipMasters.User
{
    /// <summary>
    /// Simple <see cref="IWallet"/> implementation. Can't have debt, can reach 0.
    /// </summary>
    public sealed class RWallet : AWallet
    {
        /// <summary>
        /// <see cref="ENDEC"/>'s key for the <see cref="AWallet.Chips"/> property.
        /// </summary>
        public const string CHIPS_KEY = "chips";

        /// <summary>
        /// <see cref="IEnDec{T}"/> for codes <see cref="RWallet"/> instances.
        /// </summary>
        public static readonly IEnDec<RWallet> ENDEC = EnDecUtil.KeyedEnDecBuilder<string, RWallet>(EnDecUtil.STRING)
            .Add(CHIPS_KEY, EnDecUtil.INT_32, (x) => x.Chips)
            .Build((c) => new(c));

        /// <inheritdoc/>
        public RWallet(int startingChips = 0) : base(startingChips) { } // end ctor



        /// <inheritdoc/>
        protected override int ConstrainChips(int value) => value;
    } // end class
} // end namespace