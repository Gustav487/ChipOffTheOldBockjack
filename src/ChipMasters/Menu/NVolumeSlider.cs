using ChipMasters.GodotWrappers;
using ChipMasters.User;
using Godot;
using System;

namespace ChipMasters.Menu
{
    /// <summary>
    /// <see cref="NHSlider"/> for adjusting an audio bus volume level setting.
    /// </summary>
    public sealed partial class NVolumeSlider : NHSlider
    {
        private const float MinDb = -30f;
        private const float MaxDb = 0f;

        [Export] private string _busName = "Master";




        /// <inheritdoc/>
        public override void _Ready()
        {
            // Ensure volume updates visually and audibly
            ValueChanged += SetToValue;

            int busIndex = AudioServer.GetBusIndex(_busName);
            if (busIndex == -1)
                throw new ArgumentException($"Bus not defined: {_busName}");

            // Set slider's value range (expecting 0-100 scale in the editor)
            MinValue = 0;
            MaxValue = 100;

            // Get current volume from the bus and update slider accordingly
            float currentDb = AudioServer.GetBusVolumeDb(busIndex);
            currentDb = Mathf.Clamp(currentDb, MinDb, MaxDb); // Clamp in case it's outside expected range
            float interp = Mathf.InverseLerp(MinDb, MaxDb, currentDb);
            Value = interp * 100f; // Set slider to match current bus volume

            SetToValue(Value);
        } // end _Ready()

        private void SetToValue(double value)
        {
            int busIndex = AudioServer.GetBusIndex(_busName);

            float newDb = Mathf.Lerp(MinDb, MaxDb, (float)value / 100f);
            AudioServer.SetBusVolumeDb(busIndex, newDb);
            RUser.INSTANCE.Settings.VolumeLevels[_busName] = newDb;
        } // end SetToValue()
    } // end class
} // end namespace 