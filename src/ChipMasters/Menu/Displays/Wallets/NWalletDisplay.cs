using ChipMasters.GodotWrappers;
using ChipMasters.Menu.Displays.Chips;
using ChipMasters.User;
using Godot;

namespace ChipMasters.Menu.Displays.Wallets
{
    /// <summary>
    /// <see cref="NNode"/> that's safely maintained up to date with a <see cref="IUser.Wallet"/>'s <see cref="IWallet.Chips"/> count.
    /// </summary>
    public partial class NWalletDisplay : NNode
    {
        [Export] private Node? _chipDisplay;

        private RWalletDisplay? _display;



        /// <inheritdoc/>
        public override void _Ready()
        {
            base._Ready();
            _display = new(RUser.INSTANCE.Wallet, (IChipDisplay)_chipDisplay.AssertNotNull());
        } // end _ready()

        /// <inheritdoc/>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            _display?.Dispose();
        } // end Dispose()

    } // end class
} // end namespace
