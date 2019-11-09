using Maplewing.LeapBound.Engine.Data;

namespace Maplewing.LeapBound.Engine
{
    public class Player
    {
        private const float X_MOVE_SPEED = 1f;

        public Vector2D Position { get; private set; }
        
        public Player(Vector2D position)
            => Position = position;

        public Player Move(float deltaTime)
            => new Player(Position + new Vector2D(X_MOVE_SPEED * deltaTime, 0));
    }
}
