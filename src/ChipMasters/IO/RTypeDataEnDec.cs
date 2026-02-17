using GSR.EnDecic;
using GSR.EnDecic.EnDecs;
using GSR.Utilic.Generic;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace ChipMasters.IO
{
    /// <summary>
    /// <see cref="IEnDec{T}"/> implementation for coding data distinctly base on an accoiated 'type'.
    /// </summary>
    /// <typeparam name="TType"></typeparam>
    /// <typeparam name="TData"></typeparam>
    public class RTypeDataEnDec<TType, TData> : IEnDec<TData>
    {
        /// <summary>
        /// <see cref="IEnDec{T}"/> for encoding TType instances.
        /// </summary>
        protected readonly IEnDec<TType> _typeEnDec;
        /// <summary>
        /// Function to get the type of a TData instance.
        /// </summary>
        protected readonly Func<TData, TType> _typeGetter;
        /// <summary>
        /// Function to find the TData <see cref="IEnDec{T}"/> from the TType of a TData instance.
        /// </summary>
        protected readonly Func<TType, IEnDec<TData>> _enDecLookup;



        /// <inheritdoc/>
        public RTypeDataEnDec(
            IEnDec<TType> typeEnDec,
            Func<TData, TType> typeGetter,
            Func<TType, IEnDec<TData>> enDecLookup)
        {
            _typeEnDec = typeEnDec.AssertNotNull();
            _typeGetter = typeGetter.AssertNotNull();
            _enDecLookup = enDecLookup.AssertNotNull();
        } // end ctor



        /// <inheritdoc/>
        public virtual TData Decode<U>(IStreamDecoder<U> streamDecoder, U stream, CoderSettings settings)
        {
            IDictionary<U, U> data = streamDecoder.DecodeMap(stream, settings);
            if (data.Count != 2)
                throw new ArgumentException();

            IDictionary<string, U> d2 = data
                .Select((x) => KeyValuePair.Create(
                    EnDecUtil.STRING.Decode(streamDecoder, x.Key, settings),
                    x.Value))
                .ToImmutableDictionary();

            TType type = _typeEnDec.Decode(streamDecoder, d2["type"], settings);
            return _enDecLookup(type).Decode(streamDecoder, d2["data"], settings);
        } // end Decode()

        /// <inheritdoc/>
        public virtual U Encode<U>(IStreamEncoder<U> streamEncoder, TData data, CoderSettings settings)
        {
            TType type = _typeGetter(data);
            return streamEncoder.EncodeMap(new OrderedDictionary<U, U>()
            {
                { EnDecUtil.STRING.Encode(streamEncoder, "type", settings),
                    _typeEnDec.Encode(streamEncoder, type, settings) },
                { EnDecUtil.STRING.Encode(streamEncoder, "data", settings),
                    _enDecLookup(type).Encode(streamEncoder, data, settings) }
            }, settings);
        } // end Encode()
    } // end class
} // end namespace