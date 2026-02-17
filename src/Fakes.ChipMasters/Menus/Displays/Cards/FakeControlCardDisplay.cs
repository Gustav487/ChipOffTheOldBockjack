using ChipMasters.Cards;
using ChipMasters.Menu.Displays.Cards;
using Godot;

namespace Fakes.ChipMasters.Menus.Displays.Cards
{
    public sealed class FakeControlCardDisplay : IControlCardDisplay
    {
        public bool? _CardFliped_ { get; private set; }

        public float AnchorTop { get; set; }
        public float AnchorBottom { get; set; }
        public float AnchorLeft { get; set; }
        public float AnchorRight { get; set; }
        public ICard? Display
        {
            get => _card;
            set
            {
                _card = value;
                if (_card is not null)
                {
                    _CardFliped_ = false;
                    _card.OnFlipped += () => _CardFliped_ = true;
                }
                else
                    _CardFliped_ = null;
            }
        }
        private ICard? _card;
        public bool Visible { get; set; }
        public Material Material { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string TooltipText { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Control.MouseFilterEnum MouseFilter { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    } // end class
} // end namespace