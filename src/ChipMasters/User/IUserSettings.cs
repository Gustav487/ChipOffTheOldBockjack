using GSR.EnDecic;
using GSR.EnDecic.EnDecs;
using System.Collections.Generic;

namespace ChipMasters.User
{
    /// <summary>
    /// A user's settings.
    /// </summary>
    public interface IUserSettings
    {
        /// <summary>
        /// <see cref="IEnDec{T}"/> for coding <see cref="IUserSettings"/> strictly by contract.
        /// </summary>
        public static readonly IEnDec<IUserSettings> ENDEC = EnDecUtil.KeyedEnDecBuilder<string, IUserSettings>(EnDecUtil.STRING)
            .Add("volume_levels", EnDecUtil.SINGLE.MapOf(EnDecUtil.STRING), (x) => x.VolumeLevels)
            .Add("brightness_level", EnDecUtil.SINGLE, (x) => x.BrightnessLevel)
            .Build((vl, bl) => new RUserSettings(vl, bl));

        /// <summary>
        /// Bus names and volume levels.
        /// </summary>
        IDictionary<string, float> VolumeLevels { get; }

        /// <summary>
        /// Screen brightness level (0.5 to 1.0).
        /// </summary>
        float BrightnessLevel { get; set; }
    } // end class
} // end namespace