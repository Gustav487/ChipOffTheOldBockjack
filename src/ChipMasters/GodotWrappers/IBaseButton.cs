using System;

namespace ChipMasters.GodotWrappers
{
    /// <summary>
    /// Contract parallel to a <see cref="Godot.BaseButton"/>, see also ChipMasters.GodotWrappers.<see cref="NBaseButton"/>.
    /// </summary>
    public interface IBaseButton : IControl
    {
        /// <summary>
        /// Refer to <see cref="Godot.BaseButton.ButtonPressed"/>.
        /// </summary>
        bool ButtonPressed { get; set; }

        /// <summary>
        /// Refer to <see cref="Godot.BaseButton.Disabled"/>.
        /// </summary>
        bool Disabled { get; set; }

        /// <summary>
        /// Refer to <see cref="Godot.BaseButton.ButtonDown"/>.
        /// </summary>
        event Action? ButtonDown;

        /// <summary>
        /// Refer to <see cref="Godot.BaseButton.Pressed"/>.
        /// </summary>
        event Action? Pressed;
    } // end interface
} // end namespace