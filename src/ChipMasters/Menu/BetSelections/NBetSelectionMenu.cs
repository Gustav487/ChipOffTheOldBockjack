using ChipMasters.Games.BetHandlers;
using ChipMasters.GodotWrappers;
using ChipMasters.Menu.BetSelections;
using ChipMasters.Menu.Displays.Chips;
using ChipMasters.User;
using Godot;
using System;

namespace ChipMasters.Menu
{
    /// <summary>
    /// Menu for selecting a bet.
    /// </summary>
    public partial class NBetSelectionMenu : NControl, IBetSelectionMenu
    {
        [Export] private Node? _minDisplay;
        [Export] private Node? _maxDisplay;
        [Export] private Node? _betEdit;
        [Export] private Node? _betSlider;
        [Export] private Node? _submitButton;

        /// <inheritdoc/>
        public VBetRange Range
        {
            get => BetSelectionMenu.Range;
            set => BetSelectionMenu.Range = value;
        } // end Range

        /// <inheritdoc/>
        public int Selected { get => BetSelectionMenu.Selected; set => BetSelectionMenu.Selected = value; }



        private IBetSelectionMenu BetSelectionMenu => _betSelectionMenu ?? throw new RNotReadyException(nameof(_betSelectionMenu));
        private IBetSelectionMenu? _betSelectionMenu;



        /// <inheritdoc/>
        public override void _Ready()
        {
            base._Ready();
            IBaseButton submitBtn = (IBaseButton)_submitButton.AssertNotNull();
            _betSelectionMenu = new RBetSelectionMenu(RUser.INSTANCE.Wallet,
                (IChipDisplay)_minDisplay.AssertNotNull(), (IChipDisplay)_maxDisplay.AssertNotNull(),
                (ITextEdit)_betEdit.AssertNotNull(), (IRange)_betSlider.AssertNotNull(),
                submitBtn);
            submitBtn.Pressed += Close;
        } // end _Ready()

        /// <inheritdoc/>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            BetSelectionMenu.Dispose();
        } // end Dispose()



        /// <inheritdoc/>
        public void Open(Action<int> submitCallback)
        {
            BetSelectionMenu.Open(submitCallback);
            Visible = true;
        } // end Open()

        /// <inheritdoc/>
        public void Close()
        {
            BetSelectionMenu.Close();
            Visible = false;
        } // end Close()
    } // end class
} // end namespace
