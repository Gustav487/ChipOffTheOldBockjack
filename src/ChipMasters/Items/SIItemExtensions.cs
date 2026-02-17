using ChipMasters.Registers;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace ChipMasters.Items
{
    /// <summary>
    /// Class containing extension methods for <see cref="IItem"/>
    /// </summary>
    public static class SIItemExtensions
    {
        private const float REFUND_PERCENTAGE = 0.8F;

        /// <summary>
        /// Returns the refund price of an item.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static int GetRefundPrice(this IItem item)
        {
            return (int)(item.Price * REFUND_PERCENTAGE);
        } // end GetRefundPrice()

        /// <summary>
        /// Returns a list of all registered items in the game.
        /// Use as an extension for: SItems.REGISTER
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public static IImmutableList<IItem> GetAllItems(this IDictionary<string, IItem> items)
        {
            return items.Values
                .OrderBy(item => item.Category)
                .ThenBy(item => item.Price)
                .ToImmutableList();
        } // end GetAllItems()

        /// <summary>
        /// Get the default item in the same category as <paramref name="item"/>.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static IItem GetDefaultItem(this IItem item)
        {
            return SItems.REGISTER.GetAllItems().Where(i => i.Price == 0 && i.Category == item.Category).Single();
        } // end GetDefaultItem()
    } // end class
} // end namespace 