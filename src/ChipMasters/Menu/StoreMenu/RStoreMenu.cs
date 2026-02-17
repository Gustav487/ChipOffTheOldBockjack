using ChipMasters.Items;
using System;
using System.Collections.Generic;

namespace ChipMasters.Menu.StoreMenu
{
    /// <summary>
    /// Simple <see cref="IStoreMenu"/> implementation.
    /// </summary>
    public sealed class RStoreMenu : IStoreMenu
    {
        private readonly IStoreMenu _storeMenu;

        /// <inheritdoc/>
        /// <see cref="NStoreItem._Ready()"/> for implementation.
        /// Used to reference each tab, don't know a better place to put.
        public static int TAB_REF = 0;

        private readonly Dictionary<int, EItemCategory> _tabCategories = new();

        /// <inheritdoc/>
        public int CurrentTab { get => _storeMenu.CurrentTab; set => _storeMenu.CurrentTab = value; }

        /// <inheritdoc/>
        public RStoreMenu(IStoreMenu storeMenu)
        {
            _storeMenu = storeMenu;
            TAB_REF = 0;
            InitializeTabCategories();
        } // end ctor

        /// <inheritdoc/>
        public EItemCategory GetCategoryForTab(int tabIndex)
        {
            return _tabCategories.TryGetValue(tabIndex, out EItemCategory category)
                ? category
                : throw new ArgumentOutOfRangeException(nameof(tabIndex), "Tab index out of range.");
        } // end GetCategoryForTab()

        private void InitializeTabCategories()
        {
            int i = 0;

            foreach (int value in Enum.GetValues(typeof(EItemCategory)))
            {
                _tabCategories[i] = (EItemCategory)value;
                i++;
            }
        } // end InitializeTabCategories()
    } // end class
} // end namespace
