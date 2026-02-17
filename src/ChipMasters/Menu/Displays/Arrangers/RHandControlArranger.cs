using ChipMasters.GodotWrappers;
using System;
using System.Collections.Generic;
using System.Numerics;
using Tests.ChipMasters.Util;

namespace ChipMasters.Menu.Displays.Arrangers
{
    /// <summary>
    /// <see cref="IArranger{T}"/> for setting a series of <see cref="IControl"/>s anchors such that they lay within a rectangular bound as cards would on a table.
    /// </summary>
    public sealed class RHandControlArranger : IArranger<IControl>
    {
        private readonly Vector2 _displayOffset;
        private readonly float _displayWidth;
        private readonly Vector2 _cardSize;
        private readonly float _maximalSpacing;



        /// <summary>
        /// 
        /// </summary>
        /// <param name="displayOffset">From top left of screen, position on screen from top left of card bounts, expressed as a percentage of screen</param>
        /// <param name="displayWidth">Width of area cards are displayed in.</param>
        /// <param name="cardSize">Idealized card proportions as percentages of screen.</param>
        /// <param name="maximalSpacing">Maximal horizontal distance between cards.</param>
        public RHandControlArranger(
            Vector2 displayOffset, float displayWidth,
            Vector2 cardSize, float maximalSpacing)
        {
            if (displayWidth < cardSize.X)
                throw new ArgumentException();

            _displayOffset = displayOffset;
            _displayWidth = displayWidth;
            _cardSize = cardSize;
            _maximalSpacing = maximalSpacing;
        } // end ctor



        /// <inheritdoc/>
        public void Arrange(IReadOnlyList<IControl> elements)
        {
            int cc = elements.Count;
            float cardsWidth = cc * _cardSize.X; // total width all cards take up without overlapping
            float emptyWidth = _displayWidth - cardsWidth;

            float start;
            float step;

            if (emptyWidth.ApproximatelyLessThan(0)) // cards take up more than display width, do overlap backing
            {
                start = _displayOffset.X;
                // one card will always be against start of bounds, one against end, all others step equally between the two.
                // Thus the display width minus card width is the space to step over. dividing the space by the card count - 1 gives the step size per each card.(first card does not step, as we said)
                step = (_displayWidth - _cardSize.X) / (float)(cc - 1);
            }
            else // width partially occupied, do spaced packing
            {
                float maximalSpacingWidth = (cc - 1) * _maximalSpacing; // space between cards at maximum


                float intercardSpace = maximalSpacingWidth <= emptyWidth // maximal spacing to be used if true, means it takes less than or equal to the free space
                    ? _maximalSpacing
                    : emptyWidth / (float)(cc - 1);

                float occupiedWidth = cardsWidth + (intercardSpace * (cc - 1));
                float freeWidth = _displayWidth - occupiedWidth;
                start = _displayOffset.X + (freeWidth / 2f); // (freeWidth / 2f) is the amount needed to center it
                step = _cardSize.X + intercardSpace;
            }

            foreach (IControl control in elements)
            {
                control.AnchorTop = _displayOffset.Y;
                control.AnchorBottom = _displayOffset.Y + _cardSize.Y;
                control.AnchorLeft = start;
                control.AnchorRight = start + _cardSize.X;
                start += step;
            }
        } // end ArrangeDisplay()
    } // end class
} // end namespace