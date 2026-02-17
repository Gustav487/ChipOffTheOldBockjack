using ChipMasters.Items;

namespace ChipMasters.Menu.StoreMenu
{
    /// <summary>
    /// Contract for a store menu.
    /// </summary>
    public interface IStoreMenu
    {
        /// <summary>
        /// The current tab being displayed.
        /// </summary>
        int CurrentTab { get; set; }

        /// <summary>
        /// Get what item category is assciated with each tab.
        /// </summary>
        /// <param name="tabIndex"></param>
        /// <returns></returns>
        EItemCategory GetCategoryForTab(int tabIndex);
    } // end interface
} // end namespace
