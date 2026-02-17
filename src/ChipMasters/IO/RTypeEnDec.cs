using GSR.EnDecic;
using System;
using System.Linq;

namespace ChipMasters.IO
{
    /// <summary>
    /// <see cref="IEnDec{T}"/> for coding <see cref="System.Type"/>s
    /// </summary>
    public sealed class RTypeEnDec : IEnDec<Type>
    {
        /// <inheritdoc/>
        public Type Decode<U>(IStreamDecoder<U> streamDecoder, U stream, CoderSettings settings)
        {
            string fn = streamDecoder.DecodeString(stream, settings);
            return AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany((x) => x.GetTypes())
                .First((x) => x.FullName == fn);
        } // end Decode()

        /// <inheritdoc/>
        public U Encode<U>(IStreamEncoder<U> streamEncoder, Type data, CoderSettings settings)
            => streamEncoder.EncodeString(data.FullName ?? throw new InvalidOperationException(), settings);

        /*        /// <inheritdoc/>
                public Type Decode<U>(IStreamDecoder<U> streamDecoder, U stream, CoderSettings settings)
                {
                    string aqn = streamDecoder.DecodeString(stream, settings);
                    return AppDomain.CurrentDomain.GetAssemblies()
                        .SelectMany((x) => x.GetTypes())
                        .First((x) => x.AssemblyQualifiedName == aqn);
                } // end Decode()

                /// <inheritdoc/>
                public U Encode<U>(IStreamEncoder<U> streamEncoder, Type data, CoderSettings settings)
                    => streamEncoder.EncodeString(data.AssemblyQualifiedName ?? throw new InvalidOperationException(), settings);*/
    } // end class
} // end namespace