using System;

namespace ChipMasters
{
    /// <summary>
    /// Utility methods for validating parameters are valid.
    /// </summary>
    public static class SContractUtil
    {
        /// <summary>
        /// Throw an exception if <paramref name="argument"/> is null, otherwise return <paramref name="argument"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="argument"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static T AssertNotNull<T>(this T? argument) => argument is not null ? argument : throw new ArgumentNullException();
    } // end class
} // end namespace
