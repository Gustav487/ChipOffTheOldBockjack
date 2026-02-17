using ChipMasters.GodotWrappers;
using System;

namespace ChipMasters.Menu.Displays.Chips
{
    /// <summary>
    /// <see cref="IChipDisplay"/> that displays an abbreviation of the total number.
    /// </summary>
    public class RAbbreviatedChipDisplay : AChipDisplay
    {
        /// <summary>
        /// Text displayed when <see cref="AChipDisplay.Chips"/>'s null.
        /// </summary>
        public const string UNDEFINED = ALabelDisplay<object>.UNDEFINED;
        private readonly ILabel _label;



        /// <inheritdoc/>
        public RAbbreviatedChipDisplay(ILabel label)
        {
            _label = label.AssertNotNull();
            _label.MouseFilter = Godot.Control.MouseFilterEnum.Pass;
            RefreshDisplay();
        } // end ctor



        /// <inheritdoc/>
        protected override void RefreshDisplay()
        {
            if (Chips is null)
            {
                _label.Text = UNDEFINED;
                _label.TooltipText = UNDEFINED;
                return;
            }

            int chips = Chips ?? throw new InvalidOperationException();
            _label.TooltipText = chips.ToString();

            if (chips >= -999 && chips <= 999)
                _label.Text = chips.ToString();

            else if (chips >= -9_999 && chips <= 9_999)
                Decimalize(chips, 100, 'k');
            else if (chips >= -999_999 && chips <= 999_999)
                Scale(chips, 1_000, 'k');

            else if (chips >= -9_999_999 && chips <= 9_999_999)
                Decimalize(chips, 100_000, 'm');
            else if (chips >= -999_999_999 && chips <= 999_999_999)
                Scale(chips, 1_000_000, 'm');

            else
                Decimalize(chips, 100_000_000, 'b');

            if (ExplicitSign && chips > 0)
            {
                _label.Text = $"+{_label.Text}";
                _label.TooltipText = $"+{_label.TooltipText}";
            }
        } // end UpdateDisplay()



        private void Scale(int chips, int scale, char symbol)
        {
            int s = chips / scale;
            _label.Text = $"{s}{symbol}";
        } // end Scale()

        private void Decimalize(int chips, int splitPoint, char symbol)
        {
            int s = chips / splitPoint;
            int l = s % 10;
            int h = s / 10;
            _label.Text =
                l != 0
                ? $"{h}.{Math.Abs(l)}{symbol}"
                : $"{h}{symbol}";
        } // end Factionalize()

    } // end class
} // end namespace