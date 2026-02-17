using System.Collections.Generic;

namespace ChipMasters.Menu.Displays.Arrangers
{
    /// <summary>
    /// Contract for an object that positions a list of <typeparamref name="T"/> by some rules.
    /// </summary>
    public interface IArranger<T>
    {
        /// <summary>
        /// Arrange the elements.
        /// </summary>
        /// <param name="elements"></param>
        void Arrange(IReadOnlyList<T> elements);
    } // end interface
} // end namespace