using LaYumba.Functional;

namespace Maplewing.LeapBound.Engine
{
    public class PlayerJumpCommand : ICommand
    {
        GamePlayEngine.State ICommand.Execute(GamePlayEngine.State state)
            => state.With(
                s => s.Player,
                state.Player.Jump());
    }
}