using Godot;

namespace ChipMasters.Menu.Displays
{
    /// <summary>
    /// <see cref="ANDisplay{T}"/> for wrapping another <see cref="IDisplay{T}"/>, abstract due to Godot limitations on generic.
    /// </summary>
    public abstract partial class ANWrappingDisplay<T> : ANDisplay<T>
    {
        [Export] private Node _display = null!; // set by export 

        /// <inheritdoc/>
        protected override IDisplay<T> ConstructInner()
            => (IDisplay<T>)_display.AssertNotNull();

    } // end class
} // end namespace