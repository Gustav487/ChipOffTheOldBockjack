using ChipMasters.GodotWrappers;
using ChipMasters.Items;
using ChipMasters.User;
using Godot;
using System;

namespace ChipMasters.Menu.StoreMenu
{
    /// <summary>
    /// Container for displaying an item in the store.
    /// </summary>
    public partial class NStoreItem : NContainer, IStoreItem
    {
        /// <inheritdoc/>
        public Node _storeMenu = null!;
        /// <inheritdoc/>
        public Node _itemLabel = null!;
        /// <inheritdoc/>
        public Node _purchaseButton = null!;
        /// <inheritdoc/>
        public Node _sellButton = null!;
        /// <inheritdoc/>
        public Node _previousItemButton = null!;
        /// <inheritdoc/>
        public Node _nextItemButton = null!;
        /// <inheritdoc/>
        public Node _selectButton = null!;
        /// <inheritdoc/>
        public CanvasItem _itemPreview = null!;

        /// <inheritdoc/>
        public int TabIndex { get; }

        /// <inheritdoc/>
        public IItem Item { get; set; } = null!;

        /// <inheritdoc/>
        public event Action? OnPurchase;
        /// <inheritdoc/>
        public event Action? OnSell;
        /// <inheritdoc/>
        public event Action? OnCycle;
        /// <inheritdoc/>
        public event Action? OnSelect;

        private IStoreItem StoreItem => _storeItem ?? throw new RNotReadyException(nameof(_storeItem));
        private IStoreItem? _storeItem;

        /// <inheritdoc/>
        public override void _Ready()
        {
            base._Ready();

            _storeItem = new RStoreItem(System.Threading.Interlocked.Increment(ref RStoreMenu.TAB_REF), RUser.INSTANCE, Item,
                (IStoreMenu)_storeMenu.AssertNotNull(),
                (ILabel)_itemLabel.AssertNotNull(),
                (IButton)_purchaseButton.AssertNotNull(),
                (IButton)_sellButton.AssertNotNull(),
                (IBaseButton)_previousItemButton.AssertNotNull(),
                (IBaseButton)_nextItemButton.AssertNotNull(),
                (IBaseButton)_selectButton.AssertNotNull(),
                (ICanvasItem)_itemPreview.AssertNotNull());

            _storeItem.OnPurchase += () => OnPurchase?.Invoke();
            _storeItem.OnSell += () => OnSell?.Invoke();
            _storeItem.OnCycle += () => OnCycle?.Invoke();
            _storeItem.OnSelect += () => OnSelect?.Invoke();
        } // end _Ready()

        /// <inheritdoc/>
        public void Purchase(IItem item) => StoreItem.Purchase(item);

        /// <inheritdoc/>
        public void Sell(IItem item) => StoreItem.Sell(item);

        /// <inheritdoc/>
        public void Cycle(bool isPrevious) => StoreItem.Cycle(isPrevious);

        /// <inheritdoc/>
        public void Select(IItem item) => StoreItem.Select(item);

        /// <inheritdoc/>
        public void Refresh() => StoreItem.Refresh();
    } // end class
} // end namespace
