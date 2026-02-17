using ChipMasters.GodotWrappers;
using ChipMasters.Menu.Displays.Chips;
using ChipMasters.User;
using System;

namespace ChipMasters.Menu.Displays.Wallets
{
    /// <summary>
    /// Class which safely maintains a <see cref="ILabel"/> with an up to date <see cref="IWallet.Chips"/> count.
    /// </summary>
    public sealed class RWalletDisplay : IDisposable
    {
        private readonly IWallet _wallet;
        private readonly IChipDisplay _display;



        /// <inheritdoc/>
        public RWalletDisplay(IWallet wallet, IChipDisplay display)
        {
            _wallet = wallet.AssertNotNull();
            _display = display.AssertNotNull();

            RefreshDisplay();
            _wallet.OnChipsChanged += RefreshDisplay;
        } // end ctor

        /// <inheritdoc/>
        public void Dispose() => _wallet.OnChipsChanged -= RefreshDisplay;



        private void RefreshDisplay() => _display.Chips = _wallet.Chips;

    } // end class
} // end namespace