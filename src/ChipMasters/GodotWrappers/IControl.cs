namespace ChipMasters.GodotWrappers
{
    /// <summary>
    /// Contract parallel to a <see cref="Godot.Control"/>, see also ChipMasters.GodotWrappers.<see cref="NControl"/>.
    /// </summary>
    public interface IControl : ICanvasItem
    {


        /// <summary>
        /// Refer to <see cref="Godot.Control.TooltipText"/>.
        /// </summary>
        string TooltipText { get; set; }
        /// <summary>
        /// Refer to <see cref="Godot.Control.MouseFilter"/>.
        /// </summary>
        Godot.Control.MouseFilterEnum MouseFilter { get; set; }

        /// <summary>
        /// Refer to <see cref="Godot.Control.AnchorTop"/>.
        /// </summary>
        float AnchorTop { get; set; }
        /// <summary>
        /// Refer to <see cref="Godot.Control.AnchorBottom"/>.
        /// </summary>
        float AnchorBottom { get; set; }
        /// <summary>
        /// Refer to <see cref="Godot.Control.AnchorLeft"/>.
        /// </summary>
        float AnchorLeft { get; set; }
        /// <summary>
        /// Refer to <see cref="Godot.Control.AnchorRight"/>.
        /// </summary>
        float AnchorRight { get; set; }
    } // end interface
} // end namespace