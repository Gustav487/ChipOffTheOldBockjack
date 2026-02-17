using ChipMasters.Menu;
using Godot;

namespace ChipMasters.GodotWrappers
{
    /// <summary>
    /// Contract parallel to a <see cref="Godot.CanvasItem"/>.
    /// </summary>
    public interface ICanvasItem : INode, IVisable
    {
        /// <summary>
        /// Refer to <see cref="Godot.CanvasItem.Visible"/>.
        /// </summary>
        Material Material { get; set; }
    } // end interface
} // end namespace