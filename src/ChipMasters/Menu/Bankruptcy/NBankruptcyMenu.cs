using ChipMasters.GodotWrappers;
using ChipMasters.Menu.Bankruptcy;
using ChipMasters.User;
using Godot;
using System;

namespace ChipMasters.Menu
{
    /// <summary>
    /// <see cref="NControl"/> based <see cref="IBankruptcyMenu"/> that wraps <see cref="RBankruptcyMenu"/>
    /// </summary>
	public partial class NBankruptcyMenu : NControl, IBankruptcyMenu
    {
        [Export] private Node _gameplayScreen = null!;
        [Export] private Node _buttonContainer = null!;
        [Export] private Node _continueButton = null!;
        [Export] private Node _infoLabel = null!;
        [Export] private Node _payoutLabel = null!;

        private NConfirmationDialog _confirmationDialog = new NConfirmationDialog();

        /// <inheritdoc/>
        public event Action? OnOpen;
        /// <inheritdoc/>
        public event Action? OnClose;

        private IBankruptcyMenu BankruptcyMenu => _bankruptcyMenu ?? throw new RNotReadyException(nameof(_bankruptcyMenu));

        private IBankruptcyMenu? _bankruptcyMenu;

        /// <inheritdoc/>
        public bool HideBankruptcyBox { get => _bankruptcyMenu!.HideBankruptcyBox; set => _bankruptcyMenu!.HideBankruptcyBox = value; }

        /// <inheritdoc/>
        public override void _Ready()
        {
            base._Ready();

            _bankruptcyMenu = new RBankruptcyMenu(RUser.INSTANCE, this,
                (IControl)_gameplayScreen.AssertNotNull(),
                (IContainer)_buttonContainer.AssertNotNull(),
                (IBaseButton)_continueButton.AssertNotNull(),
                (ILabel)_infoLabel.AssertNotNull(),
                (ILabel)_payoutLabel.AssertNotNull(),
                _confirmationDialog);

            _bankruptcyMenu.OnOpen += () => OnOpen?.Invoke();
            _bankruptcyMenu.OnClose += () => OnClose?.Invoke();

            _confirmationDialog.Confirmed += Open;
            _confirmationDialog.Canceled += Canceled;
            _confirmationDialog.DialogText = "You have run out of chips! Would you like to file for bankruptcy?";
            _confirmationDialog.OkButtonText = "Yes";
            _confirmationDialog.CancelButtonText = "No";
            ((NGameplayScreen)_gameplayScreen).CallDeferred("add_child", _confirmationDialog);
        } // end _Ready()

        /// <inheritdoc/>
        public void ShowBankruptcyBox() => BankruptcyMenu.ShowBankruptcyBox();

        /// <inheritdoc/>
        public void Open() => BankruptcyMenu.Open();

        /// <inheritdoc/>
        public void Close() => BankruptcyMenu.Close();

        private void Canceled()
        {
            _bankruptcyMenu!.HideBankruptcyBox = true;
            _confirmationDialog.Hide();
            ((NGameplayScreen)_gameplayScreen).ShowContinueBox();
        } // end Canceled()
    } // end class
} // end namespace
