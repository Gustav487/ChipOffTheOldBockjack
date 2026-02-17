using ChipMasters.Games.Appraisers;
using ChipMasters.Games.Matches;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ChipMasters.User
{
    /// <summary>
    /// Simple <see cref="IMetrics"/> implementation.
    /// </summary>
    public sealed class RMetrics : IMetrics
    {
        /// <summary>
        /// Maximum number of chip count changes retained.
        /// </summary>
        public const int CHIP_HISTORY_LENGTH = 10;

        /// <summary>
        /// Maximum number of match results that're retained.
        /// </summary>
        public const int MATCH_HISTORY_LENGTH = 10;



        /// <inheritdoc/>
        public IReadOnlyList<VChipRecord> ChipHistory => _chipCountHistory;
        private readonly List<VChipRecord> _chipCountHistory = new();

        /// <inheritdoc/>
        public IReadOnlyList<VMatchRecord> MatchHistory => _matchHistory;
        private readonly List<VMatchRecord> _matchHistory = new();

        /// <inheritdoc/>
        public VWinRatio WinRatio => new(GetStat(SStats.WINS), GetStat(SStats.TIES), GetStat(SStats.LOSSES));

        /// <inheritdoc/>
        public IReadOnlyDictionary<string, int> Stats => _stats;
        private readonly Dictionary<string, int> _stats = new();

        /// <inheritdoc/>
        public event Action? OnStatChanged;



        private readonly Func<DateTime> _timeFunc = () => DateTime.Now;




        /// <inheritdoc/>
        public RMetrics() { } // end ctor

        /// <inheritdoc/>
        public RMetrics(Func<DateTime> timeFunc)
        {
            _timeFunc = timeFunc.AssertNotNull();
        } // end ctor

        /// <inheritdoc/>
        public RMetrics(IEnumerable<VMatchRecord> history, IEnumerable<VChipRecord> chipHistory, IDictionary<string, int> stats)
        {
            _matchHistory = history.AssertNotNull().Select((x) => x.AssertNotNull()).ToList();
            _chipCountHistory = chipHistory.ToList();
            _stats = new Dictionary<string, int>(stats.AssertNotNull());

            ClampMatchHistory();
            ClampCCHistory();
        } // end ctor



        /// <inheritdoc/>
        public int GetStat(string statName)
        {
            if (_stats.TryGetValue(statName, out int s))
                return s;

            return 0;
        } // end GetStat()

        /// <inheritdoc/>
        public void SetStat(string stat, int value)
        {
            if (_stats.ContainsKey(stat) && _stats[stat] == value)
                return;

            _stats[stat] = value;
            OnStatChanged?.Invoke();
        } // SetStat()

        /// <summary>
        /// Records a match and updates metrics based on the match outcome.
        /// </summary>
        /// <param name="match">The match to record.</param>
        public void RecordMatch(IMatch match)
        {
            VHandAppraisal pApp = match.AppraisePlayerHand(false).AssertKnown();
            VHandAppraisal dApp = match.AppraiseDealerHand(false).AssertKnown();

            _matchHistory.Add(new VMatchRecord(match.Bet, pApp, dApp));
            ClampMatchHistory();



            if (pApp > dApp)
            {
                this.IncrementStat(SStats.WINS);

                if (match.PlayerHand.Count == 4)
                    this.IncrementStat(SStats.FOUR_CARD_WINS);
                else if (match.PlayerHand.Count == 5)
                    this.IncrementStat(SStats.FIVE_CARD_WINS);
                else if (match.PlayerHand.Count == 6)
                    this.IncrementStat(SStats.SIX_CARD_WINS);

                if (pApp.TotalValue == 17)
                    this.IncrementStat(SStats.STAND_SEVENTEEN_WINS);
            }
            else if (pApp < dApp)
                this.IncrementStat(SStats.LOSSES);
            else
                this.IncrementStat(SStats.TIES);



            if (pApp.State == EHandState.Blackjack)
                this.IncrementStat(SStats.BLACKJACKS);
            else if (pApp.State == EHandState.Bust)
                this.IncrementStat(SStats.BUSTS);



            this.IncrementStat(SStats.GAMES_PLAYED);
            this.IncreaseStat(SStats.BETTED_AMOUNT, match.Bet);
            if (match.Bet >= 5000)
                this.IncrementStat(SStats.HIGH_BETS);
        } // end RecordMatch()

        /// <summary>
        /// Records the current chip count.
        /// </summary>
        public void RecordChipCount(int chipCount)
        {
            _chipCountHistory.Add(new VChipRecord(
                _timeFunc(),
                chipCount,
                _chipCountHistory.Count > 0
                    ? chipCount - _chipCountHistory[^1].Count
                    : null
                ));

            SetStat(SStats.MAX_BALANCE, Math.Max(chipCount, GetStat(SStats.MAX_BALANCE)));
            ClampCCHistory();
        } // end RecordChipCount()

        private void ClampMatchHistory()
        {
            int d = _matchHistory.Count - MATCH_HISTORY_LENGTH;
            if (d > 0)
                _matchHistory.RemoveRange(0, d);
        } // end ClampChipHistory()

        private void ClampCCHistory()
        {
            int d = _chipCountHistory.Count - CHIP_HISTORY_LENGTH;
            if (d > 0)
                _chipCountHistory.RemoveRange(0, d);
        } // end ClampChipHistory()
    }// end class
}// end namespace
