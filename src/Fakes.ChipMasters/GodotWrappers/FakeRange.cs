using ChipMasters.GodotWrappers;

namespace Fakes.ChipMasters.GodotWrappers
{
    public sealed class FakeRange : IRange
    {
        public double MinValue { get; set; }
        public double MaxValue { get; set; }
        public double Value
        {
            get => _value;
            set
            {
                _value = value;
                ValueChanged?.Invoke(_value);
            }
        }
        private double _value;

        public event Godot.Range.ValueChangedEventHandler? ValueChanged;
    } // end class
} // end namespace