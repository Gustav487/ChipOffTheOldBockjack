using ChipMasters.GodotWrappers;
using ChipMasters.User;

namespace ChipMasters.Menu.Displays.ChipRecords
{
    /// <summary>
    /// <see cref="NLabel"/> based <see cref="VChipRecord"/> <see cref="IDisplay{T}"/> wrapper of a <see cref="RChipRecordDisplay"/>.
    /// </summary>
#warning has to be it's own NLabel otherwise layout doesn't work right. This is not composition though, and disallows using ANDisplay as base class
    public sealed partial class NChipRecordDisplay : NLabel, IDisplay<VChipRecord?>
    {
        /// <inheritdoc/>
        public VChipRecord? Display { get => Inner.Display; set => Inner.Display = value; }

        private IDisplay<VChipRecord?> Inner => _inner ?? throw new RNotReadyException();
        private IDisplay<VChipRecord?>? _inner;



        /// <inheritdoc/>
        public override void _Ready()
        {
            base._Ready();

            _inner = new RChipRecordDisplay(this);
        } // end _Ready()

    } // end class
} // end namespace
