using ChipMasters.GodotWrappers;
using ChipMasters.Menu.Displays.Chips;
using ChipMasters.User;
using Godot;
using System.Threading.Tasks;

namespace ChipMasters.Menu.Displays.Wallets
{
    /// <summary>
    /// Godot <see cref="NNode"/> wrapper of <see cref="RWalletDeltaDisplay"/>.
    /// </summary>
    public partial class NWalletDeltaDisplay : NControl
    {
        [Export] private Node? _chipDisplay;

        [Export] private int _lingerPeriod = 2000;
        // [Export] private int _fadeDuration = 1000;
        // [Export] private int _fadeSteps = 10;

        private RWalletDeltaDisplay? _display;
        private Canceller? _runningFadeCanceller;



        /// <inheritdoc/>
        public override void _Ready()
        {
            base._Ready();

            _display = new(RUser.INSTANCE.Wallet, (IChipDisplay)_chipDisplay.AssertNotNull());
            RUser.INSTANCE.Wallet.OnChipsChanged += DoFadeOut;
        } // end ctor

        /// <inheritdoc/>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            _display?.Dispose();
            _runningFadeCanceller?.Cancel();
            RUser.INSTANCE.Wallet.OnChipsChanged -= DoFadeOut;
        } // end Dispose()



        private async void DoFadeOut()
        {
            _runningFadeCanceller?.Cancel();
            Canceller c = new();
            _runningFadeCanceller = c;

            Visible = true;
            await Task.Delay(_lingerPeriod);

            if (c.Cancelled)
                return;

            Visible = false;
        } // end DoFadeOut()


        private class Canceller
        {
            public bool Cancelled { get; private set; } = false;

            public void Cancel() => Cancelled = true;
        } // end inner class Canceller
    } // end class
} // end namespace
