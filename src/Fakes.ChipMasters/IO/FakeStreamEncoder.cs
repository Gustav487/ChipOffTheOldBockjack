using GSR.EnDecic;

namespace Fakes.ChipMasters.IO
{
    public sealed class FakeStreamEncoder : IStreamEncoder<string>
    {
        public string EncodeBoolean(bool data, CoderSettings settings)
        {
            throw new NotImplementedException();
        }

        public string EncodeByte(byte data, CoderSettings settings)
        {
            throw new NotImplementedException();
        }

        public string EncodeDecimal(decimal data, CoderSettings settings)
        {
            throw new NotImplementedException();
        }

        public string EncodeDouble(double data, CoderSettings settings)
        {
            throw new NotImplementedException();
        }

        public string EncodeInt16(short data, CoderSettings settings)
        {
            throw new NotImplementedException();
        }

        public string EncodeInt32(int data, CoderSettings settings)
        {
            throw new NotImplementedException();
        }

        public string EncodeInt64(long data, CoderSettings settings)
        {
            throw new NotImplementedException();
        }

        public string EncodeList(IList<string> data, CoderSettings settings)
        {
            throw new NotImplementedException();
        }

        public string EncodeMap(IDictionary<string, string> data, CoderSettings settings)
        {
            throw new NotImplementedException();
        }

        public string EncodeNullableReference<U>(U? data, IEncoder<U> elementEncoder, CoderSettings settings) where U : class
        {
            return "encoded";
        }

        public string EncodeNullableValue<U>(U? data, IEncoder<U> elementEncoder, CoderSettings settings) where U : struct
        {
            throw new NotImplementedException();
        }

        public string EncodeSingle(float data, CoderSettings settings)
        {
            throw new NotImplementedException();
        }

        public string EncodeString(string data, CoderSettings settings)
        {
            throw new NotImplementedException();
        }
    }
}
