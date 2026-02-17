using ChipMasters.Menu.Interactables.Buttons.Closes;
using System;

namespace ChipMasters.Menu.TipsMenu
{
    /// <summary>
    /// Contract for a menu that displays helpful tips during gameplay.
    /// </summary>
    public interface ITipsMenu : IClosable, IDisposable
    {
        /// <summary>
        /// Displays a helpful tips based on the user's hand appraisal.
        /// </summary>
        void ShowTip(bool isMatchConcluded, int playerTotal, int dealerTotal);

        /// <summary>
        /// Open the menu.
        /// </summary>
        void Open();
    } // end interface
} // end namespace
