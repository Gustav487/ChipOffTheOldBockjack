using System.Collections.Generic;

namespace ChipMasters.User
{
    /// <summary>
    /// Simple <see cref="IUserSettings"/> implementation.
    /// </summary>
    public sealed class RUserSettings : IUserSettings
    {
        /// <inheritdoc/>
        public IDictionary<string, float> VolumeLevels { get; } = new Dictionary<string, float>();

        /// <inheritdoc/>
        public float BrightnessLevel { get; set; } = 1.0f;

        /// <inheritdoc/>
        public RUserSettings()
        { } // end ctor

        /// <inheritdoc/>
        public RUserSettings(IDictionary<string, float> volumeLevels, float brightnessLevel = 1.0f)
        {
            VolumeLevels = new Dictionary<string, float>(volumeLevels.AssertNotNull());
            BrightnessLevel = brightnessLevel;
        } // end ctor
    } // end class
} // end namespace