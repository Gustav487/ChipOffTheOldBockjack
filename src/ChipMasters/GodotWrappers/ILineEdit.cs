namespace ChipMasters.GodotWrappers
{
    /// <summary>
    /// Contract parallel to a <see cref="Godot.LineEdit"/>, see also ChipMasters.GodotWrappers.<see cref="NLineEdit"/>.
    /// </summary>
    public interface ILineEdit
    {
        /// <summary>
        /// Refer to <see cref="Godot.LineEdit.Text"/>.
        /// </summary>
        string Text { get; set; }
    } // end interface
} // end namespace