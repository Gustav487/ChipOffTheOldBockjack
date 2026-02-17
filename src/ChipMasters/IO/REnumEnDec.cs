using GSR.EnDecic;
using System;

namespace ChipMasters.IO
{
    /// <summary>
    /// <see cref="IEnDec{T}"/> for coding enums of type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class REnumEnDec<T> : IEnDec<T>
        where T : Enum
    {
        /// <inheritdoc/>
        public T Decode<U>(IStreamDecoder<U> streamDecoder, U stream, CoderSettings settings)
        {
            T data = (T)Enum.Parse(typeof(T), streamDecoder.DecodeString(stream, settings));
            if (!IsValid(data))
                throw new ArgumentException();

            return data;
        } // end Decode()

        /// <inheritdoc/>
        public U Encode<U>(IStreamEncoder<U> streamEncoder, T data, CoderSettings settings)
        {
            if (!IsValid(data))
                throw new ArgumentException();

            return streamEncoder.EncodeString(data.ToString(), settings);
        } // end Encode()


        private static bool IsValid(T t)
            => Enum.IsDefined(typeof(T), t);
    } // end class
} // end namespace