namespace Tests.ChipMasters.Util
{
    /// <summary>
    /// Extension and utility methods for performing basic mathematical calculation.
    /// </summary>
    public static class MathUtil
    {
        /// <summary>
        /// different between two <see cref="float"/>s within which they're considered equal.
        /// </summary>
        public const float EPSILON = 1e-5f;



        /// <summary>
        /// Is the difference between two <see cref="float"/>s less than <paramref name="epsilon"/>, i.e. are they approximately equal.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="epsilon"></param>
        /// <returns></returns>
        public static bool ApproximatelyEquals(this float a, float b, float epsilon = EPSILON)
            => b < (a + epsilon) && b > (a - epsilon);

        /// <summary>
        /// Is <paramref name="a"/> more than <paramref name="epsilon"/> smaller than <paramref name="b"/>.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="epsilon"></param>
        /// <returns></returns>
        public static bool ApproximatelyLessThan(this float a, float b, float epsilon = EPSILON)
             => a < (b + epsilon) && a < (b - epsilon);
    } // end class
} // end namespace