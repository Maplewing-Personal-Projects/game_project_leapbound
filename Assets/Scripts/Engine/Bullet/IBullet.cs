using Maplewing.LeapBound.Engine.Data;

namespace Maplewing.LeapBound.Engine
{
    public enum TargetType
    {
        Player,
        Enemy
    }

    public interface IBullet
    {
        string Id { get; }
        int Atk { get; }
        Rectangle AreaRange { get; }
        TargetType Target { get; }

        IBullet Move(float deltaTime);
    }
}