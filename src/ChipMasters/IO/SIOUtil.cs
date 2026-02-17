using GSR.EnDecic;
using GSR.EnDecic.EnDecs;
using GSR.Jsonic.Formatting;
using GSR.Utilic.Generic;
using System;
using System.Linq;

namespace ChipMasters.IO
{
    /// <summary>
    /// IO related uilities and extensions.
    /// </summary>
    public static class SIOUtil
    {
        /// <summary>
        /// Application standard <see cref="CoderSettings"/>.
        /// </summary>
        public const CoderSettings CODING_SETTINGS =
#if EXPORTRELEASE
            CoderSettings.SIGNIFICANT_ONLY;
#else
            CoderSettings.DEFAULT;
#endif
        /// <summary>
        /// Application standard <see cref="JsonFormatting"/>.
        /// </summary>
        public static readonly JsonFormatting JSON_FORMATTING =
#if EXPORTRELEASE
        JsonFormatting.COMPRESSED;
#else
        new();
#endif




        /// <summary>
        /// <see cref="IEnDec{T}"/> for encoding types in the current app domain.
        /// </summary>
        public static readonly IEnDec<Type> TYPE_ENDEC = new RTypeEnDec();

        /// <summary>
        /// <see cref="IEnDec{T}"/> for <see cref="DateTime"/>s. Codes only to the seconds. Decode back to local time
        /// </summary>
        public static readonly IEnDec<DateTime> DATE_TIME_ENDEC = EnDecUtil.INT_64
            .Map(
                (x) => new DateTimeOffset(x).ToUnixTimeSeconds(),
                (x) => DateTimeOffset.FromUnixTimeSeconds(x).DateTime.ToLocalTime());

        /// <summary>
        /// Create an <see cref="RRegistryEnDec{TKey, TValue}"/>.
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="register"></param>
        /// <param name="keyEnDec"></param>
        /// <returns></returns>
        public static IEnDec<TValue> RegistryEnDec<TKey, TValue>(this IBijectiveDictionary<TKey, TValue> register, IEnDec<TKey> keyEnDec)
            where TKey : notnull
            where TValue : notnull
            => new RRegistryEnDec<TKey, TValue>(register, keyEnDec);

        /// <summary>
        /// Create a <see cref="RTypeDataEnDec{TType, TData}"/> based of a <see cref="IBijectiveDictionary{TKey1, TKey2}"/> type register.
        /// </summary>
        /// <typeparam name="TData"></typeparam>
        /// <param name="register"></param>
        /// <returns></returns>
        public static IEnDec<TData> TypeRegistryEnDec<TData>(
            this IBijectiveDictionary<string, IType<TData>> register)
            => new RDefaultableTypeDataEnDec<IType<TData>, TData>(
                register.RegistryEnDec(EnDecUtil.STRING),
                (x) => register.Select(y => y.Value).First(y => y.IsTypeOf(x)),
                (x) => x.EnDec,
                (x) => x.Instantiate(),
                (x, y) => x.IsDefault(y));

        /// <summary>
        /// Create a <see cref="RTypeDataEnDec{TType, TData}"/> based of a <see cref="IBijectiveDictionary{TKey1, TKey2}"/> type register.
        /// </summary>
        /// <typeparam name="TType"></typeparam>
        /// <typeparam name="TData"></typeparam>
        /// <param name="register"></param>
        /// <param name="typeGetter"></param>
        /// <returns></returns>
        public static IEnDec<TData> TypeRegistryEnDec<TType, TData>(
            this IBijectiveDictionary<string, TType> register,
            Func<TData, TType> typeGetter)

            where TType : notnull, IType<TData>

            => new RDefaultableTypeDataEnDec<TType, TData>(
                register.RegistryEnDec(EnDecUtil.STRING), typeGetter, (x) => x.EnDec, (x) => x.Instantiate(), (x, d) => x.IsDefault(d));



        /// <summary>
        /// Convert an <see cref="IEnDec{T}"/> of type <typeparamref name="TFrom"/> to less derived type <typeparamref name="TTo"/>.
        /// </summary>
        /// <typeparam name="TTo"></typeparam>
        /// <typeparam name="TFrom"></typeparam>
        /// <param name="enDec"></param>
        /// <returns></returns>
        public static IEnDec<TTo> Cast<TTo, TFrom>(this IEnDec<TFrom> enDec)
                where TFrom : TTo
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8603 // Possible null reference return.
            => enDec
            .Map((x) => (TFrom)x, (x) => (TTo)x);
#pragma warning restore CS8603 // Possible null reference return.
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

    } // end class
} // end namespace