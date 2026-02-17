using ChipMasters.IO;
using ChipMasters.Registers;
using GSR.EnDecic;
using System;

namespace ChipMasters.User
{
    /// <summary>
    /// Contract for an object representing ones amount of chips.
    /// </summary>
    public interface IWallet
    {
        /// <summary>
        /// <see cref="IEnDec{T}"/> for coding cards associated with <see cref="IWallet"/> <see cref="IType{T}"/>s in the <see cref="SWalletTypes.REGISTER"/>.
        /// </summary>
        public static readonly IEnDec<IWallet> ENDEC = SWalletTypes.REGISTER.TypeRegistryEnDec();

        /// <summary>
        /// Number of chips currently held.
        /// </summary>
        int Chips { get; set; }

        /// <summary>
        /// Event raised when chip count is changed.
        /// </summary>
        event Action? OnChipsChanged;
    } // end class
} // end namespace