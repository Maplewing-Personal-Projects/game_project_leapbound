using Maplewing.LeapBound.Engine.Data;

namespace Maplewing.LeapBound.Engine
{
    public class GamePlayEngine
    {
        public Player Player { get; private set; }

        public GamePlayEngine(Player player)
            => Player = player;

        public GamePlayEngine Update(float deltaTime)
            => new GamePlayEngine(Player.Move(deltaTime));
    }
}