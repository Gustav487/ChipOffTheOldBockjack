using System;

namespace ChipMasters.User
{
    /// <summary>
    /// Abstract <see cref="IWallet"/> implementation.
    /// </summary>
    public abstract class AWallet : IWallet
    {
        /// <inheritdoc/>
        public int Chips
        {
            get => _chips;
            set
            {
                int cv = ConstrainChips(value);
                if (_chips == cv)
                    return;

                _chips = cv;
                OnChipsChanged?.Invoke();
            }
        } // end Chips
        private int _chips;

        /// <inheritdoc/>
        public event Action? OnChipsChanged;



        /// <inheritdoc/>
        public AWallet(int startingChips)
        {
            Chips = startingChips;
        } // end ctor




        /// <summary>
        /// Called before updating chip count.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected abstract int ConstrainChips(int value);
    } // end class
} // end namespace