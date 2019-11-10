using Maplewing.LeapBound.Engine.Data;
using LaYumba.Functional;

namespace Maplewing.LeapBound.Engine
{
    public class Player
    {
        private const float X_MOVE_SPEED = 10f;
        private const float JUMP_SPEED = 10f;
        private const float GRAVITY_SPEED = 30f;
        private const float GROUND_Y = 0f;
        
        private static readonly Vector2D INIT_SPEED = new Vector2D(X_MOVE_SPEED, 0f);
        private static readonly Vector2D INIT_SIZE = new Vector2D(1, 1);
        private const int INIT_HP = 5;

        public Rectangle AreaRange { get; private set; }
        public int HP { get; private set; }

        private Vector2D _currentSpeed;

        public Player(Vector2D position, Vector2D speed = null)
        {
            AreaRange = new Rectangle(
                position,
                INIT_SIZE);
            HP = INIT_HP;
            _currentSpeed = speed ?? INIT_SPEED;
        }

        public Player Move(float deltaTime)
            => _IsHitGround() && _IsFallState() ?
                new Player(
                    new Vector2D(AreaRange.Position.X + _currentSpeed.X * deltaTime, GROUND_Y),
                    new Vector2D(_currentSpeed.X, 0f)) :
                new Player(
                    AreaRange.Position + _currentSpeed * deltaTime,
                    _currentSpeed - new Vector2D(0f, GRAVITY_SPEED) * deltaTime);

        public Player Jump()
            => _IsInAirState() ?
                this :
                new Player(AreaRange.Position, new Vector2D(_currentSpeed.X, JUMP_SPEED));

        private bool _IsHitGround()
            => AreaRange.Position.Y <= GROUND_Y;

        private bool _IsInAirState()
            => _currentSpeed.Y != 0f && !_IsHitGround();

        private bool _IsFallState()
            => _currentSpeed.Y < 0f;
    }
}
