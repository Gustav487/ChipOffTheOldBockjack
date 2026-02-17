using ChipMasters.GodotWrappers;
using Godot;

namespace ChipMasters.Menu.Displays
{
    /// <summary>
    /// <typeparamref name="T"/> <see cref="ANDisplay{T}"/> that displays to a <see cref="ILabel"/> <see cref="_label"/>. 
    /// </summary>
    public abstract partial class ANLabelDisplay<T> : ANDisplay<T>
    {
        /// <summary>
        /// Label to be written too, must implement <see cref="ILabel"/>. 
        /// </summary>
        [Export] protected Node _label = null!;
    } // end class
} // end namespace