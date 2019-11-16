using LaYumba.Functional;
using Maplewing.LeapBound.Engine.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Maplewing.LeapBound.Engine
{
    public class BulletManager
    {
        public GamePlayEngine.State UpdateState(GamePlayEngine.State currentState, float deltaTime)
        {
            var enemies = currentState.Enemies;
            var state = currentState;
            var remainEnemies = new List<IEnemy>();
            var bulletStates = currentState.Bullets
                .Select(bullet => (bullet, nextBullet: bullet.Move(deltaTime))).ToList();
            foreach (var enemy in enemies)
            {
                var currentEnemy = enemy;
                var bullets = state.Bullets;
                var remainBullets = new List<(IBullet bullet, IBullet nextBullet)>();
                foreach (var bulletPair in bulletStates)
                {
                    var moveRange = GamePlayEngine.GetMoveRange(bulletPair.bullet.AreaRange, bulletPair.nextBullet.AreaRange);
                    if (!currentEnemy.IsDead() && currentEnemy.AreaRange.IsIntersect(moveRange))
                    {
                        currentEnemy = currentEnemy.BeInjured(bulletPair.bullet.Atk);
                    }
                    else if(bulletPair.nextBullet.AreaRange.IsIntersect(state.PlayRange))
                    {
                        remainBullets.Add(bulletPair);
                    }
                }

                if (currentEnemy.IsDead())
                {
                    state = state.With(
                        s => s.Score,
                        state.Score + currentEnemy.Score);
                }
                else
                {
                    remainEnemies.Add(currentEnemy);
                }

                bulletStates = remainBullets;
            }

            return state.With(
                s => s.Enemies,
                remainEnemies.ToArray())
                .With(
                    s => s.Bullets,
                    bulletStates.Select(bulletPair => bulletPair.nextBullet).ToArray());
        }
        
    }
}