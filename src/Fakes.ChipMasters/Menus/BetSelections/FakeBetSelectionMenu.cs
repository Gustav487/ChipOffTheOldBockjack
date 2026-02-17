using ChipMasters.Games.BetHandlers;
using ChipMasters.Menu.BetSelections;

namespace Fakes.ChipMasters.Menus.BetSelections
{
    public sealed class FakeBetSelectionMenu : IBetSelectionMenu
    {
        public bool _Disposed_ { get; private set; }
        public bool _Open_ { get; set; }


        public VBetRange Range { get; set; }

        public int Selected { get; set; }

        private Action<int>? _submitCallback;

        public bool Visible { get; private set; }



        public void Dispose() => _Disposed_ = true;



        public void Close()
        {
            _submitCallback = null;
            Visible = false;
            _Open_ = false;
        } // end Close()

        public void Open(Action<int> submitCallback)
        {
            _submitCallback = submitCallback;
            Visible = true;
            _Open_ = true;
        } // end Open()

        public void Submit()
        {
            _submitCallback?.Invoke(Selected);
            Close();
        }

    } // end class
} // end namespace