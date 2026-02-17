using ChipMasters.Menu.Displays;

namespace Fakes.ChipMasters.Menus.Displays
{
    public sealed class FakeDisplay<T> : IDisplay<T>
    {
        public T? Display { get; set; }



        public void Dispose()
        {
            throw new NotImplementedException();
        } // end Dispose()
    } // end class
} // end namespace