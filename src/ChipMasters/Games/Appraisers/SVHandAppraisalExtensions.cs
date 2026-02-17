using ChipMasters.Games.Matches;
using System;

namespace ChipMasters.Games.Appraisers
{
    /// <summary>
    /// Assorted extension methods for <see cref="IMatch"/>.
    /// </summary>
    public static class SVHandAppraisalExtensions
    {
        /// <summary>
        /// Throw an exception if <see cref="VHandAppraisal.State"/> is <see cref="EHandState.Unknown"/>, otherwise return value.
        /// </summary>
        /// <param name="appraisal"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static VHandAppraisal AssertKnown(this VHandAppraisal appraisal)
            => appraisal.State == EHandState.Unknown
            ? throw new InvalidOperationException()
            : appraisal;


    } // end class
} // end namespace