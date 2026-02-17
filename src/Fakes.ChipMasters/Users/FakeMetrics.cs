using ChipMasters.Games.Matches;
using ChipMasters.User;

namespace Fakes.ChipMasters.Users
{
    public class FakeMetrics : IMetrics
    {
        public IReadOnlyList<VMatchRecord> MatchHistory { get; } = new List<VMatchRecord>();
        public readonly Dictionary<string, int> _stats = new();

        // Initialize with a default win ratio (adjust as needed for your tests).
        public VWinRatio WinRatio => new VWinRatio(
            GetStat(SStats.WINS),
            GetStat(SStats.TIES),
            GetStat(SStats.LOSSES));

        public IReadOnlyList<VChipRecord> ChipHistory { get; } = new List<VChipRecord>();

        public IReadOnlyDictionary<string, int> Stats { get; } = new Dictionary<string, int>();


        public event Action? OnConcluded;
        public event Action? OnStatChanged;



        public void RecordChipCount(int chipCount)
        {
            //throw new NotImplementedException();
        }

        public void RecordMatch(IMatch match)
        {
            // throw new NotImplementedException();
        }

        public int GetStat(string stat)
        {
            return _stats.TryGetValue(stat, out var value) ? value : 0;
        }

        public void SetStat(string stat, int value)
        {
            _stats[stat] = value;
            OnStatChanged?.Invoke();
        }
    } // end class
} // end namespace