using ChipMasters.GodotWrappers;
using Godot;

namespace ChipMasters.Menu.Interactables.Buttons.Bankruptcy
{
    /// <summary>
    /// Class handling the bankruptcy button in the Gameplay Scene.
    /// </summary>
    public partial class NBankruptcyButton : NButton
    {
        [Export] private NBankruptcyMenu _bankruptcyMenu = null!;

        /// <inheritdoc/>
        public override void _Ready()
        {
            base._Ready();
            _bankruptcyMenu.AssertNotNull();
            Pressed += OpenMenu;
        } // end _Ready()

        /// <summary>
        /// Hide this button and open bankruptcy menu.
        /// </summary>
        private void OpenMenu()
        {
            Visible = false;
            _bankruptcyMenu.Open();
        }
    }
}
