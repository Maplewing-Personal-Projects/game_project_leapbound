using System;
using System.Linq;
using LaYumba.Functional;
using Maplewing.LeapBound.Engine.Data;

namespace Maplewing.LeapBound.Engine
{
    public class GamePlayEngine
    {
        public class State
        {
            public Player Player;
            public long Money;
            public IItem[] Items;
        }

        private const float FIRST_ITEM_DISTANCE = 10f;

        public State CurrentState { get; private set; }

        private ItemManager _itemManager;

        public GamePlayEngine(State state)
        {
            CurrentState = state;
            _itemManager = new ItemManager(state.Player.Position.X + FIRST_ITEM_DISTANCE);
        }

        public State Update(float deltaTime)
        {
            CurrentState = _itemManager.UpdateState(CurrentState);
            return CurrentState = CurrentState.With(
                state => state.Player,
                CurrentState.Player.Move(deltaTime));
        }

        public State ExecuteCommand(ICommand command)
            => CurrentState = command.Execute(CurrentState);
    }
}