using ChipMasters.Display;
using ChipMasters.GodotWrappers;
using ChipMasters.User;
using Godot;

namespace ChipMasters.Menu
{
    /// <summary>
    /// <see cref="NHSlider"/> for adjusting the screen brightness level setting.
    /// </summary>
    public sealed partial class NBrightnessSlider : NHSlider
    {
        private const float MinBrightness = 0.1f;
        private const float MaxBrightness = 1.0f;

        /// <inheritdoc/>
        public override void _Ready()
        {
            // Ensure brightness updates visually
            ValueChanged += SetToValue;

            // Set slider's value range (expecting 0-100 scale in the editor)
            MinValue = 0;
            MaxValue = 100;

            // Get current brightness from settings and update slider accordingly
            float currentBrightness = RUser.INSTANCE.Settings.BrightnessLevel;
            currentBrightness = Mathf.Clamp(currentBrightness, MinBrightness, MaxBrightness);
            float interp = Mathf.InverseLerp(MinBrightness, MaxBrightness, currentBrightness);
            Value = interp * 100f; // Set slider to match current brightness

            SetToValue(Value);
        } // end _Ready()

        private void SetToValue(double value)
        {
            float newBrightness = Mathf.Lerp(MinBrightness, MaxBrightness, (float)value / 100f);
            RUser.INSTANCE.Settings.BrightnessLevel = newBrightness;
            NBrightnessSettingsManager.INSTANCE.ApplyBrightness(newBrightness);
        } // end SetToValue()
    } // end class
} // end namespace 
