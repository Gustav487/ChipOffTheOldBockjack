using ChipMasters.GodotWrappers;
using ChipMasters.User;
using Godot;
using System;
using System.Collections.Generic;

namespace ChipMasters.Audio
{
    /// <summary>
    /// <see cref="NNode"/> for applying the user's settings after they're loaded.
    /// </summary>
    public sealed partial class NAudioSettingsManager : NNode
    {
        /// <summary>
        /// Single instance of the class.
        /// </summary>
        public static NAudioSettingsManager INSTANCE => s_instance ?? throw new RNotReadyException();
        private static NAudioSettingsManager? s_instance;

        /// <inheritdoc/>
        public NAudioSettingsManager()
        {
            if (s_instance is not null)
                throw new InvalidOperationException();

            s_instance = this;
        } // end ctor



        /// <inheritdoc/>
        public override void _Ready()
        {
            foreach (KeyValuePair<string, float> kvp in RUser.INSTANCE.Settings.VolumeLevels)
                AudioServer.SetBusVolumeDb(AudioServer.GetBusIndex(kvp.Key), kvp.Value);
        } // ApplySavedAudioSettings()
    } // end class
} // end namespace