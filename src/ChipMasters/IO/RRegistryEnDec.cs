using GSR.EnDecic;
using GSR.Utilic.Generic;

namespace ChipMasters.IO
{
    /// <summary>
    /// Code values by key from a <see cref="IBijectiveDictionary{TKey1, TKey2}"/>.
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public sealed class RRegistryEnDec<TKey, TValue> : IEnDec<TValue>
        where TKey : notnull
        where TValue : notnull
    {
        private readonly IBijectiveDictionary<TKey, TValue> _register;
        private readonly IEnDec<TKey> _keyEnDec;



        /// <inheritdoc/>
        public RRegistryEnDec(IBijectiveDictionary<TKey, TValue> register, IEnDec<TKey> keyEnDec)
        {
            _register = register.AssertNotNull();
            _keyEnDec = keyEnDec;
        } // end ctor



        /// <inheritdoc/>
        public TValue Decode<U>(IStreamDecoder<U> streamDecoder, U stream, CoderSettings settings) => _register[_keyEnDec.Decode(streamDecoder, stream, settings)];

        /// <inheritdoc/>
        public U Encode<U>(IStreamEncoder<U> streamEncoder, TValue data, CoderSettings settings) => _keyEnDec.Encode(streamEncoder, _register.I[data], settings);
    } // end class
} // end namespace