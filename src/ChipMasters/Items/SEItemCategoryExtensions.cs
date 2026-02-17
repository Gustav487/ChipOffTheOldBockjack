using System.Text.RegularExpressions;

namespace ChipMasters.Items
{
    /// <summary>
    /// Extension methods for <see cref="EItemCategory"/>.
    /// </summary>
    public static class SEItemCategoryExtensions
    {
        /// <summary>
        /// Convert the enum pascal case text into correctly space english text.
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static string DisplayText(this EItemCategory e)
            => Regex.Replace(e.ToString(), "(?<=[a-z])(?=[A-Z])", " ");
    } // end class
} // end namespace