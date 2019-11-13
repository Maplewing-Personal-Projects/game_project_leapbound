using Maplewing.LeapBound.Engine.Data;
using System;

namespace Maplewing.LeapBound.Engine
{
    public class Monster : IEnemy
    {
        private static readonly Vector2D INIT_SIZE = new Vector2D(0.2f, 1f);
        private const int MAX_HP = 30;

        string IEnemy.Id { get; } = "Monster";
        int IEnemy.HP { get => _hp; }
        int IEnemy.Atk { get; } = 10;
        Rectangle IEnemy.AreaRange { get => _areaRange; }

        private Rectangle _areaRange;
        private int _hp;

        public Monster(Vector2D position, int hp = MAX_HP)
        { 
            _areaRange = new Rectangle(position, INIT_SIZE);
            _hp = hp;
        }

        IEnemy IEnemy.BeInjured(int enemyAtk)
            => new Monster(_areaRange.Position, _hp - enemyAtk);

        bool IEnemy.IsDead()
            => _hp <= 0;
    }
}
