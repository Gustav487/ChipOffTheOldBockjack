using ChipMasters.GodotWrappers;
using Godot;
using Godot.Collections;
using GSR.Utilic.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace ChipMasters.Menu.Displays.Chips
{
    /// <summary>
    /// <see cref="NNode"/> <see cref="IChipDisplay"/> that will update several subdisplays.
    /// </summary>
    public partial class NAggregateChipDisplay : NNode, IChipDisplay
    {
        [Export] private Array<Node> _subDisplays = null!;



        /// <inheritdoc/>
        public int? Chips
        {
            get => _chips;
            set
            {
                _chips = value;
                SubDisplays.ForEvery((x) => x.Chips = value);
            }
        } // end Chips
        private int? _chips;

        /// <inheritdoc/>
        public bool ExplicitSign
        {
            get => _explicitSign;
            set
            {
                _explicitSign = value;
                SubDisplays.ForEvery((x) => x.ExplicitSign = value);
            }
        } // end ExplicitSign
        private bool _explicitSign;

        private IImmutableList<IChipDisplay> SubDisplays => i_subDisplays ?? throw new RNotReadyException();
        private IImmutableList<IChipDisplay>? i_subDisplays;



        /// <inheritdoc/>
        public override void _Ready()
        {
            base._Ready();
            i_subDisplays = _subDisplays.AssertNotNull().Cast<IChipDisplay>().ToImmutableList();
        } // end _Ready()

    } // end class
} // end namespace
