using ChipMasters.Games.BetHandlers;
using ChipMasters.GodotWrappers;
using ChipMasters.Menu.BetSelections;
using Godot;
using System;

namespace ChipMasters.Menu
{
    /// <summary>
    /// <see cref="NNode"/> based <see cref="IBetSelectionMenu"/> that wraps oover <see cref="RAutobetBetSelectionMenu"/>.
    /// </summary>
    public partial class NAutobetBetSelectionMenu : NNode, IBetSelectionMenu
    {
        [Export] private Node _betSelectionMenu = null!;
        [Export] private Node _autoBetToggle = null!;



        /// <inheritdoc/>
        public VBetRange Range { get => Inner.Range; set => Inner.Range = value; } // end Range

        /// <inheritdoc/>
        public int Selected { get => Inner.Selected; set => Inner.Selected = value; }



        private IBetSelectionMenu Inner => _inner ?? throw new RNotReadyException(nameof(_inner));
        private IBetSelectionMenu? _inner;



        /// <inheritdoc/>
        public override void _Ready()
        {
            base._Ready();
            _inner = new RAutobetBetSelectionMenu(
                (IBetSelectionMenu)_betSelectionMenu.AssertNotNull(), (ICheckBox)_autoBetToggle.AssertNotNull());
        } // end _Ready()

        /// <inheritdoc/>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            Inner.Dispose();
        } // end Dispose()



        /// <inheritdoc/>
        public void Open(Action<int> submitCallback)
        {
            Inner.Open(submitCallback);
        } // end Open()

        /// <inheritdoc/>
        public void Close()
        {
            Inner.Close();
        } // end Close()
    } // end class
} // end namespace
