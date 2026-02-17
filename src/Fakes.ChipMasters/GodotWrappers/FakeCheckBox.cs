using ChipMasters.GodotWrappers;
using Godot;

namespace Fakes.ChipMasters.GodotWrappers
{
    public sealed class FakeCheckBox : ICheckBox
    {
        public bool ButtonPressed { get; set; }
        public bool Visible { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool Disabled { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Text { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public float AnchorTop { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public float AnchorBottom { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public float AnchorLeft { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public float AnchorRight { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Material Material { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string TooltipText { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Control.MouseFilterEnum MouseFilter { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public event Action? ButtonDown;
        public event Action? Pressed;
    } // end class
} // end namespace