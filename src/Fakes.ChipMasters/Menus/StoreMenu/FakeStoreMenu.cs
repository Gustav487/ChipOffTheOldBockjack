using ChipMasters.Items;
using ChipMasters.Menu.StoreMenu;

namespace Fakes.ChipMasters.Menus.StoreMenu
{
    public sealed class FakeStoreMenu : IStoreMenu
    {
        public int CurrentTab { get; set; }

        public EItemCategory GetCategoryForTab(int tabIndex)
        {
            throw new NotImplementedException();
        } // end GetCategoryForTab()
    } // end class
} // end namespace