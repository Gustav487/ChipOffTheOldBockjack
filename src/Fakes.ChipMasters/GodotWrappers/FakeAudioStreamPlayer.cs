using ChipMasters.GodotWrappers;

namespace Fakes.ChipMasters.GodotWrappers
{
    /// <summary>
    /// Simulates the playing and stopping of sound. Use case in RObjects that implement AudioStreamPlayer node for testing. 
    /// </summary>
    public class FakeAudioStreamPlayer : IAudioStreamPlayer
    {
        public bool Playing { get; set; }


        public void Play(float fromPosition = 0f) => Playing = true;
        public void Stop() => Playing = false;

    } // end class
} // end namespace