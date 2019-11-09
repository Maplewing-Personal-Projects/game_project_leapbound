namespace Maplewing.LeapBound.Engine
{
    public class GamePlayEngine
    {
        public class State
        {
            public Player Player;
            public long Money;
        }

        public State CurrentState;

        public GamePlayEngine(State state)
            => CurrentState = state;

        public State Update(float deltaTime)
            => CurrentState = new State
            {
                Player = CurrentState.Player.Move(deltaTime)
            };
    }
}