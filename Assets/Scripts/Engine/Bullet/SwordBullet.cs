using Maplewing.LeapBound.Engine.Data;

namespace Maplewing.LeapBound.Engine
{
    public class SwordBullet : IBullet
    {
        private static readonly Vector2D INIT_SIZE = new Vector2D(0.2f, 0.2f);
        private static readonly float INIT_SPEED = 25f;

        string IBullet.Id { get; } = "SwordBullet";
        int IBullet.Atk { get; } = 15;
        Rectangle IBullet.AreaRange { get => _areaRange; }
        TargetType IBullet.Target { get => _target; }

        private Rectangle _areaRange;
        private Vector2D _speed;
        private TargetType _target;

        public SwordBullet(Vector2D position, Vector2D direction, TargetType target)
        {
            _areaRange = new Rectangle(position, INIT_SIZE);
            _speed = direction.Normalized() * INIT_SPEED;
            _target = target;
        }

        public SwordBullet Move(float deltaTime)
            => new SwordBullet(
                _areaRange.Position + _speed * deltaTime,
                _speed,
                _target);
    }
}