using ChipMasters.GodotWrappers;
using Godot;

namespace Fakes.ChipMasters.GodotWrappers
{
    public sealed class FakeContainer : IContainer
    {
        public bool Visible { get; set; }
        public float AnchorTop { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public float AnchorBottom { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public float AnchorLeft { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public float AnchorRight { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Material Material { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string TooltipText { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Control.MouseFilterEnum MouseFilter { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    } // end class
} // end namespace