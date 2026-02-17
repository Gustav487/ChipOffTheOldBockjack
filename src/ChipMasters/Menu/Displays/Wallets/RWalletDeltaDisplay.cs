using ChipMasters.Menu.Displays.Chips;
using ChipMasters.User;

namespace ChipMasters.Menu.Displays.Wallets
{
    /// <summary>
    /// Display logic for showing changes in a <see cref="IUser"/>'s chip count.
    /// </summary>
    public sealed class RWalletDeltaDisplay
    {
        private readonly IWallet _wallet;
        private readonly IChipDisplay _display;
        private int _last;



        /// <inheritdoc/>
        public RWalletDeltaDisplay(IWallet wallet, IChipDisplay display)
        {
            _wallet = wallet.AssertNotNull();
            _display = display.AssertNotNull();
            _last = _wallet.Chips;

            _wallet.OnChipsChanged += RefreshDisplay;
        } // end ctor

        /// <inheritdoc/>
        public void Dispose() => _wallet.OnChipsChanged -= RefreshDisplay;



        private void RefreshDisplay()
        {
            int delta = _wallet.Chips - _last;
            _last = _wallet.Chips;
            _display.Chips = delta;
        } // end RefreshDisplay()

    } // end class
} // end namespace