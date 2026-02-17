using ChipMasters.GodotWrappers;
using Godot;

namespace Fakes.ChipMasters.GodotWrappers
{
    public sealed class FakeLabel : ILabel
    {
        public string Text { get; set; } = "";

        public bool Visible { get; set; }
        public float AnchorTop { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public float AnchorBottom { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public float AnchorLeft { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public float AnchorRight { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Material Material { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string TooltipText { get; set; } = "";
        public Control.MouseFilterEnum MouseFilter { get; set; } = Control.MouseFilterEnum.Ignore;
    } // end class
} // end namespace