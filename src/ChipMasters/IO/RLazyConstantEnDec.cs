using GSR.EnDecic;
using GSR.EnDecic.EnDecs;
using System;

namespace ChipMasters.IO
{
    /// <summary>
    /// <see cref="IEnDec{T}"/> that always outputs null for encoding, and always a constant value when decoding.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class RLazyConstantEnDec<T> : IEnDec<T>
    {
        private readonly Lazy<T> _constant;



        /// <inheritdoc/>
        public RLazyConstantEnDec(Func<T> constant)
            : this(new Lazy<T>(constant)) { } // end ctor

        /// <inheritdoc/>
        public RLazyConstantEnDec(Lazy<T> constant)
        {
            _constant = constant;
        } // end ctor



        /// <inheritdoc/>
        public T Decode<U>(IStreamDecoder<U> streamDecoder, U stream, CoderSettings settings)
        {
            try
            {
                streamDecoder.DecodeNullableReference(stream, EnDecUtil.STRING, settings);
            }
            catch (Exception)
            {
                throw new InvalidOperationException();
            }
            return _constant.Value;
        } // end Decode()

        /// <inheritdoc/>
        public U Encode<U>(IStreamEncoder<U> streamEncoder, T data, CoderSettings settings)
        {
            if (data is not null
                ? !data.Equals(_constant.Value) // data isn't null, and they do not equal
                : _constant.Value is not null) // data is null, _constant is not, they don't equal
                throw new InvalidOperationException();

            return streamEncoder.EncodeNullableReference(null, EnDecUtil.STRING, settings);
        } // end Encode()

    } // end class
} // end namespace