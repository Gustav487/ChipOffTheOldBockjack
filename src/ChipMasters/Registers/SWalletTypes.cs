using ChipMasters.IO;
using ChipMasters.User;
using GSR.Utilic.Generic;
using System;
using System.Collections.Generic;

namespace ChipMasters.Registers
{
    /// <summary>
    /// Static class containing a registery of <see cref="IWallet"/> <see cref="IType{T}"/>s that the game can recognize and handle.
    /// </summary>
    public static class SWalletTypes
    {
        /// <summary>
        /// Left key of register that the user's wallet'll be associated with.
        /// </summary>
        public const string USER_KEY = "user";



        /// <summary>
        /// One to one(Bijective) dictionary of all defined <see cref="IWallet"/> <see cref="IType{T}"/>s, and their identifiers.
        /// </summary>
        public static readonly IBijectiveDictionary<string, IType<IWallet>> REGISTER = REGISTER_WITH(() => RUser.INSTANCE.Wallet);

        /// <summary>
        /// Create the register with a given user's wallet associated with <see cref="USER_KEY"/>.
        /// </summary>
        /// <param name="userWalletSupplier"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public static IBijectiveDictionary<string, IType<IWallet>> REGISTER_WITH(Func<IWallet> userWalletSupplier)
        {
            bool Is(IWallet x, Type t) => !IsUsers(x) && x.GetType() == t;
            bool IsUsers(IWallet x) => ReferenceEquals(x, userWalletSupplier());

            return new Dictionary<string, IType<IWallet>>()
                {
                    // not strictly ordered, thus user type won't necessarily check first despite needing logical priority.
                    { USER_KEY, new RType<IWallet>(new RLazyConstantEnDec<IWallet>(new Lazy<IWallet>(userWalletSupplier)), userWalletSupplier, (x) => true, IsUsers) },
                    { "infinite", new RType<IWallet>(new RConstantEnDec<IWallet>(new RInfiniteWallet()), () => new RInfiniteWallet(), (x) => true, (x) => Is(x, typeof(RInfiniteWallet))) },
                    { "standard", new RType<IWallet>(RWallet.ENDEC.Cast<IWallet, RWallet>(), () => new RWallet(), (x) => x.Chips == 0, (x) => Is(x, typeof(RWallet))) },
                    { "bankruptable", new RType<IWallet>(RBankruptableWallet.ENDEC.Cast<IWallet, RBankruptableWallet>(), (x) => Is(x, typeof(RBankruptableWallet))) },
                    { "debtless", new RType<IWallet>(RDebtlessWallet.ENDEC.Cast<IWallet, RDebtlessWallet>(), (x) => Is(x, typeof(RDebtlessWallet))) }
                }.ToImmutableBijectiveDictionary();
        } // end REGISTER_WITH()

    } // end class
} // end namespace