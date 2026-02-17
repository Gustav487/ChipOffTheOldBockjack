using ChipMasters.Items;
using System;

namespace ChipMasters.Menu.StoreMenu
{
    /// <summary>
    /// Contract for an item in the store.
    /// </summary>
    public interface IStoreItem
    {
        /// <summary>
        /// The index of this <see cref="IStoreItem"/> in the tab container.
        /// </summary>
        int TabIndex { get; }

        /// <summary>
        /// Representation of an <see cref="IItem"/> in the store.
        /// </summary>
        IItem Item { get; set; }

        /// <summary>
        /// Event raised when an item is purchased.
        /// </summary>
        event Action? OnPurchase;

        /// <summary>
        /// Event raised when an item is sold.
        /// </summary>
        event Action? OnSell;

        /// <summary>
        /// Event raised when cycling through items.
        /// </summary>
        event Action? OnCycle;

        /// <summary>
        /// Event raised when selecting and applying the item.
        /// </summary>
        event Action? OnSelect;

        /// <summary>
        /// Purchase this item.
        /// </summary>
        /// <param name="item">The item to purchase.</param>
        void Purchase(IItem item);

        /// <summary>
        /// Sell this item.
        /// </summary>
        /// <param name="item">The item to sell.</param>
        void Sell(IItem item);

        /// <summary>
        /// Cycle through items.
        /// </summary>
        /// <param name="isPrevious">If true, cycle backwards.</param>
        void Cycle(bool isPrevious);

        /// <summary>
        /// Select and apply the item throughout the game.
        /// </summary>
        /// <param name="item">The item to select and apply.</param>
        void Select(IItem item);

        /// <summary>
        /// Refresh the store menu display.
        /// </summary>
        void Refresh();
    } // end class
} // end namespace
