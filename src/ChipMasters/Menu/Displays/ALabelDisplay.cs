using ChipMasters.GodotWrappers;

namespace ChipMasters.Menu.Displays
{
    /// <summary>
    /// <typeparamref name="T"/> <see cref="IDisplay{T}"/> for writting <see cref="Display"/> as a <see cref="string"/> to a <see cref="ILabel"/>. 
    /// </summary>
    public abstract class ALabelDisplay<T> : IDisplay<T>
    {
        /// <summary>
        /// Text shown when <see cref="Display"/>'s null.
        /// </summary>
        public const string UNDEFINED = "N/A";



        /// <inheritdoc/>
        public T? Display
        {
            get => _displaying;
            set
            {
                _displaying = value;
                RefreshDisplay();
            }
        }
        private T? _displaying;

        private readonly ILabel _label;



        /// <inheritdoc/>
        public ALabelDisplay(ILabel label)
        {
            _label = label.AssertNotNull();
            RefreshDisplay();
        } // end ctor

        /// <inheritdoc/>
        public void Dispose() { }


        private void RefreshDisplay()
        {
            if (_displaying is null)
            {
                _label.Text = UNDEFINED;
                return;
            }
            _label.Text = ToString(_displaying);
        } // end RefreshDisplay()

        /// <summary>
        /// Convert <paramref name="displaying"/> into it's nearest string representation.
        /// </summary>
        /// <param name="displaying">The value to be displayed.</param>
        /// <returns></returns>
        protected abstract string ToString(T displaying);
    } // end class
} // end namespace