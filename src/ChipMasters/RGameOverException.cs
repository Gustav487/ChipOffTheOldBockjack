using System;

namespace ChipMasters
{
    /// <summary>
    /// Exception indicating game was ended, yet further actions were taken.
    /// </summary>
    [Serializable]
    public class RGameOverException : Exception
    {
        /// <inheritdoc/>
        public RGameOverException() { }
        /// <inheritdoc/>
        public RGameOverException(string message) : base(message) { }
        /// <inheritdoc/>
        public RGameOverException(string message, Exception inner) : base(message, inner) { }
        /// <inheritdoc/>
        protected RGameOverException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    } // end class
} // end namespace
