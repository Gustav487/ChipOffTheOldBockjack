using ChipMasters.GodotWrappers;
using Godot;

namespace ChipMasters.Menu.Interactables.Buttons.Closes
{
    /// <summary>
    /// <see cref="NButton"/> that closes a <see cref="IClosable"/>.
    /// </summary>
    public partial class NCloseButton : NButton
    {
        [Export] private Node _closes = null!;



        /// <inheritdoc/>
        public override void _Ready()
        {
            base._Ready();
            IClosable c = (IClosable)_closes.AssertNotNull();
            Pressed += c.Close;
        } // end _Ready()
    } // end class
} // end namespace