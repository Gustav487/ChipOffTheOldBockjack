using ChipMasters.Games.Matches;
using ChipMasters.GodotWrappers;
using ChipMasters.Menu.Displays.Chips;
using ChipMasters.Menu.Displays.Hand;
using ChipMasters.Menu.Displays.Hand.Appraisal;

namespace ChipMasters.Menu.Displays.Matches
{
    /// <summary>
    /// Simple <see cref="IMatch"/> <see cref="IDisplay{T}"/> implementation.
    /// </summary>
    public sealed class RMatchDisplay : IDisplay<IMatch>
    {
        /// <inheritdoc/>
        public IMatch? Display
        {
            get => _displaying;
            set
            {
                if (_displaying == value)
                    return;

                Detach();
                _displaying = value;
                Attach();
                RefreshDisplay();
            }
        } // end Match
        private IMatch? _displaying;

        private readonly IHandDisplay _dealerHandDisplay;
        private readonly IHandDisplay _playerHandDisplay;

        private readonly IHandAppraisalDisplay _dealerAppraisalDisplay;
        private readonly IHandAppraisalDisplay _playerAppraisalDisplay;

        private readonly IChipDisplay _betDisplay;

        private readonly IButton _standButton;
        private readonly IButton _hitButton;




        /// <inheritdoc/>
        public RMatchDisplay(
            IHandDisplay dealerHandDisplay, IHandDisplay playerHandDisplay,
            IHandAppraisalDisplay dealerHandAppraisalDisplay, IHandAppraisalDisplay playerHandAppraisalDisplay,
            IChipDisplay betDisplay,
            IButton standButton, IButton hitButton)
        {
            _dealerHandDisplay = dealerHandDisplay.AssertNotNull();
            _playerHandDisplay = playerHandDisplay.AssertNotNull();
            _dealerAppraisalDisplay = dealerHandAppraisalDisplay.AssertNotNull();
            _playerAppraisalDisplay = playerHandAppraisalDisplay.AssertNotNull();
            _betDisplay = betDisplay.AssertNotNull();
            _standButton = standButton.AssertNotNull();
            _hitButton = hitButton.AssertNotNull();
            DisableButtons();
        } // end ctor

        /// <inheritdoc/>
        public void Dispose()
        {
            if (_displaying is not null)
                _displaying.OnConcluded -= DisableButtons;
        } // end Dispose()



        private void RefreshDisplay()
        {
            _dealerHandDisplay.Hand = _displaying?.DealerHand;
            _playerHandDisplay.Hand = _displaying?.PlayerHand;

            _dealerAppraisalDisplay.Hand = _displaying?.DealerHand;
            _dealerAppraisalDisplay.Appraiser = _displaying?.Appraiser;
            _playerAppraisalDisplay.Hand = _displaying?.PlayerHand;
            _playerAppraisalDisplay.Appraiser = _displaying?.Appraiser;

            _betDisplay.Chips = _displaying?.Bet;

            if (_displaying is null || _displaying.IsConcluded)
                DisableButtons();
            else
            {
                EnableButtons();
                _displaying.OnConcluded += DisableButtons;
            }
        } // end RefreshDisplay()

        private void Attach()
        {
            if (_displaying is null)
                return;

            _standButton.Pressed += _displaying.Stand;
            _hitButton.Pressed += _displaying.Hit;
        } // end Attach()

        private void Detach()
        {
            if (_displaying is null)
                return;

            _standButton.Pressed -= _displaying.Stand;
            _hitButton.Pressed -= _displaying.Hit;
        } // end Detach()

        private void DisableButtons()
        {
            _standButton.Disabled = true;
            _hitButton.Disabled = true;
        } // end DisableButtons()

        private void EnableButtons()
        {
            _standButton.Disabled = false;
            _hitButton.Disabled = false;
        } // end EnableButtons()

    } // end class
} // end namespace