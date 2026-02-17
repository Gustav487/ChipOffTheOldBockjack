using ChipMasters.GodotWrappers;
using ChipMasters.Menu;
using Godot;

namespace Fakes.ChipMasters.GodotWrappers
{
    public sealed class FakeConfirmationDialog : IConfirmationDialog, IVisable
    {
        public string DialogText { get; set; } = "";
        public string OkButtonText { get; set; } = "";
        public string CancelButtonText { get; set; } = "";
        public bool Visible { get; set; }

        public event Action Confirmed = null!;
        public event Action Canceled = null!;

        public void Show()
        {
            Visible = true;
        } // end Show()

        public void Hide()
        {
            Visible = false;
        } // end Hide()

        public void PopupCentered(Vector2I? minsize = null)
        {
        } // end PopupCentered()
    } // end class
} // end namespace
