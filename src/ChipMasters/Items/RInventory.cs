using ChipMasters.Registers;
using System.Collections.Generic;
using System.Linq;

namespace ChipMasters.Items
{
    /// <summary>
    /// Simple <see cref="IInventory"/> implementation.
    /// </summary>
    public class RInventory : IInventory
    {
        /// <inheritdoc/>
        public IReadOnlySet<IItem> Items => _items;
        private readonly HashSet<IItem> _items = new();



        /// <inheritdoc/>
        public RInventory() : this(Enumerable.Empty<IItem>()) { }

        /// <inheritdoc/>
        public RInventory(IEnumerable<IItem> items)
        {
            _items = items.ToHashSet();

            // Add all "default" items to the inventory when created
            foreach (IItem item in SItems.REGISTER.GetAllItems())
            {
                if (item.Price <= 0)
                    _items.Add(item);
            }
        } // end ctor





        /// <inheritdoc/>
        public void Add(IItem item) => _items.Add(item);

        /// <inheritdoc/>
        public void Remove(IItem item) => _items.Remove(item);

    } // end class
} // end namespace