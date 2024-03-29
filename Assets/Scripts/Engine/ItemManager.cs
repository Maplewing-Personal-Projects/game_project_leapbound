﻿using System;
using System.Collections.Generic;
using System.Linq;
using LaYumba.Functional;
using Maplewing.LeapBound.Engine.Data;

namespace Maplewing.LeapBound.Engine
{
    public class ItemManager
    {
        private const float ITEM_DISTANCE = 15f;
        private const float MIN_ITEM_Y = 0f;
        private const float MAX_ITEM_Y = 2f;
        private float _initialItemPositionX;
        private int _currentUpdatedItemIndex = 0;

        public ItemManager(float initialItemPositionX)
            => _initialItemPositionX = initialItemPositionX;

        public GamePlayEngine.State UpdateState(GamePlayEngine.State currentState, float deltaTime)
        {
            var items = currentState.Items;
            var state = currentState;
            var playerMoveRange = GamePlayEngine.GetMoveRange(state.Player.Move(-deltaTime).AreaRange, state.Player.AreaRange);
            var remainItems = new List<IItem>();
            foreach (var item in items)
            {
                if (item.AreaRange.IsIntersect(playerMoveRange))
                {
                    state = item.Get(state);
                }
                else if(item.AreaRange.Position.X >= state.PlayRange.LeftTopPoint.X)
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
            => currentState.Player.AreaRange.Position.X > _initialItemPositionX + _currentUpdatedItemIndex * (ITEM_DISTANCE - 1) ?
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
            => new CoinItem(new Vector2D(currentItemDistance + ITEM_DISTANCE, RandomValue.GetFloat(MIN_ITEM_Y, MAX_ITEM_Y)));

    }


}