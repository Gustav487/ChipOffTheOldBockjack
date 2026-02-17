using GSR.EnDecic;

namespace Fakes.ChipMasters.IO
{
    public sealed class FakeStreamDecoder : IStreamDecoder<string>
    {
        public bool DecodeBoolean(string stream, CoderSettings settings)
        {
            throw new NotImplementedException();
        }

        public byte DecodeByte(string stream, CoderSettings settings)
        {
            throw new NotImplementedException();
        }

        public decimal DecodeDecimal(string stream, CoderSettings settings)
        {
            throw new NotImplementedException();
        }

        public double DecodeDouble(string stream, CoderSettings settings)
        {
            throw new NotImplementedException();
        }

        public short DecodeInt16(string stream, CoderSettings settings)
        {
            throw new NotImplementedException();
        }

        public int DecodeInt32(string stream, CoderSettings settings)
        {
            throw new NotImplementedException();
        }

        public long DecodeInt64(string stream, CoderSettings settings)
        {
            throw new NotImplementedException();
        }

        public IList<string> DecodeList(string stream, CoderSettings settings)
        {
            throw new NotImplementedException();
        }

        public IDictionary<string, string> DecodeMap(string stream, CoderSettings settings)
        {
            throw new NotImplementedException();
        }

        public U? DecodeNullableReference<U>(string stream, IDecoder<U> elementDecoder, CoderSettings settings) where U : class
        {
            return null;
        }

        public U? DecodeNullableValue<U>(string stream, IDecoder<U> elementDecoder, CoderSettings settings) where U : struct
        {
            throw new NotImplementedException();
        }

        public float DecodeSingle(string stream, CoderSettings settings)
        {
            throw new NotImplementedException();
        }

        public string DecodeString(string stream, CoderSettings settings)
        {
            throw new NotImplementedException();
        }
    }
}
