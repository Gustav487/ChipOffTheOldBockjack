using ChipMasters.Cards;
using ChipMasters.GodotWrappers;

namespace ChipMasters.Menu.Displays.Cards
{
    /// <summary>
    /// <see cref="IControl"/> and and <see cref="ICard"/> <see cref="IDisplay{T}"/>.
    /// </summary>
    public interface IControlCardDisplay : IControl, IDisplay<ICard>
    {

    } // end interface
} // end namespace