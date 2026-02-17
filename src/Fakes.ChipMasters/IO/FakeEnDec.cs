using GSR.EnDecic;

namespace Fakes.ChipMasters.IO
{
    public sealed class FakeEnDec<T> : IEnDec<T>
    {
        public U Encode<U>(IStreamEncoder<U> streamEncoder, T data, CoderSettings settings) => throw new NotImplementedException();

        public T Decode<U>(IStreamDecoder<U> streamDecoder, U stream, CoderSettings settings) => throw new NotImplementedException();
    } // end class
} // end namespace
