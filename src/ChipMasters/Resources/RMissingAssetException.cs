namespace ChipMasters.Resources
{
    /// <inheritdoc/>
    [System.Serializable]
    public class RMissingAssetException : System.Exception
    {
        /// <inheritdoc/>
        public RMissingAssetException() { }
        /// <inheritdoc/>
        public RMissingAssetException(string message) : base(message) { }
        /// <inheritdoc/>
        public RMissingAssetException(string message, System.Exception inner) : base(message, inner) { }
        /// <inheritdoc/>
        protected RMissingAssetException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    } // end class
} // end namespace