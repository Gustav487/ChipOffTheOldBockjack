namespace ChipMasters.GodotWrappers.Helpers
{
    /// <summary>
    /// Contract used to unify interaction with any godot object with a <see cref="Text"/> like property.
    /// </summary>
    public interface ITextual
    {
        /// <summary>
        /// Property parallel to godot properties like <see cref="Godot.Label.Text"/> or <see cref="Godot.LineEdit.Text"/>
        /// </summary>
        public string Text { get; set; }
    } // end interface
} // end namespace