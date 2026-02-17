using ChipMasters.GodotWrappers;
using ChipMasters.Items;
using ChipMasters.User;

namespace ChipMasters.Menu.Interactables.Buttons.Bankruptcy
{
    /// <summary>
    /// Handles logic for the sell buttons in the bankruptcy menu.
    /// </summary>
    public partial class NBankruptcyItemButton : NButton
    {
        /// <summary>
        /// The item to sell.
        /// </summary>
        public IItem item = null!;

        /// <inheritdoc/>
        public override void _Ready()
        {
            base._Ready();
            item.AssertNotNull();
            Pressed += () => SellItem(item);
        }

        /// <summary>
        /// Remove item from inventory and give refund to wallet.
        /// </summary>
        /// <param name="item">The item to sell.</param>
        private void SellItem(IItem item)
        {
            RUser.INSTANCE.Sell(item);
            Visible = false;
        }
    } // end class
} // end namespace