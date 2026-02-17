namespace ChipMasters.Menu
{
    /// <summary>
    /// Contract for an object that's visbility's settable.
    /// </summary>
    public interface IVisable // not a typo, Combination of Visible and -Able, if you have a semantically clearer name please do suggest it
    {
        /// <summary>
        /// Is the object currently visible and interactable.
        /// </summary>
        bool Visible { get; set; }
    } // end class
} // end namespace
