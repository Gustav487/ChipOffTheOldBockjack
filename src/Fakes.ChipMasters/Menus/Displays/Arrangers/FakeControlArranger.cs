using ChipMasters.GodotWrappers;
using ChipMasters.Menu.Displays.Arrangers;

namespace Fakes.ChipMasters.Menus.Displays.Arrangers
{
    /// <summary>
    /// <see cref="IArranger{T}"/> for <see cref="IControl"/>s that set their anchors incrementally one higher than the prior, starting at 1.
    /// </summary>
    public class FakeControlArranger : IArranger<IControl>
    {

        public void Arrange(IReadOnlyList<IControl> elements)
        {
            int i = 1;
            foreach (IControl control in elements)
            {
                control.AnchorTop = i;
                control.AnchorBottom = i;
                control.AnchorLeft = i;
                control.AnchorRight = i;
                i++;
            }
        } // end Arrange
    } // end class
} // end namespace