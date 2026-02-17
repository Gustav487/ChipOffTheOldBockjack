namespace ChipMasters.GodotWrappers
{
    /// <summary>
    /// Contract parallel to a <see cref="Godot.Label"/>, see also ChipMasters.GodotWrappers.<see cref="NLabel"/>.
    /// </summary>
    public interface ILabel : IControl
    {
        /// <summary>
        /// Refer to <see cref="Godot.Label.Text"/>.
        /// </summary>
        string Text { get; set; }
    } // end interface
} // end namespace