using ChipMasters.Games.BetHandlers;
using ChipMasters.GodotWrappers;
using System;

namespace ChipMasters.Menu.BetSelections
{
    /// <summary>
    /// <see cref="IBetSelectionMenu"/> that can be set to automatically reuse the last bet, 
    /// otherwise functions through using a wrapped <see cref="IBetSelectionMenu"/>.
    /// </summary>
    public sealed class RAutobetBetSelectionMenu : IBetSelectionMenu
    {
        /// <inheritdoc/>
        public VBetRange Range { get => _betSelectionMenu.Range; set => _betSelectionMenu.Range = value; }
        /// <inheritdoc/>
        public int Selected { get => _betSelectionMenu.Selected; set => _betSelectionMenu.Selected = value; }

        private readonly IBetSelectionMenu _betSelectionMenu;
        private readonly ICheckBox _checkBox;



        /// <inheritdoc/>
        /// <param name="betSelectionMenu"><see cref="IBetSelectionMenu"/> used if auto-bet isn't enabled.</param>
        /// <param name="checkBox">Button determining if autobet should be used.</param>
        public RAutobetBetSelectionMenu(IBetSelectionMenu betSelectionMenu, ICheckBox checkBox)
        {
            _betSelectionMenu = betSelectionMenu.AssertNotNull();
            _checkBox = checkBox.AssertNotNull();
        } // end ctor



        /// <inheritdoc/>
        public void Dispose() => _betSelectionMenu.Dispose();

        /// <inheritdoc/>
        public void Open(Action<int> submitCallback)
        {
            if (_checkBox.ButtonPressed)
                submitCallback(Selected);
            else
                _betSelectionMenu.Open(submitCallback);
        } // end Open()

        /// <inheritdoc/>
        public void Close() => _betSelectionMenu.Close();

    } // end class
} // end namespace