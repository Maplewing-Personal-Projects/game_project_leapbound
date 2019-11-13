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
            public IItem[] Items = new IItem[0];
            public IEnemy[] Enemies = new IEnemy[0];
            public IBullet[] Bullets = new IBullet[0];
        }

        private const float FIRST_ITEM_DISTANCE = 10f;
        private const float FIRST_ENEMY_DISTANCE = 15f;

        public State CurrentState { get; private set; }

        private ItemManager _itemManager;
        private EnemyManager _enemyManager;

        public GamePlayEngine(State state)
        {
            CurrentState = state;
            _itemManager = new ItemManager(state.Player.AreaRange.Position.X + FIRST_ITEM_DISTANCE);
            _enemyManager = new EnemyManager(state.Player.AreaRange.Position.X + FIRST_ITEM_DISTANCE);
        }

        public State Update(float deltaTime)
        {
            CurrentState = _itemManager.UpdateState(CurrentState);
            CurrentState = _enemyManager.UpdateState(CurrentState);
            CurrentState = CurrentState.With(
                state => state.Player,
                CurrentState.Player.Move(deltaTime));
            return CurrentState = CurrentState.With(
                state => state.Bullets,
                CurrentState.Bullets.Select(bullet => bullet.Move(deltaTime)).ToArray());
        }

        public State ExecuteCommand(ICommand command)
            => CurrentState = command.Execute(CurrentState);
    }
}