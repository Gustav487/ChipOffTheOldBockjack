using ChipMasters.Items;

namespace ChipMasters.User
{
    /// <summary>
    /// Class containing extension methods for <see cref="IUser"/>
    /// </summary>
    public static class SIUserExtensions
    {
        /// <summary>
        /// Add <paramref name="item"/> to the <paramref name="user"/>'s inventory.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="user"></param>
        public static void Purchase(this IUser user, IItem item)
        {
            user.Wallet.Chips -= item.Price;
            user.Inventory.Add(item);
        } // end Purchase()

        /// <summary>
        /// Remove <paramref name="item"/> from the <paramref name="user"/>'s inventory.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="user"></param>
        public static void Sell(this IUser user, IItem item)
        {
            user.Wallet.Chips += item.GetRefundPrice();
            user.Inventory.Remove(item);

            if (((IApplicableItem)item).IsApplied(user))
                ((IApplicableItem)item.GetDefaultItem()).Apply(user);
        } // end Sell()
    } // end class
} // namespace
