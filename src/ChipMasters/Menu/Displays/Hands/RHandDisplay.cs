using ChipMasters.Cards;
using ChipMasters.GodotWrappers;
using ChipMasters.Menu.Displays.Arrangers;
using ChipMasters.Menu.Displays.Cards;
using ChipMasters.Menu.Displays.Hand;
using ChipMasters.Util;
using System;
using System.Collections.Generic;

namespace ChipMasters.Menu.SubDisplays
{
    /// <summary>
    /// <see cref="IHandDisplay"/> implementation for laying cards out in arectangular area.
    /// </summary>
    public class RHandDisplay : AHandDisplay
    {
        /// <summary>
        /// Card displays currently being shown.
        /// </summary>
        protected readonly List<IControlCardDisplay> _activeDisplay = new();

        private readonly IPool<IControlCardDisplay> _cardDisplayPool;
        private readonly IArranger<IControl> _arranger;
        private readonly Action<INode> _addChild;
        private readonly Action<INode> _removeChild;



        /// <inheritdoc/>
        public RHandDisplay(IPool<IControlCardDisplay> cardDisplayPool,
            IArranger<IControl> arranger,
            Action<INode> addChild, Action<INode> removeChild)
        {
            _cardDisplayPool = cardDisplayPool.AssertNotNull();
            _arranger = arranger.AssertNotNull();
            _addChild = addChild.AssertNotNull();
            _removeChild = removeChild.AssertNotNull();
        } // end ctor



        /// <inheritdoc/>
        protected override void RefreshDisplay(ICard addedCard)
        {
            AddDisplayFor(addedCard);
            _arranger.Arrange(_activeDisplay);
        } // end RefreshDisplay

        /// <inheritdoc/>
        protected override void RefreshDisplay()
        {
            ClearDisplay();
            if (Hand is not null)
                SetDisplay();
        } // end RefreshDisplay()

        private void ClearDisplay()
        {
            foreach (IControlCardDisplay cd in _activeDisplay)
            {
                _removeChild(cd);
                cd.Visible = false;
                _cardDisplayPool.Release(cd);
            }
            _activeDisplay.Clear();
        } // end ClearDisplay()

        private void SetDisplay()
        {
            PopulateDisplay();
            _arranger.Arrange(_activeDisplay);
        } // end SetDisplay()

        private void PopulateDisplay()
        {
            foreach (ICard card in Hand.AssertNotNull())
                AddDisplayFor(card);
        } // end PopulateDisplay()

        private void AddDisplayFor(ICard card)
        {
            IControlCardDisplay cd = _cardDisplayPool.Get();
            cd.Display = card;
            cd.Visible = true;

            _activeDisplay.Add(cd);
            _addChild(cd);
        } // end addDisplayFor()
    } // end class
} // end namespace
