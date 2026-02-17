using ChipMasters.GodotWrappers;
using Godot;

namespace Fakes.ChipMasters.GodotWrappers
{
    public sealed class FakeControl : IControl
    {
        public bool Visible { get; set; }
        public float AnchorTop { get; set; }
        public float AnchorBottom { get; set; }
        public float AnchorLeft { get; set; }
        public float AnchorRight { get; set; }
        public Material Material { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string TooltipText { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Control.MouseFilterEnum MouseFilter { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    } // end class
} // end namespace