using ChipMasters.Controls;
using ChipMasters.Games.BetHandlers;
using ChipMasters.GodotWrappers;
using ChipMasters.Menu.Displays.Chips;
using ChipMasters.User;
using System;

namespace ChipMasters.Menu.BetSelections
{
    /// <summary>
    /// Simple <see cref="IBetSelectionMenu"/> implementation.
    /// </summary>
    public sealed class RBetSelectionMenu : IBetSelectionMenu
    {
        /// <inheritdoc/>
        public VBetRange Range
        {
            get => _maxRange;
            set
            {
                _maxRange = value;
                UpdateRanging();

                if (_maxRange.Min <= _better.Chips)
                    Selected = _maxRange.Min;
            }
        } // end Range

        /// <inheritdoc/>
        public int Selected
        {
            get => _selected;
            set
            {
                _selected = value;
                if (!_range.Contains(value))
                    throw new ArgumentOutOfRangeException();

                _silence = true; // prevent looping when updating texts. Text events trigger selection processing, which triggers this. This breaks loop.
                _betEdit.SetTextCP(_selected.ToString());
                _betSlider.Value = _selected;
                _silence = false;
            }
        } // end Selected



        private readonly IWallet _better;
        private readonly IChipDisplay _minDisplay;
        private readonly IChipDisplay _maxDisplay;
        private readonly ITextEdit _betEdit;
        private readonly IRange _betSlider;

        private Action<int>? _submitCallback;
        private VBetRange _maxRange = new(0, int.MaxValue);
        private VBetRange _range = new(0, int.MaxValue);
        private int _selected;
        private bool _silence;


        /// <inheritdoc/>
        public RBetSelectionMenu(IWallet better, IChipDisplay minDisplay, IChipDisplay maxDisplay, ITextEdit betEdit, IRange betSlider, IBaseButton submitButton)
        {
            _better = better.AssertNotNull();
            _minDisplay = minDisplay.AssertNotNull();
            _maxDisplay = maxDisplay.AssertNotNull();
            _betEdit = betEdit.AssertNotNull();
            _betSlider = betSlider.AssertNotNull();

            submitButton.AssertNotNull().Pressed += HandleSubmit;
            _better.OnChipsChanged += UpdateRanging;

            _betEdit.TextChanged += ProcessBetTextChange;
            _betSlider.ValueChanged += ProcessBetSlideChange;

            Selected = _range.Min;
            UpdateRanging();
        } // end _Ready()

        /// <inheritdoc/>
        public void Dispose()
        {
            _better.OnChipsChanged -= UpdateRanging;
        } // end Dispose()



        /// <inheritdoc/>
        public void Open(Action<int> submitCallback)
        {
            _submitCallback = submitCallback;
        } // end Open()

        /// <inheritdoc/>
        public void Close()
        {
            _submitCallback = null;
        } // end Close()



        private void HandleSubmit()
        {
            _submitCallback?.Invoke(Selected);
            Close();
        } // end HandleSubmit()



        private void UpdateRanging()
        {
            int min = _maxRange.Min;
            int max = Math.Max(min, Math.Min(_maxRange.Max, _better.Chips));
            _range = new(min, max);

            if (min > _better.Chips)
            {
                _minDisplay.Chips = null; // indicate insufficient funds
                _maxDisplay.Chips = null;
                return;
            }

            _minDisplay.Chips = min;
            _maxDisplay.Chips = max;

            _betSlider.MinValue = min;
            _betSlider.MaxValue = max;
        } // end UpdateLabels()

        private void ProcessBetTextChange()
        {
            if (_silence)
                return;

            int value;
            if (!int.TryParse(_betEdit.Text, out value))
            { }

            if (_range.Contains(value))
                Selected = value;
            else if (value < _maxRange.Min)
                Selected = _range.Min; // clamp to range
            else
                Selected = _range.Max; // clamp to range 

        } // end ProcessTextChange()

        private void ProcessBetSlideChange(double _)
        {
            if (_silence)
                return;

            Selected = (int)_betSlider.Value;
        } // end ProcessBetSlideChange()

    } // end class
} // end namespace
