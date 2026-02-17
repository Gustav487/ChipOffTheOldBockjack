using ChipMasters.GodotWrappers;
using Godot;

namespace ChipMasters.Menu
{
    /// <summary>
    /// Handle Main Menu Exit button logic
    /// </summary>
    public partial class NMainMenuExitGame : NNode
    {
        [Export] private NBaseButton _exitButton = null!;
        private ConfirmationDialog _confirmDialog = null!;



        /// <inheritdoc/>
        public override void _Ready()
        {
            _confirmDialog = new ConfirmationDialog
            {
                DialogText = "Are you sure you would like to exit the game?",
                OkButtonText = "Yes",
                CancelButtonText = "No"
            };

            _confirmDialog.Confirmed += OnYesButtonPresseed;
            _confirmDialog.Canceled += OnNoButtonPressed;
            AddChild(_confirmDialog);

            _exitButton.AssertNotNull().Pressed += OnExitButtonPressed;
        } // end _Ready()

        /// <summary>
        /// Display the dialog box when the exit button is pressed
        /// </summary>
        public void OnExitButtonPressed()
        {
            _confirmDialog.PopupCentered();
            _confirmDialog.Show();
        } // end OnExitButtonPressed()

        // Exit the game when the Yes button is pressed
        private void OnYesButtonPresseed()
        {
            GetTree().Root.PropagateNotification((int)NotificationWMCloseRequest);
        } // end OnYesButtonPresseed()

        // Hide the dialog box when the No button is pressed
        private void OnNoButtonPressed()
        {
            _confirmDialog.Hide();
        } // end OnNoButtonPressed()
    } // end class
} // end namespace
