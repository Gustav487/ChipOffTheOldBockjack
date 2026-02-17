using System;

namespace ChipMasters.Menu.Displays
{
    /// <summary>
    /// Base contract for a display.
    /// </summary>
    public interface IDisplay<T> : IDisposable
    {
        /// <summary>
        /// Instance of <typeparamref name="T"/> to display, null if none.
        /// </summary>
        T? Display { get; set; }
    } // end interface
} // end namespace