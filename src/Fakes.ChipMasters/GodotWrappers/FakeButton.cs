using ChipMasters.GodotWrappers;
using Godot;

namespace Fakes.ChipMasters.GodotWrappers
{
    public sealed class FakeButton : IButton
    {
        public bool Visible { get; set; }
        public bool ButtonPressed { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool Disabled { get; set; }
        public string Text { get; set; } = "";
        public float AnchorTop { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public float AnchorBottom { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public float AnchorLeft { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public float AnchorRight { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Material Material { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string TooltipText { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Control.MouseFilterEnum MouseFilter { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public event Action? ButtonDown;
        public event Action? Pressed;



        /// <summary>
        /// Raise <see cref="Pressed"/> event.
        /// </summary>
        public void Press() => Pressed?.Invoke();

    } // end class
} // end namespace