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
            public Rectangle PlayRange = new Rectangle(Vector2D.ZERO, new Vector2D(20, 20));
            public Player Player;
            public long Score;
            public IItem[] Items = new IItem[0];
            public IEnemy[] Enemies = new IEnemy[0];
            public IBullet[] Bullets = new IBullet[0];
        }

        private const float FIRST_ITEM_DISTANCE = 10f;
        private const float FIRST_ENEMY_DISTANCE = 15f;
        private static readonly Vector2D PLAY_RANGE_PLAYER_DIFF = new Vector2D(4f, 0f);

        public State CurrentState { get; private set; }

        private ItemManager _itemManager;
        private EnemyManager _enemyManager;
        private BulletManager _bulletManager;

        public GamePlayEngine(State state)
        {
            CurrentState = state;
            _itemManager = new ItemManager(state.Player.AreaRange.Position.X + FIRST_ITEM_DISTANCE);
            _enemyManager = new EnemyManager(state.Player.AreaRange.Position.X + FIRST_ITEM_DISTANCE);
            _bulletManager = new BulletManager();
        }

        public State Update(float deltaTime)
        {
            CurrentState = _bulletManager.UpdateState(CurrentState, deltaTime);
            CurrentState = _itemManager.UpdateState(CurrentState, deltaTime);
            CurrentState = _enemyManager.UpdateState(CurrentState, deltaTime);
            CurrentState = CurrentState.With(
                state => state.Player,
                CurrentState.Player.Move(deltaTime));
            return CurrentState = CurrentState.With(
                state => state.PlayRange,
                new Rectangle(
                    CurrentState.Player.AreaRange.Position + PLAY_RANGE_PLAYER_DIFF,
                    CurrentState.PlayRange.Size));
        }

        public State ExecuteCommand(ICommand command)
            => CurrentState = command.Execute(CurrentState);

        public static Rectangle GetMoveRange(Rectangle currentRange, Rectangle nextRange)
        {
            var position = (currentRange.Position + nextRange.Position) / 2f;
            var leftPointX = Math.Min(currentRange.Position.X, nextRange.Position.X) - currentRange.Size.X;
            var rightPointX = Math.Max(currentRange.Position.X, nextRange.Position.X) + currentRange.Size.X;
            var size = new Vector2D(
                (rightPointX - leftPointX) / 2f,
                currentRange.Size.Y);
            return new Rectangle(position, size);
        }
    }
}