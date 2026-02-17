using Godot;

namespace ChipMasters
{
    /// <summary>
    /// Exception indicating a godot object was attempted to be used before <see cref="Node._Ready"/> had been called.
    /// </summary>
    [System.Serializable]
    public class RNotReadyException : System.Exception
    {
        /// <inheritdoc/>
        public RNotReadyException() { }
        /// <inheritdoc/>
        public RNotReadyException(string message) : base(message) { }
        /// <inheritdoc/>
        public RNotReadyException(string message, System.Exception inner) : base(message, inner) { }
        /// <inheritdoc/>
        protected RNotReadyException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    } // end class
} // end namespace
