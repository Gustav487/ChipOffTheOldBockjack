using ChipMasters.GodotWrappers;
using ChipMasters.Items;
using ChipMasters.User;
using System;
using System.Collections.Immutable;
using System.Linq;

namespace ChipMasters.Menu.Bankruptcy
{
    /// <summary>
    /// Simple <see cref="IBankruptcyMenu"/> implementation.
    /// </summary>
    public sealed class RBankruptcyMenu : IBankruptcyMenu
    {
        private readonly IUser _user;
        private readonly IBankruptcyMenu _bankruptcyMenu;
        private readonly IControl _gameplayScreen;
        private readonly IContainer _buttonContainer;
        private readonly IBaseButton _continueButton;
        private readonly ILabel _infoLabel;
        private readonly ILabel _payoutLabel;
        private readonly IConfirmationDialog _confirmationDialog;

        /// <inheritdoc/>
        public bool Visible { get; set; }

        /// <inheritdoc/>
        public event Action? OnOpen;
        /// <inheritdoc/>
        public event Action? OnClose;

        /// <inheritdoc/>
        public bool HideBankruptcyBox { get; set; }

        /// <inheritdoc/>
        public RBankruptcyMenu(IUser user, IBankruptcyMenu bankruptcyMenu, IControl gameplayScreen, IContainer buttonContainer, IBaseButton continueButton, ILabel infoLabel, ILabel payoutLabel, IConfirmationDialog confirmationDialog)
        {
            _user = user;
            _bankruptcyMenu = bankruptcyMenu;
            _gameplayScreen = gameplayScreen;
            _buttonContainer = buttonContainer;
            _continueButton = continueButton;
            _infoLabel = infoLabel;
            _payoutLabel = payoutLabel;
            _confirmationDialog = confirmationDialog;

            _continueButton.AssertNotNull().ButtonDown += Close;

            HideBankruptcyBox = false;
        } // end ctor

        /// <inheritdoc/>
        public void ShowBankruptcyBox()
        {
            if (!HideBankruptcyBox)
            {
                _confirmationDialog.PopupCentered();
                _confirmationDialog.Show();
            }

            else
                Close();
        } // end ShowBankruptcyBox()

        /// <inheritdoc/>
        public void Open()
        {
            _bankruptcyMenu.Visible = true;
            _infoLabel.Visible = true;
            _payoutLabel.Visible = false;
            _continueButton.Visible = false;
            ImmutableList<IItem> purchasedItems = _user.Inventory.Items.Where(i => i.Price > 0).ToImmutableList();

            // Give bankruptcy payout
            if (purchasedItems.Count <= 0)
            {
                _infoLabel.Visible = false;
                _payoutLabel.Visible = true;
                _continueButton.Visible = true;
                _user.Wallet.GiveBankruptcyPayout();
            }

            // Create NBankruptcyItemButtons
#if !TESTING
            else
            {
                foreach(IItem _item in purchasedItems)
                {
                    Interactables.Buttons.Bankruptcy.NBankruptcyItemButton button = new Interactables.Buttons.Bankruptcy.NBankruptcyItemButton
                    {
                        item = _item,
                        Text = $"{_item.Name} - {_item.GetRefundPrice()} Chips"
                    };

                    button.Pressed += () => _continueButton.Visible = true;
                    ((Godot.Node)_buttonContainer).AddChild(button);
                }
            }
#endif

            OnOpen?.Invoke();
        } // end Open()

        /// <inheritdoc/>
        public void Close()
        {
            _bankruptcyMenu.Visible = false;
#if !TESTING
            if(_user.Session!.Match.IsConcluded)
                ((NGameplayScreen)_gameplayScreen).ShowContinueBox();
#endif
            OnClose?.Invoke();
        } // end Close()
    } // end class
} // end namespace