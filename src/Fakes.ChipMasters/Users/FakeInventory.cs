using ChipMasters.Items;

namespace Fakes.ChipMasters.Users
{
    /// <summary>
    /// Fake implementation of IInventory for testing purposes.
    /// </summary>
    public class FakeInventory : IInventory
    {
        public IReadOnlySet<IItem> Items => _items;
        private readonly HashSet<IItem> _items = new();



        /// <summary>
        /// Adds an item to the inventory.
        /// </summary>
        public void Add(IItem item) => _items.Add(item);

        /// <summary>
        /// Removes an item from the inventory.
        /// </summary>
        public void Remove(IItem item) => _items.Remove(item);
    } // end class
} // end namespace