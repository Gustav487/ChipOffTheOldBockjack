using ChipMasters.GodotWrappers;
using ChipMasters.Items;
using ChipMasters.Registers;
using Godot;
using System;
using System.Collections.Generic;

namespace ChipMasters.Menu.StoreMenu
{
    /// <summary>
    /// TabContainer for displaying the store menu.
    /// </summary>
    public sealed partial class NStoreMenu : TabContainer, IStoreMenu
    {
        [Export] private Node itemLabel = null!;
        [Export] private Node purchaseButton = null!;
        [Export] private Node sellButton = null!;
        [Export] private Node previousItemButton = null!;
        [Export] private Node nextItemButton = null!;
        [Export] private Node selectButton = null!;
        [Export] private CanvasItem itemPreview = null!;

        /// <inheritdoc/>
        public new int CurrentTab { get => base.CurrentTab; set => base.CurrentTab = value; }

        private IStoreMenu StoreMenu => _storeMenu ?? throw new RNotReadyException(nameof(_storeMenu));
        private IStoreMenu? _storeMenu;

        /// <inheritdoc/>
        public override void _Ready()
        {
            base._Ready();
            _storeMenu = new RStoreMenu(this);
            InitializeTabs();
            TabChanged += OnTabChanged;
        } // end _Ready()

        /// <inheritdoc/>
        public EItemCategory GetCategoryForTab(int tabIndex) => StoreMenu.GetCategoryForTab(tabIndex);

        /// <summary>
        /// Get item category for tab being switched to and refresh.
        /// </summary>
        /// <param name="tabIndex"></param>
        private void OnTabChanged(long tabIndex)
        {
            _storeMenu?.GetCategoryForTab((int)tabIndex);
            ((NStoreItem)GetChild(Convert.ToInt32(tabIndex)).GetChild(0)).Refresh();
        } // end OnTabChanged()

        /// <summary>
        /// Dynamically create the tabs in the store menu based on item categories.
        /// </summary>
        private void InitializeTabs()
        {
            Dictionary<EItemCategory, TabBar> tabBars = new();
            List<IItem> firstItemOfEachType = new List<IItem>();

            // Create each tab bar for each item category
            foreach (IItem item in SItems.REGISTER.GetAllItems())
            {
                if (!tabBars.ContainsKey(item.Category))
                {
                    TabBar tabBar = new TabBar { Name = $"{item.Category.DisplayText()}s" };
                    AddChild(tabBar);
                    tabBars[item.Category] = tabBar;
                    firstItemOfEachType.Add(item);
                }
            }

            // Create a storeItem for each tab bar
            foreach (IItem item in firstItemOfEachType)
            {
                NStoreItem storeItem = new NStoreItem
                {
                    Item = item,
                    _storeMenu = this,
                    _itemLabel = (NLabel)itemLabel,
                    _purchaseButton = (NButton)purchaseButton,
                    _sellButton = (NButton)sellButton,
                    _previousItemButton = (NBaseButton)previousItemButton,
                    _nextItemButton = (NBaseButton)nextItemButton,
                    _selectButton = (NBaseButton)selectButton,
                    _itemPreview = (NControl)itemPreview
                };
                tabBars[item.Category].AddChild(storeItem);
            }
        } // end InitializeTabs()
    } // end class
} // end namespace
