using ChipMasters.GodotWrappers;
using Godot;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace ChipMasters.Controls
{
    /// <summary>
    /// <see cref="NTextEdit"/> constrained to a <see cref="Regex"/>.
    /// </summary>
    public partial class NRegexTextEdit : NTextEdit
    {
        [Export] private string? _regex;
        [Export] private bool _allMatches = false;



        /// <inheritdoc/>
        public override void _Ready()
        {
            base._Ready();
            NRegexTextEdit.Constrain(this, _regex.AssertNotNull(), _allMatches);
        } // end _Ready()



        /// <summary>
        /// Constaint the <see cref="ITextEdit"/> <paramref name="textEdit"/> to always abide the <paramref name="regex"/>.
        /// </summary>
        /// <param name="textEdit"></param>
        /// <param name="regex"></param>
        /// <param name="allMatches"></param>
        public static void Constrain(ITextEdit textEdit, string regex, bool allMatches)
        {
            Regex r = new(regex.AssertNotNull());

            Action a = () => ProcessText(textEdit, r, allMatches);
            a();
            textEdit.TextChanged += a;
        } // end Constrain()

        private static void ProcessText(ITextEdit textEdit, Regex regex, bool allMatches)
        {
            string value;
            if (allMatches)
            {
                MatchCollection m = regex.Matches(textEdit.Text);
                if (m.Count == 0)
                    throw new InvalidOperationException();

                value = m.Aggregate(string.Empty, (seed, x) => seed + x);
            }
            else
            {
                System.Text.RegularExpressions.Match m = regex.Match(textEdit.Text);
                if (!m.Success)
                    throw new InvalidOperationException();

                value = m.Value;
            }
            textEdit.SetTextCP(value);
        } // end ProcessText()

    } // end class
} // end namespace