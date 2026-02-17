using ChipMasters.Games.Matches;
using ChipMasters.Games.Sessions;
using Fakes.ChipMasters.Games.Matches;
using GSR.Utilic.Generic;

namespace Fakes.ChipMasters.Games.Sessions
{
    public class FakeSession : ISession
    {
        public IMatch Match { get; private set; }

        public bool IsConcluded => throw new NotImplementedException();

        public event Action? OnMatchChanged;
        public event Action? OnConcluded;

        private readonly IQueue<IMatch> _queue;



        public FakeSession(params IMatch[] values)
        {
            _queue = new GSR.Utilic.Generic.Queue<IMatch>();
            values.ForEvery((x) => _queue.Enqueue(x));
            if (_queue.Count > 0)
                Match = _queue.Dequeue();
            else
                Match = new FakeMatch();
        } // end ctor



        public void PlayAgain(int bet)
        {
            Match = _queue.Dequeue();
            OnMatchChanged?.Invoke();
        } // end PlayAgain()
    } // end class
} // end namespace