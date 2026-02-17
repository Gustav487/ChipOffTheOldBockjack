using ChipMasters.Games.Appraisers;
using ChipMasters.Games.Hands;
using GSR.Utilic.Generic;

namespace Fakes.ChipMasters.Games.Appraisers
{
    /// <summary>
    /// <see cref="IAppraiser"/> for testing, returns preset appraisals in an order ignoring the hands.
    /// </summary>
    public sealed class FakeOrdinalAppraiser : IAppraiser
    {
        private readonly IQueue<VHandAppraisal> _queue;



        public FakeOrdinalAppraiser(params VHandAppraisal[] values)
        {
            _queue = new GSR.Utilic.Generic.Queue<VHandAppraisal>();
            values.ForEvery((x) => _queue.Enqueue(x));
        } // end ctor



        public VHandAppraisal AppraiseHand(IHand hand, bool includeHidden) => _queue.Dequeue();
    } // end class
} // end namespace