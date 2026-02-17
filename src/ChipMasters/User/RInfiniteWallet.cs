using System;

namespace ChipMasters.User
{
    /// <summary>
    /// <see cref="IWallet"/> which's permanently at <see cref="int.MaxValue"/>.
    /// </summary>
    public sealed class RInfiniteWallet : IWallet
    {
        /// <inheritdoc/>
        public int Chips
        {
            get => int.MaxValue;
            set { }
        }

#pragma warning disable CS0067
        /// <inheritdoc/>
        public event Action? OnChipsChanged;
#pragma warning restore CS0067
    } // end class
} // end namespace