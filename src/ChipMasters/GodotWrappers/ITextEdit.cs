using System;

namespace ChipMasters.GodotWrappers
{
    /// <summary>
    /// Contract parallel to a <see cref="Godot.TextEdit"/>, see also ChipMasters.GodotWrappers.<see cref="NTextEdit"/>.
    /// </summary>
    public interface ITextEdit
    {
        /// <summary>
        /// Refer to <see cref="Godot.TextEdit.Text"/>.
        /// </summary>
        string Text { get; set; }

        /// <summary>
        /// Refer to <see cref="Godot.TextEdit.TextChanged"/>.
        /// </summary>
        event Action? TextChanged;



        /// <summary>
        /// Refer to <see cref="Godot.TextEdit.GetCaretColumn(int)"/>.
        /// </summary>
        int GetCaretColumn(int caretIndex = 0);

        /// <summary>
        /// Refer to <see cref="Godot.TextEdit.GetCaretLine(int)"/>.
        /// </summary>
        int GetCaretLine(int caretIndex = 0);

        /// <summary>
        /// Refer to <see cref="Godot.TextEdit.SetCaretColumn(int, bool, int)"/>.
        /// </summary>
        void SetCaretColumn(int column, bool adjustViewport = true, int caretIndex = 0);

        /// <summary>
        /// Refer to <see cref="Godot.TextEdit.SetCaretLine(int, bool, bool, int, int)"/>.
        /// </summary>
        void SetCaretLine(int line, bool adjustViewport = true, bool canBeHidden = true, int wrapIndex = 0, int caretIndex = 0);

    } // end interface
} // end namespace