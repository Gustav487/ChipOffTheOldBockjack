using ChipMasters.GodotWrappers;

namespace ChipMasters.Controls
{
    /// <summary>
    /// Extension methods for acting on Godot <see cref="Godot.Control"/>
    /// </summary>
    public static class SControlUtil
    {
        /// <summary>
        /// Set's the text of a <see cref="ITextEdit"/> while preserving the caret position.
        /// </summary>
        /// <param name="textEdit"></param>
        /// <param name="text"></param>
        public static void SetTextCP(this ITextEdit textEdit, string text)
        {
            int c = textEdit.GetCaretColumn();
            int l = textEdit.GetCaretLine();
            textEdit.Text = text;
            textEdit.SetCaretColumn(c);
            textEdit.SetCaretLine(l);
        } // end SetText()
    } // end class
} // end namespace