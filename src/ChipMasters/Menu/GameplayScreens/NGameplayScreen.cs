using ChipMasters.Games.Sessions;
using ChipMasters.GodotWrappers;
using ChipMasters.Menu.BetSelections;
using ChipMasters.Menu.Displays;
using ChipMasters.User;
using Godot;
using System;

namespace ChipMasters.Menu
{
    /// <summary>
    /// Represents the gameplay screen in the ChipMasters game.
    /// </summary>
    public partial class NGameplayScreen : NControl
    {
        [Export] private Node _sessionDisplay = null!;
        [Export] private Node _betSelectionMenu = null!;

        [Export] private Button _bankruptcyButton = null!;

        [Export] private CanvasItem _concludeAnimiation = null!;


        private IBetSelectionMenu i_betSelectionMenu = null!;




        /// <inheritdoc/>
        public override void _Ready()
        {
            base._Ready();
            i_betSelectionMenu = (IBetSelectionMenu)_betSelectionMenu.AssertNotNull();

            _bankruptcyButton.AssertNotNull();

            _concludeAnimiation.AssertNotNull();



            ((IDisplay<ISession>)_sessionDisplay.AssertNotNull()).Display = RUser.INSTANCE.Session ?? throw new InvalidOperationException("Error: Users's session is not null. Ensure RPlayer.INSTANCE.Match is initialized properly."); ;
            RUser.INSTANCE.Session.OnMatchChanged += RefreshDisplay;
            RefreshDisplay();
        } // end _Ready

        /// <inheritdoc/>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (RUser.INSTANCE.Session is not null)
            {
                RUser.INSTANCE.Session.OnMatchChanged -= RefreshDisplay;
                RUser.INSTANCE.Session.Match.OnConcluded -= Continue;
            }
        } // end Dispose()

        private void RefreshDisplay()
        {
            // Connect buttons and displays to their respective functions
            _bankruptcyButton.Visible = RUser.INSTANCE.Wallet.IsBankrupt();

            if (RUser.INSTANCE.Session!.Match.IsConcluded)
                Continue();
            else
                RUser.INSTANCE.Session.Match.OnConcluded += Continue;
        } // end RefreshDisplay()

        /// <summary>
        /// Wait three seconds and either check for bankruptcy or
        /// prompt the user to continue the game
        /// </summary>
        private async void Continue()
        {
            if (_bankruptcyButton.Visible) _bankruptcyButton.Disabled = true;

            if (!RUser.INSTANCE.Wallet.IsBankrupt())
            {
                await ToSignal(GetTree().CreateTimer(3), "timeout");
                ShowContinueBox();
            }
        } // end Continue()

        /// <summary>
        /// Create and display the Continue Confirmation Box
        /// </summary>
        public void ShowContinueBox()
        {
            string[] hints = {
                "Hint: Did you know 90% of gamblers quit before they win it big?",
                "LETS GO GAMBLING!!!",
                "Hint: If you family ask about the missing 4000 dollars, tell them you invested it",
                "Hint: If you are in debt, bet more to get out of it faster.",
                "Hint: Card count at your own risk.",
                "Hint: 2-6 are low, 7-9 are neutral, and 10-A are high",
                "Hint: On low add 1 and on high subtract 1",
                "Hint: A count greater than 1 means the odds are in your favor"
            };

            string RandomHint = hints[new Random().Next(hints.Length)];

            var ContinueBox = new ConfirmationDialog();
            ContinueBox.DialogText = "\t\tWould you like to replay match?\n" + RandomHint;
            ContinueBox.OkButtonText = "Yes";
            ContinueBox.CancelButtonText = "No";
            ContinueBox.Exclusive = false;
            AddChild(ContinueBox);
            ContinueBox.Confirmed += PlayAgain;
            ContinueBox.Canceled += TakeUserHome;
            ContinueBox.PopupCentered();
            ContinueBox.Show();
        } // end ShowContinueBox()

        /// <summary>
        /// function that replays match and reinstantiates games objects
        /// </summary>
        private void PlayAgain()
        {
            if (_bankruptcyButton.Visible)
                _bankruptcyButton.Disabled = false;
            _concludeAnimiation.Visible = false; // set animation to stop before match is turned over. If new match is concluded, it starts animation visibile again

            int bet = Math.Min(RUser.INSTANCE.Wallet.Chips, RUser.INSTANCE.Session!.Match.Bet);
            if (i_betSelectionMenu.Range.Contains(bet))
                i_betSelectionMenu.Selected = bet;
            else
                throw new InvalidOperationException("User can't meet bet range, shouldn't be allowed to continue with palying");
            i_betSelectionMenu.Open((bet) => RUser.INSTANCE.Session!.PlayAgain(bet));
        } // end PlayAgain()

        private void TakeUserHome()
        {
            // So user can't join an already completed match
            GetTree().ChangeSceneToFile("res://scenes/main_menu.tscn");
            RUser.INSTANCE.Session = null;
        } // end TakeUserHome()
    } // end class
} // end namespace
