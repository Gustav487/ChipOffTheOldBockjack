namespace ChipMasters.GodotWrappers
{
    /// <summary>
    /// Contract parallel to a <see cref="Godot.Range"/>, see also ChipMasters.GodotWrappers.<see cref="NRange"/>.
    /// </summary>
    public interface IRange
    {
        /// <summary>
        /// Refer to <see cref="Godot.Range.MinValue"/>.
        /// </summary>
        double MinValue { get; set; }

        /// <summary>
        /// Refer to <see cref="Godot.Range.MaxValue"/>.
        /// </summary>
        double MaxValue { get; set; }

        /// <summary>
        /// Refer to <see cref="Godot.Range.Value"/>.
        /// </summary>
        double Value { get; set; }

        /// <summary>
        /// Refer to <see cref="Godot.Range.ValueChanged"/>.
        /// </summary>
        event Godot.Range.ValueChangedEventHandler? ValueChanged;
    } // end interface
} // end namespace