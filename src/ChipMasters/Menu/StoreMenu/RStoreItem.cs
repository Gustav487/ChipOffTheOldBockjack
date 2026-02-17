using ChipMasters.GodotWrappers;
using ChipMasters.Items;
using ChipMasters.Registers;
using ChipMasters.User;
using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ChipMasters.Menu.StoreMenu
{
    /// <summary>
    /// Simple <see cref="IStoreItem"/> implementation.
    /// </summary>
    public sealed class RStoreItem : IStoreItem
    {
        private readonly IUser _user;
        private readonly IStoreMenu _storeMenu;
        private readonly ILabel _itemLabel;
        private readonly IButton _purchaseButton;
        private readonly IButton _sellButton;
        private readonly IBaseButton _previousItemButton;
        private readonly IBaseButton _nextItemButton;
        private readonly IBaseButton _selectButton;
        private readonly ICanvasItem _itemPreview;

        /// <inheritdoc/>
        public int TabIndex { get; }

        /// <inheritdoc/>
        public IItem Item
        {
            get => _item;
            set
            {
                _item = value;
                IsCurrentIndex(() => Refresh());
            }
        } // end Item
        private IItem _item = null!;

        /// <inheritdoc/>
        public event Action? OnPurchase;
        /// <inheritdoc/>
        public event Action? OnSell;
        /// <inheritdoc/>
        public event Action? OnCycle;
        /// <inheritdoc/>
        public event Action? OnSelect;

        private ConfirmationDialog _confirmDialogPurchasing = null!;
        private ConfirmationDialog _confirmDialogSelling = null!;

        private readonly Dictionary<EItemCategory, int> _cycleItemIndices = new(
            Enum.GetValues<EItemCategory>()
            .Select((x) => KeyValuePair.Create(x, 0)));

        /// <inheritdoc/>
        public RStoreItem(int tabIndex, IUser user, IItem item, IStoreMenu storeMenu, ILabel itemLabel, IButton purchaseButton, IButton sellButton, IBaseButton previousItemButton, IBaseButton nextItemButton, IBaseButton selectButton, ICanvasItem itemPreview)
        {
            TabIndex = tabIndex;
            _user = user;
            _item = item;
            _storeMenu = storeMenu;
            _itemLabel = itemLabel;
            _purchaseButton = purchaseButton;
            _sellButton = sellButton;
            _previousItemButton = previousItemButton;
            _nextItemButton = nextItemButton;
            _selectButton = selectButton;
            _itemPreview = itemPreview;

#if !TESTING
			_confirmDialogPurchasing = new ConfirmationDialog { DialogText = "Do you really want to purchase this item?" };
			_confirmDialogPurchasing.Confirmed += () => IsCurrentIndex(() => Purchase(_item));
			((NStoreMenu)storeMenu).AddChild(_confirmDialogPurchasing);

			_confirmDialogSelling = new ConfirmationDialog { DialogText = "Do you really want to sell this item?" };
			_confirmDialogSelling.Confirmed += () => IsCurrentIndex(() => Sell(_item));
			((NStoreMenu)storeMenu).AddChild(_confirmDialogSelling);
#endif

            _purchaseButton.AssertNotNull().Pressed += () => IsCurrentIndex(() => _confirmDialogPurchasing.PopupCentered());
            _sellButton.AssertNotNull().Pressed += () => IsCurrentIndex(() => _confirmDialogSelling.PopupCentered());
            _previousItemButton.AssertNotNull().Pressed += () => IsCurrentIndex(() => Cycle(true));
            _nextItemButton.AssertNotNull().Pressed += () => IsCurrentIndex(() => Cycle(false));
            _selectButton.AssertNotNull().Pressed += () => IsCurrentIndex(() => Select(_item));

            IsCurrentIndex(() => Refresh());
        } // end ctor

        /// <summary>
        /// Checks if TabIndex correlates with the store menu's current tab.
        /// </summary>
        /// <param name="action">Perform the aciton.</param>
        private void IsCurrentIndex(Action action)
        {
            if (TabIndex == _storeMenu.CurrentTab + 1)
                action();
        } // end IsCurrentIndex()

        /// <inheritdoc/>
        public void Purchase(IItem item)
        {
            _user.Purchase(item);
            IsCurrentIndex(() => Refresh());
            OnPurchase?.Invoke();
        } // end Purchase()

        /// <inheritdoc/>
        public void Sell(IItem item)
        {
            _user.Sell(item);
            IsCurrentIndex(() => Refresh());
            OnSell?.Invoke();
        } // end Sell()

        /// <inheritdoc/>
        public void Cycle(bool isPrevious)
        {
            EItemCategory currentCategory = _storeMenu.GetCategoryForTab(_storeMenu.CurrentTab);
            List<IItem> itemList = SItems.REGISTER.GetAllItems().Where(i => i.Category == currentCategory).ToList();

            if (itemList.Count == 0)
                return;

            if (!_cycleItemIndices.ContainsKey(currentCategory))
                _cycleItemIndices[currentCategory] = 0;

            int currentIndex = _cycleItemIndices[currentCategory];
            currentIndex = (currentIndex + (isPrevious ? -1 : 1) + itemList.Count) % itemList.Count;
            _cycleItemIndices[currentCategory] = currentIndex;
            _item = itemList[currentIndex];
            IsCurrentIndex(() => Refresh());
            OnCycle?.Invoke();
        } // end Cycle()

        /// <inheritdoc/>
        public void Select(IItem item)
        {
            ((IApplicableItem)item).Apply(_user);
            IsCurrentIndex(() => Refresh());
            OnSelect?.Invoke();
        } // end Select()

        /// <inheritdoc/>
        public void Refresh()
        {
            IsCurrentIndex(() =>
            {
                _itemLabel.Text = _item.AssertNotNull().Name;
#if !TESTING
                _itemPreview.Material = Resources.SAssetUtil.GetPreviewMaterials(Item).Material2D;
#endif
                _purchaseButton.Text = $"Buy for {_item.Price} Chips";
                _sellButton.Text = $"Sell for {_item.GetRefundPrice()} Chips";

                bool isPurchased = _user.Inventory.Items.Contains(Item);
                _purchaseButton.Disabled = isPurchased || _user.Wallet.Chips < Item.Price;
                _sellButton.Disabled = !isPurchased;
                _purchaseButton.Visible = Item.Price > 0;
                _sellButton.Visible = Item.Price > 0;

                if (Item is IApplicableItem apItem)
                {
                    _selectButton.Visible = true;
                    _selectButton.Disabled = !isPurchased || apItem.IsApplied(_user);
                }

                else
                    _selectButton.Visible = false;
            });
        } // end Refresh()
    } // end class
} // end namespace
