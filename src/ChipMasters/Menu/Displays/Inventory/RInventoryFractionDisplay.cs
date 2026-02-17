using ChipMasters.GodotWrappers;
using ChipMasters.Items;
using ChipMasters.Registers;
using System.Linq;

namespace ChipMasters.Menu.Displays.Inventory
{
    /// <summary>
    /// <see cref="IInventory"/> <see cref="ANLabelDisplay{T}"/> for display fraction of items owned to a text <see cref="ILabel"/>.
    /// </summary>
    public sealed class RInventoryFractionDisplay : ALabelDisplay<IInventory>
    {
        /// <inheritdoc/>
        public RInventoryFractionDisplay(ILabel label) : base(label)
        { } // end ctor

        /// <inheritdoc/>
        protected override string ToString(IInventory displaying)
#warning no need to repeated recalculate the number of non-default items in register, when register is theoretically constant.
            => $"{displaying.Items.Where(x => x.Price > 0).Count()} / {SItems.REGISTER.Where(x => x.Value.Price > 0).Count()}";

    } // end class
} // end namespace