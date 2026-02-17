namespace ChipMasters.GodotWrappers
{
    /// <summary>
    /// Contract parallel to a <see cref="Godot.Button"/>, see also ChipMasters.GodotWrappers.<see cref="NButton"/>.
    /// </summary>
    public interface IButton : IBaseButton
    {
        /// <summary>
        /// Refer to <see cref="Godot.Button.Text"/>.
        /// </summary>
        string Text { get; set; }
    } // end interface
} // end namespace