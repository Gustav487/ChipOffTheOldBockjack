using System;

namespace ChipMasters.Games.BetHandlers
{
    /// <summary>
    /// Representation of the inclusive range of valid values for a bet.
    /// </summary>
    public struct VBetRange
    {
        /// <summary>
        /// Value from 0 to <see cref="int.MaxValue"/> indicating lowest legal bet.
        /// </summary>
        public int Min { get; }

        /// <summary>
        /// Value from 0 to <see cref="int.MaxValue"/> indicating highest legal bet.
        /// </summary>
        public int Max { get; }



        /// <inheritdoc/>
        public VBetRange(int min, int max)
        {
            if (min < 0)
                throw new ArgumentException($"{nameof(min)} bet can't be less than 0.");
            if (max < min)
                throw new ArgumentException($"{nameof(max)} was lower than {nameof(min)}.");

            Min = min;
            Max = max;
        } // end ctor



        /// <summary>
        /// Check if <paramref name="value"/> lays inside of the inclusive range.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Contains(int value) => value <= Max && value >= Min;

    } // end class
} // end namespace
