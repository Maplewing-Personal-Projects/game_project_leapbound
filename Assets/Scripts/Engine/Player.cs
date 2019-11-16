using Maplewing.LeapBound.Engine.Data;
using LaYumba.Functional;
using System;

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
        private const int INIT_HP = 30;
        private const float INIT_INVISIBLE_TIME = 1f;

        public Rectangle AreaRange { get; private set; }
        public int HP { get; private set; }
        public int MaxHP { get; } = INIT_HP;

        private Vector2D _currentSpeed;
        private Vector2D _acceleration;
        private float _invisibleTime = 0f;

        public Player(
            Vector2D position, 
            Vector2D speed = null, 
            Vector2D acceleration = null,
            int hp = INIT_HP,
            float invisibleTime = 0f)
        {
            AreaRange = new Rectangle(
                position,
                INIT_SIZE);
            HP = hp;
            _invisibleTime = invisibleTime;
            _currentSpeed = speed ?? INIT_SPEED;
            _acceleration = acceleration ?? Vector2D.ZERO;
        }

        public Player Move(float deltaTime)
        {
            if (IsDead()) return this;

            float nextInvisibleTime = Math.Max(_invisibleTime - deltaTime, 0f);
            if (_IsHitGround() && _IsFallState())
            {
                return new Player(
                    new Vector2D(AreaRange.Position.X + _currentSpeed.X * deltaTime, GROUND_Y),
                    new Vector2D(_currentSpeed.X, 0f),
                    _acceleration,
                    HP,
                    nextInvisibleTime);
            }
            
            return new Player(
                AreaRange.Position + _currentSpeed * deltaTime,
                _currentSpeed - new Vector2D(0f, GRAVITY_SPEED) * deltaTime + _acceleration * deltaTime,
                _acceleration,
                HP,
                nextInvisibleTime);
        }

        public Player Jump()
            => _IsInAirState() ?
                this :
                new Player(AreaRange.Position, new Vector2D(_currentSpeed.X, JUMP_SPEED), _acceleration, HP, _invisibleTime);

        public Player BeInjured(int enemyAtk)
            => IsInvisible() ?
                this :
                new Player(AreaRange.Position, _currentSpeed, _acceleration, HP - enemyAtk, INIT_INVISIBLE_TIME);

        public SwordBullet Attack()
            => new SwordBullet(AreaRange.Position, new Vector2D(_currentSpeed.X + 20f, 0f), _acceleration, TargetType.Enemy);
        
        public bool IsDead()
            => HP <= 0f;

        public bool IsInvisible()
            => _invisibleTime > 0f;

        private bool _IsHitGround()
            => AreaRange.Position.Y <= GROUND_Y;

        private bool _IsInAirState()
            => _currentSpeed.Y != 0f && !_IsHitGround();

        private bool _IsFallState()
            => _currentSpeed.Y < 0f;

    }
}
