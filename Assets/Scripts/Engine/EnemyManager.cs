using LaYumba.Functional;
using Maplewing.LeapBound.Engine.Data;
using System.Collections.Generic;
using System.Linq;

namespace Maplewing.LeapBound.Engine
{
    public class EnemyManager
    {
        private const float ENEMY_DISTANCE = 18f;
        private const float INIT_ENEMY_Y = 0f;
        private float _initialEnemyPositionX;
        private int _currentUpdatedEnemyIndex = 0;

        public EnemyManager(float initialEnemyPositionX)
            => _initialEnemyPositionX = initialEnemyPositionX;

        public GamePlayEngine.State UpdateState(GamePlayEngine.State currentState, float deltaTime)
        {
            var state = currentState;
            var playerMoveRange = GamePlayEngine.GetMoveRange(state.Player.Move(-deltaTime).AreaRange, state.Player.AreaRange);
            var enemies = state.Enemies;
            var remainEnemies = new List<IEnemy>();
            foreach (var enemy in enemies)
            {
                if (enemy.AreaRange.IsIntersect(playerMoveRange))
                {
                    state = state.With(
                        s => s.Player,
                        state.Player.BeInjured(enemy.Atk));
                }

                if (enemy.AreaRange.Position.X > state.PlayRange.LeftTopPoint.X)
                {
                    remainEnemies.Add(enemy);
                }
            }

            state = state.With(
                s => s.Enemies,
                remainEnemies.ToArray());

            return _ProcessGeneratingEnemies(state);
        }
        
        private GamePlayEngine.State _ProcessGeneratingEnemies(GamePlayEngine.State currentState)
            => currentState.Player.AreaRange.Position.X > _initialEnemyPositionX + _currentUpdatedEnemyIndex * (ENEMY_DISTANCE - 1) ?
                    _UpdateCurrentUpdatedEnemyPositionX(_DecideWhetherGeneratingEnemies(currentState)) :
                    currentState;

        private GamePlayEngine.State _DecideWhetherGeneratingEnemies(GamePlayEngine.State currentState)
            => RandomValue.GetBool() ?
                    currentState.With(
                        state => state.Enemies,
                        currentState.Enemies.Concat(new IEnemy[] {
                            _GenerateEnemy(_initialEnemyPositionX + _currentUpdatedEnemyIndex * ENEMY_DISTANCE)
                        }).ToArray())
                    : currentState;

        private GamePlayEngine.State _UpdateCurrentUpdatedEnemyPositionX(GamePlayEngine.State currentState)
        {
            ++_currentUpdatedEnemyIndex;
            return currentState;
        }

        private IEnemy _GenerateEnemy(float currentEnemyDistance)
            => new Monster(new Vector2D(currentEnemyDistance + ENEMY_DISTANCE, INIT_ENEMY_Y));
    }
}