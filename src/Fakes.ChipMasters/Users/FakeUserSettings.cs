using ChipMasters.User;

namespace Fakes.ChipMasters.Users
{
    public sealed class FakeUserSettings : IUserSettings
    {
        public IDictionary<string, float> VolumeLevels { get; } = new Dictionary<string, float>();
        public float BrightnessLevel { get; set; } = 1.0f;
    } // end class
} // end namespace
