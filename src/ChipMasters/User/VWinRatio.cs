using GSR.EnDecic;
using GSR.EnDecic.EnDecs;
using System;

namespace ChipMasters.User
{
    /// <summary>
    /// Ratio of wins:ties:losses.
    /// </summary>
    public struct VWinRatio
    {
        /// <summary>
        /// <see cref="ENDEC"/>'s key for the <see cref="VWinRatio.Wins"/> property.
        /// </summary>
        public const string WINS_KEY = "wins";

        /// <summary>
        /// <see cref="ENDEC"/>'s key for the <see cref="VWinRatio.Ties"/> property.
        /// </summary>
        public const string TIES_KEY = "ties";

        /// <summary>
        /// <see cref="ENDEC"/>'s key for the <see cref="VWinRatio.Losses"/> property.
        /// </summary>
        public const string LOSSES_KEY = "losses";



        /// <summary>
        /// <see cref="IEnDec{T}"/> for <see cref="VWinRatio"/>s.
        /// </summary>
        public static readonly IEnDec<VWinRatio> ENDEC = EnDecUtil.KeyedEnDecBuilder<string, VWinRatio>(EnDecUtil.STRING)
            .Add(WINS_KEY, EnDecUtil.INT_32.Ranged(0, int.MaxValue), (x) => x.Wins)
            .Add(TIES_KEY, EnDecUtil.INT_32.Ranged(0, int.MaxValue), (x) => x.Ties)
            .Add(LOSSES_KEY, EnDecUtil.INT_32.Ranged(0, int.MaxValue), (x) => x.Losses)
            .Build((w, t, l) => new VWinRatio(w, t, l));



        /// <summary>
        /// Number of wins.
        /// </summary>
        public int Wins { get; }

        /// <summary>
        /// Number of ties.
        /// </summary>
        public int Ties { get; }

        /// <summary>
        /// Number of losses
        /// </summary>
        public int Losses { get; }



        /// <inheritdoc/>
        public VWinRatio(int w, int t, int l)
        {
            if (w < 0 || t < 0 || l < 0)
                throw new ArgumentOutOfRangeException();

            Wins = w;
            Ties = t;
            Losses = l;
        } // end ctor



        /// <inheritdoc/>
        public override string ToString() => $"{Wins}:{Ties}:{Losses}";

    } // end class
} // end namespace