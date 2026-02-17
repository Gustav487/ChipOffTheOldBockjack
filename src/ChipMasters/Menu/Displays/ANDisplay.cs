using ChipMasters.GodotWrappers;

namespace ChipMasters.Menu.Displays
{
    /// <summary>
    /// <see cref="NNode"/> based <see cref="IDisplay{T}"/> abstract class for wrapping another <see cref="IDisplay{T}"/>.
    /// </summary>
    public abstract partial class ANDisplay<T> : NNode, IDisplay<T>
    {
        /// <inheritdoc/>
        public T? Display { get => Inner.Display; set => Inner.Display = value; }



        private IDisplay<T> Inner => _inner ?? throw new RNotReadyException(nameof(Inner));
        private IDisplay<T>? _inner;



        /// <inheritdoc/>
        public override void _Ready()
        {
            base._Ready();
            _inner = ConstructInner();
        } // end _Ready()

        /// <inheritdoc/>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            _inner?.Dispose();
        } // end Dispose()



        /// <summary>
        /// Create the innner <see cref="IDisplay{T}"/> to be wrapped.
        /// </summary>
        /// <returns></returns>
        protected abstract IDisplay<T> ConstructInner();

    } // end class
} // end namespace