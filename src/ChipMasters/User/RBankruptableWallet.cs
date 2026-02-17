using GSR.EnDecic;
using GSR.EnDecic.EnDecs;
using System;

namespace ChipMasters.User
{
    /// <summary>
    /// <see cref="IWallet"/> which receives an amount of funds if chip count would reach 0 or below
    /// </summary>
    public class RBankruptableWallet : AWallet
    {
        /// <summary>
        /// <see cref="ENDEC"/>'s key for the <see cref="AWallet.Chips"/> property.
        /// </summary>
        public const string CHIPS_KEY = "chips";

        /// <summary>
        /// <see cref="ENDEC"/>'s key for the <see cref="RBankruptableWallet._bankruptcyChips"/> field.
        /// </summary>
        public const string BANKRUPTCY_PAY_KEY = "bankruptcy_pay";

        /// <summary>
        /// <see cref="IEnDec{T}"/> for codes <see cref="RBankruptableWallet"/> instances.
        /// </summary>
        public static readonly IEnDec<RBankruptableWallet> ENDEC = EnDecUtil.KeyedEnDecBuilder<string, RBankruptableWallet>(EnDecUtil.STRING)
            .Add(CHIPS_KEY, EnDecUtil.INT_32, (x) => x.Chips)
            .Add(BANKRUPTCY_PAY_KEY, EnDecUtil.INT_32, (x) => x._bankruptcyChips)
            .Build((c, b) => new(c, b));

        private readonly int _bankruptcyChips;



        /// <inheritdoc/>
        public RBankruptableWallet(int startingChips, int bankrupcyChips) : base(startingChips)
        {
            if (bankrupcyChips <= 0)
                throw new ArgumentOutOfRangeException();

            _bankruptcyChips = bankrupcyChips;
            if (Chips <= 0)
                Chips = _bankruptcyChips;
        } // end ctor



        /// <inheritdoc/>
        protected override int ConstrainChips(int value)
        {
            if (value <= 0)
                return _bankruptcyChips;

            return value;
        } // end ConstrainChips()
    } // end class
} // end namespace