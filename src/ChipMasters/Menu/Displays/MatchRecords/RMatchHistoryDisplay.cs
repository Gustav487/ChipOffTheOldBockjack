using ChipMasters.GodotWrappers;
using ChipMasters.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChipMasters.Menu.Displays.MatchRecords
{
    /// <summary>
    /// <see cref="IReadOnlyList{T}"/> of <see cref="VMatchRecord"/> <see cref="IDisplay{T}"/>  that displays the entries column aligned to an <see cref="ILabel"/>. 
    /// </summary>
    public sealed class RMatchHistoryDisplay : ALabelDisplay<IReadOnlyList<VMatchRecord>>
    {
        /// <summary>
        /// Text shown when <see cref="IDisplay{T}.Display"/>'s empty.
        /// </summary>
        public const string NO_DATA = "No data.";



        /// <inheritdoc/>
        public RMatchHistoryDisplay(ILabel label) : base(label) { }  // end ctor



        /// <inheritdoc/>
        protected override string ToString(IReadOnlyList<VMatchRecord> displaying)
        {
            if (displaying.Count == 0)
                return NO_DATA;

            int bL = 1; // bet length
            int pApL = 1; // player appraisal length

            List<Tuple<string, string, string>> data = new();
            foreach (VMatchRecord vmr in displaying)
            {
                string b = vmr.Bet.ToString();
                string pAp = vmr.PlayerResult.ToString();
                string dAp = vmr.DealerResult.ToString();

                bL = Math.Max(b.Length, bL);
                pApL = Math.Max(pAp.Length, pApL);
                data.Add(Tuple.Create(b, pAp, dAp));
            }

            StringBuilder sb = new();
            string format = "${0,-" + bL + "} | {1," + pApL + "} vs {2}";
            for (int i = 0; i < data.Count; i++)
            {
                sb.Append(string.Format(format, data[i].Item1, data[i].Item2, data[i].Item3));
                if (i < data.Count - 1)
                    sb.Append('\r');
            }

            return sb.ToString();
        } // end ToString()
    } // end class
} // end namespace