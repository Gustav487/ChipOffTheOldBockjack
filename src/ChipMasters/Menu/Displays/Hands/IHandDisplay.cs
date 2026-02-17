using ChipMasters.Games.Hands;
using System;

namespace ChipMasters.Menu.Displays.Hand
{
    /// <summary>
    /// Contract for an object which renders/displays a <see cref="IHand"/>.
    /// </summary>
    public interface IHandDisplay : IDisposable
    {
        /// <summary>
        /// Hand to display.
        /// </summary>
        public IHand? Hand { get; set; }

    } // end interface
} // end namespace