using System;
using System.Collections.Generic;

namespace ChipMasters.Util
{
    /// <summary>
    /// Extension methods for <see cref="IList{T}"/>s.
    /// </summary>
    public static class SIListExtensions
    {
        /// <summary>
        /// Randomize the order of the given <paramref name="list"/>.
        /// </summary>
        /// <typeparam name="L"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static L Shuffled<L, T>(this L list)
            where L : IList<T>
        {
            Random r = new();
            for (int i = list.Count - 1; i > 0; i--)
            {
                int j = r.Next(i + 1);
                (list[i], list[j]) = (list[j], list[i]); // Swap
            }
            return list;
        } // end Shuffled()
    } // end class
} // end namespace