using GSR.EnDecic;
using GSR.EnDecic.EnDecs;
using System.Collections.Generic;
using System.Linq;

namespace ChipMasters.Items
{
    /// <summary>
    /// Contract for an inventory's representation.
    /// </summary>
    public interface IInventory
    {
        /// <summary>
        /// Create a generic <see cref="IInventory"/> <see cref="IEnDec{T}"/>.
        /// </summary>
        public static readonly IEnDec<IInventory> ENDEC = EnDecUtil.KeyedEnDecBuilder<string, IInventory>(EnDecUtil.STRING)
            .Add("items", IItem.ENDEC.ListOf(), (x) => x.Items.ToList())
            .Build((i) => new RInventory(i));



        /// <summary>
        /// A list of held items.
        /// </summary>
        public IReadOnlySet<IItem> Items { get; }



        /// <summary>
        /// Adds an item to the inventory.
        /// </summary>
        /// <param name="item"></param>
        public void Add(IItem item);

        /// <summary>
        /// Removes an item from the inventory.
        /// </summary>
        /// <param name="item"></param>
        public void Remove(IItem item);
    } // end interface
} // end namespace