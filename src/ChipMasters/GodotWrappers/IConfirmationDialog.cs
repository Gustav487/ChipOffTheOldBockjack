using ChipMasters.Menu;
using System;

namespace ChipMasters.GodotWrappers
{
    /// <summary>
    /// Contract parallel to a <see cref="Godot.ConfirmationDialog"/>, see also ChipMasters.GodotWrappers.<see cref="NConfirmationDialog"/>.
    /// </summary>
    public interface IConfirmationDialog : INode, IVisable
    {
        /// <summary>
        /// Refer to <see cref="Godot.AcceptDialog.DialogText"/>.
        /// </summary>
        string DialogText { get; set; }

        /// <summary>
        /// Refer to <see cref="Godot.AcceptDialog.OkButtonText"/>.
        /// </summary>
        string OkButtonText { get; set; }

        /// <summary>
        /// Refer to <see cref="Godot.ConfirmationDialog.CancelButtonText"/>.
        /// </summary>
        string CancelButtonText { get; set; }

        /// <summary>
        /// Refer to <see cref="Godot.AcceptDialog.Confirmed"/>.
        /// </summary>
        event Action Confirmed;

        /// <summary>
        /// Refer to <see cref="Godot.AcceptDialog.Canceled"/>.
        /// </summary>
        event Action Canceled;

        /// <summary>
        /// Refer to <see cref="Godot.Window.Show()"/>.
        /// </summary>
        void Show();

        /// <summary>
        /// Refer to <see cref="Godot.Window.Hide()"/>.
        /// </summary>
        void Hide();

        /// <summary>
        /// Refer to <see cref="Godot.Window.PopupCentered(Godot.Vector2I?)"/>.
        /// </summary>
        void PopupCentered(Godot.Vector2I? minsize = null);
    } // end interface
} // end namespace