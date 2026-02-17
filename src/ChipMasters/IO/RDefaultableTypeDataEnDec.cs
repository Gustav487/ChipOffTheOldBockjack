using GSR.EnDecic;
using System;

namespace ChipMasters.IO
{
    /// <summary>
    /// <see cref="IEnDec{T}"/> implementation for coding data distinctly base on an accoiated 'type'.
    /// </summary>
    /// <typeparam name="TType"></typeparam>
    /// <typeparam name="TData"></typeparam>
    public sealed class RDefaultableTypeDataEnDec<TType, TData> : RTypeDataEnDec<TType, TData>
    {
        private readonly Func<TType, TData> _defaultSupplier;
        private readonly Func<TType, TData, bool> _defaultChecker;



        /// <summary>
        /// 
        /// </summary>
        /// <param name="typeEnDec"><see cref="IEnDec{T}"/> for coding <typeparamref name="TType"/> instances.</param>
        /// <param name="typeGetter">Get type for <typeparamref name="TData"/> instance</param>
        /// <param name="enDecLookup">Get endec for the type</param>
        /// <param name="defaultSupplier">Get default value.</param>
        /// <param name="defaultChecker">Is the value in it's default state.</param>
        public RDefaultableTypeDataEnDec(
            IEnDec<TType> typeEnDec,
            Func<TData, TType> typeGetter,
            Func<TType, IEnDec<TData>> enDecLookup,
            Func<TType, TData> defaultSupplier,
            Func<TType, TData, bool> defaultChecker)
            : base(typeEnDec, typeGetter, enDecLookup)
        {
            _defaultSupplier = defaultSupplier.AssertNotNull();
            _defaultChecker = defaultChecker.AssertNotNull();
        } // end ctor



        /// <inheritdoc/>
        public override TData Decode<U>(IStreamDecoder<U> streamDecoder, U stream, CoderSettings settings)
        {
            try
            {
                return base.Decode(streamDecoder, stream, settings);
            }
            catch (Exception e)
            {
                try // if type/data fail check implied case.
                {
                    TType type = _typeEnDec.Decode(streamDecoder, stream, settings);
                    return _defaultSupplier(type);
                }
                catch (Exception e2)
                {
                    throw new AggregateException(e, e2);
                } // end inner catch
            }
        } // end Decode()

        /// <inheritdoc/>
        public override U Encode<U>(IStreamEncoder<U> streamEncoder, TData data, CoderSettings settings)
        {
            TType type = _typeGetter(data);
            if (_defaultChecker(type, data))
                return _typeEnDec.Encode(streamEncoder, type, settings);

            return base.Encode(streamEncoder, data, settings);
        } // end Encode()
    } // end class
} // end namespace