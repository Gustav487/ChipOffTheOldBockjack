using GSR.EnDecic;
using GSR.EnDecic.EnDecs;
using GSR.Utilic.Generic;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ChipMasters.Resources.Materials
{
    /// <summary>
    /// Record for defining an animation.
    /// </summary>
    public struct VAnimData
    {
        /// <summary>
        /// <see cref="IEnDec{T}"/> for coding <see cref="VAnimData"/> instances.
        /// </summary>
        public static readonly IEnDec<VAnimData> ENDEC = EnDecUtil.KeyedEnDecBuilder<string, VAnimData>(EnDecUtil.STRING)
            .Add("frame_duration", EnDecUtil.SINGLE.Ranged(0f, float.MaxValue), (x) => x.FrameDuration)
            .Add("frame_sequence", EnDecUtil.INT_32.Ranged(0, int.MaxValue).ListOf(), (x) => x.FrameSequence.ToList())
            .Build((fd, fs) => new(fd, fs));



        /// <summary>
        /// How long each frame last on screen.
        /// </summary>
        public float FrameDuration { get; }

        /// <summary>
        /// List of frame indices that the animation goes through.
        /// </summary>
        public IReadOnlyList<int> FrameSequence { get; }



        /// <inheritdoc/>
        public VAnimData(float frameDuration, IEnumerable<int> frameSequence)
        {
            if (frameDuration < 0)
                throw new ArgumentOutOfRangeException();
            FrameDuration = frameDuration;
            FrameSequence = frameSequence.AssertNotNull().ForEachS((x) =>
            {
                if (x < 0)
                    throw new ArgumentOutOfRangeException();
            }).ToList();
        } // end ctor
    } // end struct
} // end namespace