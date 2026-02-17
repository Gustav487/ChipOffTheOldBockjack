using ChipMasters.GodotWrappers;

namespace Fakes.ChipMasters.GodotWrappers
{
    public sealed class FakeTextEdit : ITextEdit
    {
        public string Text
        {
            get => _text;
            set
            {
                if (_text == value)
                    return;

                _text = value;
                _caretColumn = 0;
                _caretLine = 0;
                TextChanged?.Invoke();
            }
        }
        private string _text = "";

        public event Action? TextChanged;

        private int _caretColumn = 0;
        private int _caretLine = 0;



        public FakeTextEdit(string text = "")
        {
            Text = text;
        } // end ctor




        public int GetCaretColumn(int caretIndex = 0) => _caretColumn;

        public int GetCaretLine(int caretIndex = 0) => _caretLine;

        public void SetCaretColumn(int column, bool adjustViewport = true, int caretIndex = 0) => _caretColumn = column;

        public void SetCaretLine(int line, bool adjustViewport = true, bool canBeHidden = true, int wrapIndex = 0, int caretIndex = 0) => _caretLine = line;
    } // end class
} // end namespace