using ChipMasters.GodotWrappers;
using ChipMasters.User;
using Godot;

namespace ChipMasters.Menu.Interactables.Buttons.Sessions
{
    /// <summary>
    /// Button that handles forfeit the current session.
    /// </summary>
    public partial class NForfeitButton : NButton
    {
        [Export] private string? _scenePath;



        /// <inheritdoc/>
        public override void _Ready()
        {
            base._Ready();
            Pressed += ShowPopup;
        } // end _Ready()



        /// <summary>
        /// Prompts the user asking if they are sure. 
        /// </summary>
        private void ShowPopup()
        {
            // Hiding pause options
            // creating and displaying dialog box
            var ForfeitPrompt = new ConfirmationDialog();

            ForfeitPrompt.Confirmed += Forfeit;
            ForfeitPrompt.DialogText = "Are you sure? This will remove match data and take away the chips you have as a bet.";

            AddChild(ForfeitPrompt);
            ForfeitPrompt.PopupCentered();
            ForfeitPrompt.Show();
        } // end ShowPopup()

        /// <summary>
        ///  Takes user to homepage and user loses chips.
        /// </summary>
        private void Forfeit()
        {
            RUser.INSTANCE.Metrics.IncrementStat(SStats.FORFEITS);
            RUser.INSTANCE.Session = null;
            GetTree().ChangeSceneToFile(_scenePath);
        }// end Forfeit()

    } // end class
} // end namespace