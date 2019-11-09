using System;
using System.Collections.Generic;
using System.Linq;
using LaYumba.Functional;
using Maplewing.LeapBound.Engine.Data;

namespace Maplewing.LeapBound.Engine
{
    public class ItemManager
    {
        private const float ITEM_DISTANCE = 10f;
        private float _initialItemPositionX;
        private int _currentUpdatedItemIndex = 0;

        public ItemManager(float initialItemPositionX)
            => _initialItemPositionX = initialItemPositionX;

        public GamePlayEngine.State UpdateState(GamePlayEngine.State currentState)
        {
            var items = currentState.Items;
            var state = currentState;
            var remainItems = new List<IItem>();
            foreach (var item in items)
            {
                if (item.AreaRange.IsPointIn(state.Player.Position))
                {
                    state = item.Get(state);
                }
                else
                {
                    remainItems.Add(item);
                }
            }

            state = state.With(
                s => s.Items,
                remainItems.ToArray());

            return _ProcessGeneratingItems(state);
        }

        private GamePlayEngine.State _ProcessGeneratingItems(GamePlayEngine.State currentState)
            => currentState.Player.Position.X > _initialItemPositionX + _currentUpdatedItemIndex * (ITEM_DISTANCE - 1) ?
                    _UpdateCurrentUpdatedItemPositionX(_DecideWhetherGeneratingItems(currentState)) :
                    currentState;

        private GamePlayEngine.State _DecideWhetherGeneratingItems(GamePlayEngine.State currentState)
            => RandomValue.GetBool() ?
                    currentState.With(
                        state => state.Items,
                        currentState.Items.Concat(new IItem[] {
                            _GenerateItem(_initialItemPositionX + _currentUpdatedItemIndex * ITEM_DISTANCE)
                        }).ToArray())
                    : currentState;

        private GamePlayEngine.State _UpdateCurrentUpdatedItemPositionX(GamePlayEngine.State currentState)
        {
            ++_currentUpdatedItemIndex;
            return currentState;
        }

        private IItem _GenerateItem(float currentItemDistance)
            => new CoinItem(new Vector2D(currentItemDistance + ITEM_DISTANCE, 0));

    }


}