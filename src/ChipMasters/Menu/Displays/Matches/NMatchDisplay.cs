using ChipMasters.Games.Matches;
using ChipMasters.GodotWrappers;
using ChipMasters.Menu.Displays.Chips;
using ChipMasters.Menu.Displays.Hand;
using ChipMasters.Menu.Displays.Hand.Appraisal;
using Godot;

namespace ChipMasters.Menu.Displays.Matches
{
    /// <summary>
    /// <see cref="IMatch"/> <see cref="ANDisplay{T}"/> for displaying hands, hand appraisals, bet, and button interaction.
    /// </summary>
    public sealed partial class NMatchDisplay : ANDisplay<IMatch>
    {
        [Export] private Node _dealerHandDisplay = null!;
        [Export] private Node _playerHandDisplay = null!;

        [Export] private Node _dealerHandAppraisalDisplay = null!;
        [Export] private Node _playerHandAppraisalDisplay = null!;

        [Export] private Node _betDisplay = null!;

        [Export] private Button _standButton = null!;
        [Export] private Button _hitButton = null!;



        /// <inheritdoc/>
        protected override IDisplay<IMatch> ConstructInner()
            => new RMatchDisplay(
                dealerHandDisplay: (IHandDisplay)_dealerHandDisplay.AssertNotNull(), playerHandDisplay: (IHandDisplay)_playerHandDisplay.AssertNotNull(),
                dealerHandAppraisalDisplay: (IHandAppraisalDisplay)_dealerHandAppraisalDisplay.AssertNotNull(), playerHandAppraisalDisplay: (IHandAppraisalDisplay)_playerHandAppraisalDisplay.AssertNotNull(),
                betDisplay: (IChipDisplay)_betDisplay.AssertNotNull(),
                standButton: (IButton)_standButton.AssertNotNull(), hitButton: (IButton)_hitButton.AssertNotNull());
    } // end class
} // end namespace
