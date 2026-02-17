using ChipMasters.GodotWrappers;
using ChipMasters.User;
using System.Text;

namespace ChipMasters.Menu.Displays.ChipRecords
{
    /// <summary>
    /// <see cref="VChipRecord"/> <see cref="ALabelDisplay{T}"/>, writes time amount and diff column aligned by maximum possible width..
    /// </summary>
    public sealed class RChipRecordDisplay : ALabelDisplay<VChipRecord?>
    {
        private const int MAX_DT_LENGTH = 22;
        private const int MAX_CC_LENGTH = 10;



        /// <inheritdoc/>
        public RChipRecordDisplay(ILabel label) : base(label)
        { } // end ctor



        /// <inheritdoc/>
        protected override string ToString(VChipRecord? displaying)
        {
            StringBuilder sb = new();
            // Format timestamp with consistent spacing
            string timestamp = displaying!.Value.Timestamp.ToString("M/d/yyyy h:mm:ss tt");
            // Ensure consistent spacing between AM/PM and colon
            timestamp = timestamp.Replace("AM ", "AM:");
            timestamp = timestamp.Replace("PM ", "PM:");
            sb.Append(timestamp);
            sb.Append(": ");
            int dtL = sb.Length;

            sb.Append(' ', ((MAX_DT_LENGTH + 1) - dtL) + 1); // add spaces to create a constant width before current chip count

            int preC = sb.Length;
            sb.Append(displaying.Value.Count);

            int ccL = sb.Length - preC;
            int? d = displaying.Value.Delta;
            if (d is not null)
            {
                sb.Append(' ', (MAX_CC_LENGTH - ccL) + 1); // add spaces to create a constant width before delta
                sb.Append(d >= 0
                    ? $"(+{d})"
                    : $"({d})");
            }

            return sb.ToString();
        } // end ToString();
    } // end class
} // end namespace