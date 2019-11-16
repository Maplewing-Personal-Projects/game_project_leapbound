using Maplewing.LeapBound.Engine.Data;

namespace Maplewing.LeapBound.Engine
{
    public class SwordBullet : IBullet
    {
        private static readonly Vector2D INIT_SIZE = new Vector2D(0.2f, 0.2f);

        string IBullet.Id { get; } = "SwordBullet";
        int IBullet.Atk { get; } = 15;
        Rectangle IBullet.AreaRange { get => _areaRange; }
        TargetType IBullet.Target { get => _target; }

        private Rectangle _areaRange;
        private Vector2D _speed;
        private Vector2D _acceleration;
        private TargetType _target;

        public SwordBullet(Vector2D position, Vector2D speed, Vector2D accerelation, TargetType target)
        {
            _areaRange = new Rectangle(position, INIT_SIZE);
            _speed = speed;
            _acceleration = accerelation;
            _target = target;
        }

        IBullet IBullet.Move(float deltaTime)
            => new SwordBullet(
                _areaRange.Position + _speed * deltaTime,
                _speed + _acceleration * deltaTime,
                _acceleration,
                _target);
    }
}