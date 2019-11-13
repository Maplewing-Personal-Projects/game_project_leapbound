using Maplewing.LeapBound.Engine.Data;

namespace Maplewing.LeapBound.Engine
{
    public interface IEnemy
    {
        string Id { get; }
        int HP { get; }
        int Atk { get; }
        Rectangle AreaRange { get; }

        IEnemy BeInjured(int enemyAtk);
        bool IsDead();
    }
}