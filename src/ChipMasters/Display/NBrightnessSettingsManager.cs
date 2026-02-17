using ChipMasters.GodotWrappers;
using ChipMasters.User;
using Godot;
using System;

namespace ChipMasters.Display
{
    /// <summary>
    /// <see cref="NNode"/> for managing and applying screen brightness settings.
    /// </summary>
    public sealed partial class NBrightnessSettingsManager : NNode
    {
        /// <summary>
        /// Single instance of the class.
        /// </summary>
        public static NBrightnessSettingsManager INSTANCE => s_instance ?? throw new RNotReadyException();
        private static NBrightnessSettingsManager? s_instance;

        private ColorRect? _brightnessOverlay;

        /// <inheritdoc/>
        public NBrightnessSettingsManager()
        {
            if (s_instance is not null)
                throw new InvalidOperationException();

            s_instance = this;
        } // end ctor

        /// <inheritdoc/>
        public override void _Ready()
        {
            // Create a full-screen overlay for brightness control
            _brightnessOverlay = new ColorRect
            {
                Color = new Color(0, 0, 0, 0),
                MouseFilter = Control.MouseFilterEnum.Ignore,
                ZIndex = (int)RenderingServer.CanvasItemZMax, // Ensure it's on top of everything
                ProcessMode = ProcessModeEnum.Always, // Ensure it's always processed
                LayoutMode = 1, // Use container layout (1 = Container)
                AnchorLeft = 0,
                AnchorTop = 0,
                AnchorRight = 1,
                AnchorBottom = 1,
                OffsetLeft = 0,
                OffsetTop = 0,
                OffsetRight = 0,
                OffsetBottom = 0
            };

            // Add the overlay to the root viewport
            var root = GetTree().Root;
            if (root != null)
            {
                // Add to the root's first child (usually the main viewport)
                var mainViewport = root.GetChild(0);
                if (mainViewport != null)
                {
                    mainViewport.AddChild(_brightnessOverlay);

                    // Apply initial brightness from settings
                    ApplyBrightness(RUser.INSTANCE.Settings.BrightnessLevel);
                }
            }
        } // end _Ready()

        /// <summary>
        /// Applies the given brightness level to the screen.
        /// </summary>
        /// <param name="brightness">Brightness level between 0.1 and 1.0</param>
        public void ApplyBrightness(float brightness)
        {
            if (_brightnessOverlay is null)
                return;

            // Convert brightness to alpha value (inverse relationship)
            float alpha = 1.0f - brightness;
            _brightnessOverlay.Color = new Color(0, 0, 0, alpha);

            // Force update
            _brightnessOverlay.QueueRedraw();

            // Ensure the overlay is visible and on top
            _brightnessOverlay.Visible = true;
            _brightnessOverlay.MoveToFront();
        } // end ApplyBrightness()
    } // end class
} // end namespace 
