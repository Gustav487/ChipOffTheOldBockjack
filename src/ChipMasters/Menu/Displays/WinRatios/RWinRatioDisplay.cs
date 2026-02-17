using ChipMasters.GodotWrappers;
using ChipMasters.User;
using GSR.Utilic;

namespace ChipMasters.Menu.Displays.WinRatios
{
    /// <summary>
    /// <see cref="VWinRatio"/> <see cref="ALabelDisplay{T}"/>.
    /// </summary>
    public sealed class RWinRatioDisplay : ALabelDisplay<VWinRatio?>
    {
        /// <inheritdoc/>
        public RWinRatioDisplay(ILabel label) : base(label) { } // end ctor

        /// <inheritdoc/>
        protected override string ToString(VWinRatio? displaying)
#warning Note: C# nullability works horribly with generics, without constraining the type parameter it can't resolve reference and value nullability together, code duplication required just for different struct/class constraints.
            => displaying?.ToString() ?? throw new UnexpectedStateException();
    } // end class
} // end namespace