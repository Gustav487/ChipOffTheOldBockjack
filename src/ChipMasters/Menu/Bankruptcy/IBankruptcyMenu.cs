using ChipMasters.Menu.Interactables.Buttons.Closes;
using System;

namespace ChipMasters.Menu.Bankruptcy
{
    /// <summary>
    /// Contract for a bankruptcy menu.
    /// </summary>
    public interface IBankruptcyMenu : IVisable, IClosable
    {
        /// <summary>
        /// Display the bankruptcy confirmation box.
        /// </summary>
        void ShowBankruptcyBox();

        /// <summary>
        /// Open the bankruptcy menu.
        /// </summary>
        void Open();

        /// <summary>
        /// Event raised when the menu opens.
        /// </summary>
        event Action? OnOpen;

        /// <summary>
        /// Event raised when the menu closes.
        /// </summary>
        event Action? OnClose;

        /// <summary>
        /// If true, do not show the bankruptcy confim box until game reset.
        /// </summary>
        bool HideBankruptcyBox { get; set; }
    } // end interface
} // end namespace
