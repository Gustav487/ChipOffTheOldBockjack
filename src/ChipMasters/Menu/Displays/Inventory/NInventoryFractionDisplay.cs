using ChipMasters.GodotWrappers;
using ChipMasters.Items;

namespace ChipMasters.Menu.Displays.Inventory
{
    /// <summary>
    /// <see cref="IInventory"/> <see cref="ANLabelDisplay{T}"/> that wraps a <see cref="RInventoryFractionDisplay"/>.
    /// </summary>
    public sealed partial class NInventoryFractionDisplay : ANLabelDisplay<IInventory>
    {
        /// <inheritdoc/>
        protected override IDisplay<IInventory> ConstructInner()
            => new RInventoryFractionDisplay((ILabel)_label.AssertNotNull());
    } // end class
} // end namespace