namespace ChipMasters.Menu.Interactables.Buttons.Closes
{
    /// <summary>
    /// Contract for an object which is closable. Often a popup type menu.
    /// </summary>
    public interface IClosable
    {
        /// <summary>
        /// Close the object, reseting it back to it's default state.
        /// </summary>
        void Close();
    } // end class
} // end namespace