using ChipMasters.GodotWrappers;
using Godot;

namespace ChipMasters.Menu
{
    /// <summary>
    /// Godot <see cref="Button"/> node that changes the <see cref="CanvasItem.Visible"/> property on a <see cref="CanvasItem"/> to a given value when clicked.
    /// </summary>
    public partial class NSetVisibilityButton : NButton
    {
        [Export] private CanvasItem? _controlToSet;
        [Export] private bool _setTo = true;


        /// <inheritdoc/>
        public override void _Ready()
        {
            base._Ready();

            _controlToSet.AssertNotNull();
            Pressed += () => _controlToSet!.Visible = _setTo;
        } // end _Ready()

    } // end class
} // end namespace
