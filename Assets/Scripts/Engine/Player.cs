using Maplewing.LeapBound.Engine.Data;
using LaYumba.Functional;

namespace Maplewing.LeapBound.Engine
{
    public class Player
    {
        private const float X_MOVE_SPEED = 10f;
        private const float JUMP_SPEED = 10f;
        private const float GRAVITY_SPEED = 30f;
        private static readonly Vector2D INIT_SPEED = new Vector2D(X_MOVE_SPEED, 0f);
        private const float GROUND_Y = 0f;

        public Vector2D Position { get; private set; }

        private Vector2D _currentSpeed;

        public Player(Vector2D position, Vector2D speed = null)
        {
            Position = position;
            _currentSpeed = speed ?? INIT_SPEED;
        }

        public Player Move(float deltaTime)
            => _IsHitGround() && _IsFallState() ?
                new Player(
                    new Vector2D(Position.X + _currentSpeed.X * deltaTime, GROUND_Y),
                    new Vector2D(_currentSpeed.X, 0f)) :
                new Player(
                    Position + _currentSpeed * deltaTime,
                    _currentSpeed - new Vector2D(0f, GRAVITY_SPEED) * deltaTime);

        public Player Jump()
            => _IsInAirState() ?
                this :
                new Player(Position, new Vector2D(_currentSpeed.X, JUMP_SPEED));

        private bool _IsHitGround()
            => Position.Y <= GROUND_Y;

        private bool _IsInAirState()
            => _currentSpeed.Y != 0f && !_IsHitGround();

        private bool _IsFallState()
            => _currentSpeed.Y < 0f;
    }
}
