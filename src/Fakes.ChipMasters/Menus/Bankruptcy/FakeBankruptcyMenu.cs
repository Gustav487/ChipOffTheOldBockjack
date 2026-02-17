using ChipMasters.Menu.Bankruptcy;

namespace Fakes.ChipMasters.Menus.Bankruptcy
{
    public sealed class FakeBankruptcyMenu : IBankruptcyMenu
    {
        public bool Visible { get; set; }

        public event Action? OnOpen;
        public event Action? OnClose;

        public bool WasShown { get; private set; } = false;

        public bool HideBankruptcyBox { get; set; }

        public void ShowBankruptcyBox()
        {
            WasShown = true;
        } // end ShowBankruptcyBox()

        public void Open()
        {
            Visible = true;
        } // end Open()

        public void Close()
        {
            Visible = false;
        } // end Close()
    } // end class
} // end namespace