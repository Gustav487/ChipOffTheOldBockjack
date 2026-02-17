using ChipMasters.User;
using ChipMasters.Util;
using Godot;
using System.Collections.Generic;

namespace ChipMasters.Menu.Displays.ChipRecords
{
    /// <summary>
    /// <see cref="IReadOnlyList{T}"/> of <see cref="VChipRecord"/> <see cref="ANDisplay{T}"/> wrapper of a <see cref="RChipHistoryDisplay"/>.
    /// </summary>
    public sealed partial class NChipHistoryDisplay : ANDisplay<IReadOnlyList<VChipRecord>>
    {
        [Export] private Node _displayPool = null!;
        [Export] private Node _container = null!;



        /// <inheritdoc/>
        protected override IDisplay<IReadOnlyList<VChipRecord>> ConstructInner()
            => new RChipHistoryDisplay(
                (IPool<IDisplay<VChipRecord?>>)_displayPool.AssertNotNull(),
                (x) => _container.AddChild((Node)x),
                (x) => _container.RemoveChild((Node)x));
    } // end class
} // end namespace