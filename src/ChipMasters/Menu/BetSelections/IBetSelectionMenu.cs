using ChipMasters.Games.BetHandlers;
using ChipMasters.Menu.Interactables.Buttons.Closes;
using System;

namespace ChipMasters.Menu.BetSelections
{
    /// <summary>
    /// Contract for a menu for selection a given bet.
    /// 
    /// Should automatically close on submit.
    /// </summary>
    public interface IBetSelectionMenu : IClosable, IDisposable
    {
        /// <summary>
        /// Legal bet range. Should clear all other fields when changed.
        /// </summary>
        public VBetRange Range { get; set; }

        /// <summary>
        /// Currently selected bet.
        /// </summary>
        public int Selected { get; set; }

        /// <summary>
        /// Open the menu.
        /// </summary>
        /// <param name="submitCallback">Code to run once submit, passes select amount as argument. Callback removed once menu closed.</param>
        void Open(Action<int> submitCallback);
    } // end interface
} // end namespace
